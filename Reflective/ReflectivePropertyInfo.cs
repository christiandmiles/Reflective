using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflective
{
    public class ReflectivePropertyInfo : PropertyInfo
    {
        private readonly PropertyInfo _value;

        private readonly ParameterInfo[] _indexParameters;

        // Inherited accessors from _value
        public override PropertyAttributes Attributes => _value.Attributes;

        public override bool CanRead => _value.CanRead;

        public override bool CanWrite => _value.CanWrite;

        public override Type PropertyType => _value.PropertyType;

        public override Type DeclaringType => _value.DeclaringType;

        public override string Name => _value.Name;

        public override Type ReflectedType => _value.ReflectedType;

        // Backing delegates for GetValue

        private Func<object, object> _getValueDelegate;

        private Action<object, object> _setValueDelegate;

        public ReflectivePropertyInfo(PropertyInfo property)
        {
            _value = property;
            _indexParameters = property.GetIndexParameters();
            _getValueDelegate = BuildGetAccessor(property.Name, property.GetGetMethod());
            _setValueDelegate = BuildSetAccessor(property.Name, property.GetSetMethod());
        }

        // Delegate creation methods
        // Taken from https://stackoverflow.com/questions/10820453/reflection-performance-create-delegate-properties-c
        // and https://stackoverflow.com/questions/36788994/reflection-performance-create-delegate-properties-c-with-conditional-parame
        private Action<object, object> BuildSetAccessor(string name, MethodInfo method)
        {
            if (method == null) return null;
            if (method.DeclaringType == null) return null;
            var obj = Expression.Parameter(typeof(object), name);
            var value = Expression.Parameter(typeof(object));
            var expr = Expression.Lambda<Action<object, object>>(Expression.Call(Expression.Convert(obj, method.DeclaringType), method, Expression.Convert(value, method.GetParameters()[0].ParameterType)), obj, value);
            return expr.Compile();
        }

        private Func<object, object> BuildGetAccessor(string name, MethodInfo method)
        {
            if (method.DeclaringType == null) return null;
            var obj = Expression.Parameter(typeof(object), name);
            var expr = Expression.Lambda<Func<object, object>>(Expression.Convert(Expression.Call(Expression.Convert(obj, method.DeclaringType), method), typeof(object)), obj);
            return expr.Compile();
        }

        // Cached delegates for GetValue and SetValue
        public new object GetValue(object target) => _getValueDelegate.Invoke(target);

        public new void SetValue(object target, object value) => _setValueDelegate.Invoke(target, value);

        // Inherited methods from _value - these are not cached
        public override MethodInfo[] GetAccessors(bool nonPublic) => _value.GetAccessors(nonPublic);

        public override MethodInfo GetGetMethod(bool nonPublic) => _value.GetGetMethod(nonPublic);

        public override ParameterInfo[] GetIndexParameters() => _indexParameters;

        public override MethodInfo GetSetMethod(bool nonPublic) => _value.GetSetMethod(nonPublic);

        public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture) => _value.GetValue(obj, invokeAttr, binder, index, culture);

        public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture) => _value.SetValue(obj, value, invokeAttr, binder, index, culture);

        public override object[] GetCustomAttributes(bool inherit) => _value.GetCustomAttributes(inherit);

        public override bool IsDefined(Type attributeType, bool inherit) => _value.IsDefined(attributeType, inherit);

        public override object[] GetCustomAttributes(Type attributeType, bool inherit) => _value.GetCustomAttributes(attributeType, inherit);
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Reflective
{
    public class ReflectiveType: Type
    {
		private readonly Type _type;

        // Cached properties
		private readonly IList<ReflectivePropertyInfo> _properties;

        // Methods from Type class
        public override Assembly Assembly => _type.Assembly;

        public override string AssemblyQualifiedName => _type.AssemblyQualifiedName;

        public override Type BaseType => _type.BaseType;

        public override string FullName => _type.FullName;

        public override Guid GUID => _type.GUID;

        public override Module Module => _type.Module;

        public override string Namespace => _type.Namespace;

        public override Type UnderlyingSystemType => _type.UnderlyingSystemType;

        public override string Name => _type.Name;

        public ReflectiveType(Type type)
		{
			_type = type ?? throw new ArgumentNullException(nameof(type));
			_properties = type.GetProperties().Select(x => new ReflectivePropertyInfo(x)).ToList();
		}

        // Uses cached properties list to get full list of properties
        public new IList<ReflectivePropertyInfo> GetProperties() => _properties;

        // Uncached methods, identical to on underlying type
        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr) => _type.GetConstructors(bindingAttr);

        public override Type GetElementType() => _type.GetElementType();

        public override EventInfo GetEvent(string name, BindingFlags bindingAttr) => _type.GetEvent(name, bindingAttr);

        public override EventInfo[] GetEvents(BindingFlags bindingAttr) => _type.GetEvents(bindingAttr);

        public override FieldInfo GetField(string name, BindingFlags bindingAttr) => _type.GetField(name, bindingAttr);

        public override FieldInfo[] GetFields(BindingFlags bindingAttr) => _type.GetFields(bindingAttr);

        public override Type GetInterface(string name, bool ignoreCase) => _type.GetInterface(name, ignoreCase);

        public override Type[] GetInterfaces() => _type.GetInterfaces();

        public override MemberInfo[] GetMembers(BindingFlags bindingAttr) => _type.GetMembers(bindingAttr);

        public override MethodInfo[] GetMethods(BindingFlags bindingAttr) => _type.GetMethods(bindingAttr);

        public override Type GetNestedType(string name, BindingFlags bindingAttr) => _type.GetNestedType(name, bindingAttr);

        public override Type[] GetNestedTypes(BindingFlags bindingAttr) => _type.GetNestedTypes(bindingAttr);

        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr) => _type.GetProperties(bindingAttr);

        public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters) => _type.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);

        public override object[] GetCustomAttributes(bool inherit) => _type.GetCustomAttributes(inherit);

        public override object[] GetCustomAttributes(Type attributeType, bool inherit) => _type.GetCustomAttributes(attributeType, inherit);

        public override bool IsDefined(Type attributeType, bool inherit) => _type.IsDefined(attributeType, inherit);

        // Protected methods on base class - not implemented here due to lack of need
        protected override TypeAttributes GetAttributeFlagsImpl() => throw new NotImplementedException();

        protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers) => throw new NotImplementedException();

        protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers) => throw new NotImplementedException();

        protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers) => throw new NotImplementedException();

        protected override bool HasElementTypeImpl() => throw new NotImplementedException();

        protected override bool IsArrayImpl() => throw new NotImplementedException();

        protected override bool IsByRefImpl() => throw new NotImplementedException();

        protected override bool IsCOMObjectImpl() => throw new NotImplementedException();

        protected override bool IsPointerImpl() => throw new NotImplementedException();

        protected override bool IsPrimitiveImpl() => throw new NotImplementedException();
    }
}

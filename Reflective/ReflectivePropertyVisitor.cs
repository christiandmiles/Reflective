using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Reflective
{
    public class ReflectivePropertyVisitor<T>
	{

		private readonly Func<T, Task> _visitorAction;
		public ReflectiveCache _reflectionCache;

		public ReflectivePropertyVisitor(Func<T, Task> visitorAction)
		{
			_visitorAction = visitorAction ?? throw new ArgumentNullException(nameof(visitorAction));
			_reflectionCache = new ReflectiveCache();
		}

		public async Task VisitAsync(object target)
		{
			var targetType = target.GetType();

			if (targetType == typeof(T))
			{
				await _visitorAction((T)target);
			}

			if (targetType == typeof(string) || targetType.IsValueType)
				return;

			if (target is IEnumerable targetEnumerator)
			{
				foreach (var item in targetEnumerator)
				{
					await VisitAsync(item);
				}
			}
			else
			{
					var properties = _reflectionCache.Get(targetType).GetProperties()
					.Where(p => p.CanRead
						&& !p.PropertyType.IsPointer
						&& p.GetIndexParameters().Length == 0);

				foreach (var prop in properties)
				{
					var val = prop.GetValue(target);
					await VisitAsync(val);
				}
			}
		}
	}
}

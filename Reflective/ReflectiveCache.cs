using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Reflective
{
	public class ReflectiveCache
	{

		private readonly ConcurrentDictionary<Type, ReflectiveType> _cache = new ConcurrentDictionary<Type, ReflectiveType>();

		public ReflectiveType Get(object target) => Get(target?.GetType());

		public ReflectiveType Get(Type type)
		{
			_ = type ?? throw new ArgumentNullException(nameof(type));
			return _cache.GetOrAdd(type, (t) => new ReflectiveType(t));
		}
	}
}

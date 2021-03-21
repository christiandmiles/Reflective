using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Reflective.Tests
{
    [TestFixture]
    public class ReflectiveCacheTests
    {
        [Test]
        public void Get_WhenGivenObject_ReturnsReflectiveType()
        {
			var cache = new ReflectiveCache();

			var basicObject = new { DataItemID = 1, DataItemCurrentStatusID = 1, DataItemStatusID = 1, DateEffective = DateTime.Now };

			var result = cache.Get(basicObject);

			Assert.AreEqual(typeof(ReflectiveType), result.GetType());
		}

	}
}

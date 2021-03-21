using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Reflective.Tests
{
    [TestFixture]
    public class ReflectivePropertyInfoTests
    {
        [Test]
        public void SetValue_WhenCalled_SetsProperty()
        {
			var cache = new ReflectiveCache();
			var basicObject = new DataStatusItem { DataItemID = 1, DataItemCurrentStatusID = 1, DataItemStatusID = 1, DateEffective = DateTime.Now };
			var type = cache.Get(basicObject);

            var prop = type.GetProperties().Where(x => x.Name == nameof(DataStatusItem.DataItemID)).First();

            prop.SetValue(basicObject, 2);

			Assert.AreEqual(2, basicObject.DataItemID);
		}

		private class DataStatusItem
		{
			public int DataItemID { get; set; }
			public int DataItemCurrentStatusID { get; set; }
			public int DataItemStatusID { get; set; }
			public DateTime DateEffective { get; set; }
		}
	}
}

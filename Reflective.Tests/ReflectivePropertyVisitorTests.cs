using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Reflective.Tests
{
    [TestFixture]
    public class ReflectivePropertyVisitorTests
    {
        [Test]
        public async Task VisitAsync_WhenGivenComplexObject_IsCalledCorrectNumberOfTimes()
        {
            var i = 0;
            var visitor = new ReflectivePropertyVisitor<DataStatusItem>(x =>
            {
                i++;
                return Task.CompletedTask;
            });

			var testObject = new
			{
				DataStatusItem = new List<DataStatusItem>
				{
					new DataStatusItem { DataItemID = 1, DataItemCurrentStatusID = 1, DataItemStatusID = 1, DateEffective = DateTime.Now },
					new DataStatusItem { DataItemID = 2, DataItemCurrentStatusID = 2, DataItemStatusID = 2, DateEffective = DateTime.Now },
				},
				MySubThingy = new
				{
					AnotherSubThingy = new
					{
						Child = new DataStatusItem { }
					}
				},
				Brand = 1,
				DataType = 2,
				ProcessingStatus = 3,
				IntList = new List<int> {
					11,
					22,
					33
				}
			};

			var basicObject = new DataStatusItem { DataItemID = 1, DataItemCurrentStatusID = 1, DataItemStatusID = 1, DateEffective = DateTime.Now };

			await visitor.VisitAsync(testObject);
			await visitor.VisitAsync(basicObject);

			Assert.AreEqual(4, i);
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

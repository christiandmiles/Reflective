using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reflective.Benchmarks
{
    public class ReflectivePropertyVisitorBenchmarks
    {
        private ReflectivePropertyVisitor<DataStatusItem> _cachedVisitor;
        private PropertyVisitor<DataStatusItem> _visitor;
        private object _testObject;

        public ReflectivePropertyVisitorBenchmarks()
        {
			_cachedVisitor = new ReflectivePropertyVisitor<DataStatusItem>(x =>
			{
				return Task.CompletedTask;
			});

			_visitor = new PropertyVisitor<DataStatusItem>(x =>
			{
				return Task.CompletedTask;
			});

			_testObject = new
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
		}

        [Benchmark]
        public Task WithCache() =>  _cachedVisitor.VisitAsync(_testObject);

		[Benchmark]
		public Task WithoutCache() => _visitor.VisitAsync(_testObject);

		private class DataStatusItem
		{
			public int DataItemID { get; set; }
			public int DataItemCurrentStatusID { get; set; }
			public int DataItemStatusID { get; set; }
			public DateTime DateEffective { get; set; }
		}
	}
}

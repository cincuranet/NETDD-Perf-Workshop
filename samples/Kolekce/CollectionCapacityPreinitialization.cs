using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Kolekce
{
	[MemoryDiagnoser]
	public class CollectionCapacityPreinitialization
	{
		[Params(100, 1_000, 10_000, 100_000, 1_000_000)]
		public int CollectionSize { get; set; }

		[Benchmark]
		public object ListPlain()
		{
			var list = new List<object>();               // <--- Grows: 0, 4, 8, 16, 32, 64, ...
			for (int i = 0; i < CollectionSize; i++)
			{
				list.Add(item);
			}
			return list;
		}

		[Benchmark]
		public object ListPreinitialized()
		{
			var list = new List<object>(CollectionSize);  // <--- Initial Capacity
			for (int i = 0; i < CollectionSize; i++)
			{
				list.Add(item);
			}
			return list;
		}

		readonly object item = new object();
	}
}

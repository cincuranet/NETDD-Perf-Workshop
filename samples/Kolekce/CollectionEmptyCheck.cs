using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Kolekce
{
	public class CollectionEmptyCheck
	{
		[Params(100, 1_000, 10_000, 50_000)]
		public int CollectionSize { get; set; }

		[Benchmark]
		public bool List_IsEmpty_Count()
		{
			return list.Count() > 0;
		}

		[Benchmark]
		public bool List_IsEmpty_Any()
		{
			return list.Any();
		}

		[Benchmark]
		public bool IEnumerable_IsEmpty_Count()
		{
			return ienumerable.Count() > 0;
		}

		[Benchmark]
		public bool IEnumerable_IsEmpty_Any()
		{
			return ienumerable.Any();
		}

		List<Guid> list;
		IEnumerable<Guid> ienumerable => list.Select(i => i);

		[IterationSetup]
		public void IterationSetup()
		{
			list = new List<Guid>();
			foreach (var guid in Enumerable.Range(0, CollectionSize).Select(g => Guid.NewGuid()))
			{
				list.Add(guid);
			}
		}
	}
}

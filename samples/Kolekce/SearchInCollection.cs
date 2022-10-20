using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Kolekce
{
	public class SearchInCollection
	{
		[Params(100, 1_000, 10_000, 50_000)]
		public int CollectionSize { get; set; }

		[Benchmark(Description = "List.Contains() : O(n)     ")]
		public int Contains()
		{
			// Contains = sekvenční vyhledávání = O(n), též hledání LINQ-to-XY: .Where(), First(), Count(), ... !!!
			return hledane.Count(t => list.Contains(t));
		}


		[Benchmark(Description = "Array.BinarySearch() : O(log(n))")]
		public int BinarySearch()
		{
			// binární půlení = O(log(n))
			return hledane.Count(t => Array.BinarySearch<Guid>(sortedArray, t) >= 0);
		}


		[Benchmark(Description = "Dictionary : O(1)     ")]
		public int Dictionary()
		{
			// Dictionary = Hashtable, O(1), též HashSet
			return hledane.Count(t => dictionary.ContainsKey(t));
		}


		[Benchmark(Description = "LINQ.ToLookup() : O(1)     ")]
		public int ToLookup()
		{
			// ToLookup = Hashtable, O(1)
			return hledane.Count(i => lookup.Contains(i));
		}


		List<Guid> list;
		List<Guid> hledane;
		Dictionary<Guid, object> dictionary;
		ILookup<Guid, Guid> lookup;
		Guid[] sortedArray;

		[IterationSetup]
		public void IterationSetup()
		{
			list = new List<Guid>();
			dictionary = new Dictionary<Guid, object>();
			foreach (var guid in Enumerable.Range(0, CollectionSize).Select(g => Guid.NewGuid()))
			{
				list.Add(guid);
				dictionary.Add(guid, null);
			}
			lookup = list.ToLookup(i => i);
			sortedArray = list.ToArray();
			Array.Sort(sortedArray);

			var rand = new Random();
			hledane = Enumerable.Range(0, CollectionSize / 2).Select(g => (rand.NextDouble() > 0.5) ? Guid.NewGuid() : list[rand.Next(list.Count)]).ToList();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Kolekce
{
	public class DictionaryRemoveRedundantChecks
	{
		const int DictionarySize = 100_000;

		[Benchmark]
		public void RemoveRedundancy()
		{
			foreach (var key in keys)
			{
				if (dictionary.ContainsKey(key))
				{
					dictionary.Remove(key);
				}
			}
		}

		[Benchmark]
		public void SimpleRemove()
		{
			foreach (var key in keys)
			{
				dictionary.Remove(key);  // Remove method returns false if key is not found
			}
		}


		Dictionary<Guid, string> dictionary;
		List<Guid> keys;
		public IEnumerable<object> GetKeys() => keys.Cast<object>();

		[IterationSetup]
		public void IterationSetup()
		{
			keys = new List<Guid>();
			dictionary = new Dictionary<Guid, string>();
			foreach (var guid in Enumerable.Range(0, DictionarySize).Select(g => Guid.NewGuid()))
			{
				keys.Add(guid);
				dictionary.Add(guid, "item");
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Kolekce
{
	public class DictionaryPickupRedundantChecks
	{
		const int DictionarySize = 100_000;

		[Benchmark]
		public string PickupRedundancy()
		{
			string item = null;

			if (dictionary.ContainsKey(key))
			{
				item = dictionary[key];
			}

			return item;
		}

		[Benchmark]
		public object SimplePickup()
		{
			dictionary.TryGetValue(key, out string item);  // TryGetValue returns true, if key is found

			return item;
		}

		Dictionary<Guid, string> dictionary;
		Guid key;

		[IterationSetup]
		public void IterationSetup()
		{
			dictionary = new Dictionary<Guid, string>();
			foreach (var guid in Enumerable.Range(0, DictionarySize).Select(g => Guid.NewGuid()))
			{
				dictionary.Add(guid, "item");
			}
			key = dictionary.Keys.First();
		}
	}
}

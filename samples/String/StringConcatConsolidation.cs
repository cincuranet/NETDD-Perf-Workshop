using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace String
{
	[MemoryDiagnoser]
	public class StringConcatConsolidation
	{
		readonly string s1 = "Blah";
		readonly string s2 = "Dooh";
		readonly string s3 = "Hey!!!!!!!";

		[Benchmark]
		public string MultipleConcats()
		{
			var s = s1 + s2;
			return s + s3;
		}

		[Benchmark]
		public string ConsolidatedConcats()
		{
			return s1 + s2 + s3;
		}
	}
}

using System;
using BenchmarkDotNet.Attributes;

namespace String
{
	[MemoryDiagnoser]
	public class StringConcatConditionConsolidation
	{
		readonly string s1 = "Blah";
		readonly string s2 = "Dooh";
		readonly string s3 = "Hey!!!!!!!";
		readonly bool condition = (DateTime.Now.Hour >= 0);

		[Benchmark]
		public string MultipleConcats()
		{
			var s = s1 + s2;
			if (condition)
			{
				s = s + s3;
			}
			return s;
		}

		[Benchmark]
		public string ConsolidatedConcats()
		{
			return condition ? s1 + s2 + s3 : s1 + s2;
		}
	}
}

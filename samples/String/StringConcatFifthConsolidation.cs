using BenchmarkDotNet.Attributes;

namespace String
{
	[MemoryDiagnoser]
	public class StringConcatFifthConsolidation
	{
		readonly string s1 = "Blah";
		readonly string s2 = "Dooh";
		readonly string s3 = "Hey!!!!!!!";
		readonly string s4 = "More...";
		readonly string s5 = "Yeah";

		[Benchmark]
		public string MultipleConcats()             // See IL SPY - C# vs. IL code
		{
			var s = s1 + s2 + s3 + s4;              // Concat(string, string, string, string)
			return s + s5;                          // Concat(string, string)	
		}

		[Benchmark]
		public string ConsolidatedConcats()
		{
			return s1 + s2 + s3 + s4 + s5;          // build string[] + Concat(string[])
		}
	}
}

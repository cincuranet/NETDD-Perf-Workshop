using System;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace String
{
	[MemoryDiagnoser]
	public class SubstringAsSpan
	{
		readonly string inputString = "BlahBlahBlahBlahFooBarDaahBlahBlahBlahBlahFooBarDaahBlahBlahBlahBlahFooBarDaah";
		StringBuilder sb;

		[Benchmark]
		public StringBuilder Substring()
		{
			sb.Append(inputString.Substring(5, 20));

			return sb;
		}

		[Benchmark(Baseline = true)]
		public StringBuilder AsSpan()
		{
			sb.Append(inputString.AsSpan(5, 20));

			return sb;
		}

		[IterationSetup]
		public void IterationSetup()
		{
			sb = new StringBuilder(128);
		}
	}
}

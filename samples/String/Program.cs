using System;
using BenchmarkDotNet.Running;

namespace String
{
	class Program
	{
		static void Main(string[] args)
		{
			BenchmarkRunner.Run<StringConcatenation>();
			//BenchmarkRunner.Run<StringConcatConsolidation>();
			//BenchmarkRunner.Run<StringConcatConditionConsolidation>();
			//BenchmarkRunner.Run<StringConcatFifthConsolidation>();
			//BenchmarkRunner.Run<StringBuilderCapacityPreinitialization>();
			//BenchmarkRunner.Run<SubstringAsSpan>();
		}
	}
}

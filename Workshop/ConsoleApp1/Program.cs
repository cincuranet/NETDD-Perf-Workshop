using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace ConsoleApp1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			BenchmarkRunner.Run<MyTest>();
		}
	}

	[MemoryDiagnoser]
	public class MyTest
	{
		//[ParamsSource()]
		//public int Delay { get; set; }

		[GlobalCleanup]
		public void GlobalCleanup()
		{

		}

		[GlobalSetup]
		public void GlobalSetup()
		{ }

		//[Benchmark(Baseline = true)]
		//[Arguments(100)]
		//[Arguments(200)]
		//public void Test1(int delay)
		//{
		//	Thread.Sleep(delay);
		//}

		[Params(new[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
		public int[] Data { get; set; }

		[Benchmark(Baseline = true)]
		public int[] SumOld()
		{
			var result = new int[Data.Length];
			for (var i = 0; i < result.Length; i++)
			{
				result[i] = Data[i] + Data[i];
			}
			return result;
		}

		[Benchmark]
		public int[] SumVector()
		{
			var v1 = new Vector<int>(Data);
			var v2 = new Vector<int>(Data);
			var result = new int[Data.Length];
			Vector.Add(v1, v2).CopyTo(result);
			return result;
		}
	}
}
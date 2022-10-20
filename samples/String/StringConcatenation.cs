using System.Text;
using BenchmarkDotNet.Attributes;

namespace String
{
	[MemoryDiagnoser]
	[ShortRunJob]
	public class StringConcatenation
	{
		[Params(1, 2, 3, 100, 100_000)]
		public int Concatenations { get; set; }

		[Benchmark]
		public string StringConcat()
		{
			var str = string.Empty;

			for (int i = 0; i < Concatenations; i++)
			{
				str += "A";
			}

			return str;
		}

		[Benchmark]
		public string StringBuilder()
		{
			var sb = new StringBuilder();

			for (int i = 0; i < Concatenations; i++)
			{
				sb.Append("A");
			}

			return sb.ToString();
		}
	}
}

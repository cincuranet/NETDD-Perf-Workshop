using System.Text;
using BenchmarkDotNet.Attributes;

namespace String
{
	public class StringBuilderCapacityPreinitialization
	{
		[Params(100, 1_000, 10_000, 100_000, 500_000)]
		public int Size { get; set; }

		[Benchmark]
		public object StringBuilderPlain()
		{
			var sb = new StringBuilder();
			for (int i = 0; i < Size; i++)
			{
				sb.Append(item);
			}
			return sb;
		}

		[Benchmark]
		public object StringBuilderPreinitialized()
		{
			var sb = new StringBuilder(Size * item.Length); // <---- Initial Capacity
			for (int i = 0; i < Size; i++)
			{
				sb.Append(item);
			}
			return sb;
		}

		const string item = "AAAAAABBBBBBBBBBBCCCCCCCCCCCCCCCDDDDDDDDDDDDDDDDDDDEEEEEEEEEEEEEEEFFFFFFFFFFFFF";
	}
}

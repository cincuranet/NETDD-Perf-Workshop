using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Workshop
{
	internal class Program
	{
		static unsafe void Main(string[] args)
		{
			//var test = new int[10];
			//fixed (int* p = test)
			//{
			//	p[-2] = -5;
			//}
			//Console.WriteLine(test.Length);

			Console.WriteLine(Vector<int>.Count);
			var v1 = new Vector<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8 });
			var v2 = new Vector<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8 });
			var result = Vector.Add(v1, v2);
			Console.WriteLine(result);
			//System.Runtime.Intrinsics.X86.Avx.Add

			return;

			var arr = new int[] { 1, 2, 3, 4 };
			for (var i = 0; i < arr.Length -1; i++)
			{
				Console.WriteLine(arr[i]);
				Console.WriteLine(arr[i+1]);
			}

			const string DataFile = @"C:\Users\Jiri\Downloads\data.csv";

			while (true)
			{
				var sw = Stopwatch.StartNew();
				var content = ReadFile(DataFile);
				var data = ParseCSV(content.AsMemory());
				//var info = GC.GetGCMemoryInfo();
				Console.WriteLine(sw.Elapsed);
				Console.WriteLine(data.Count); 
			}
		}

		static List<List<ReadOnlyMemory<char>>> ParseCSV(ReadOnlyMemory<char> content)
		{
			var result = new List<List<ReadOnlyMemory<char>>>(100_000);
			foreach (var line in Split("\r\n", content))
			{
				result.Add(new List<ReadOnlyMemory<char>>(52));
				foreach (var item in Split(',', line))
				{
					result[result.Count - 1].Add(item);
				}
			}
			return result;
		}

		//static List<Item> Split(char separator, string data)
		//{
		//	var result = new List<Item>(52);
		//	var startIndex = 0;
		//	while (true)
		//	{
		//		var index = data.IndexOf(separator, startIndex);
		//		if (index == -1)
		//			break;
		//		result.Add(new Item() { S = data, Start = startIndex, End = index - 1 });
		//		startIndex = index + 1;
		//	}
		//	result.Add(new Item() { S = data, Start = startIndex, End = data.Length });
		//	return result;
		//}

		static List<ReadOnlyMemory<char>> Split(ReadOnlySpan<char> separator, ReadOnlyMemory<char> data)
		{
			var result = new List<ReadOnlyMemory<char>>(52);
			while (true)
			{
				var index = data.Span.IndexOf(separator, StringComparison.Ordinal);
				if (index == -1)
					break;
				result.Add(data.Slice(0, index - 1));
				data = data.Slice(index + separator.Length);
			}
			result.Add(data);
			return result;
		}
		static List<ReadOnlyMemory<char>> Split(char separator, ReadOnlyMemory<char> data)
		{
			var result = new List<ReadOnlyMemory<char>>(52);
			while (true)
			{
				var index = data.Span.IndexOf(separator);
				if (index == -1)
					break;
				result.Add(data.Slice(0, index - 1));
				data = data.Slice(index + 1);
			}
			result.Add(data);
			return result;
		}

		readonly struct Item
		{
			public string S { get; }
			public int Start { get; }
			public int End { get; }

			public override string ToString()
			{
				return S[Start..End];
			}

		}

		static ref Item Foo(Item[] item)
		{
			return ref item[0];
		}

		static unsafe string ReadFile(string path)
		{
			using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, 1, FileOptions.SequentialScan))
			{
				var data = new byte[(int)fs.Length];
				Span<byte> buffer = stackalloc byte[16 * 1024];
				//Span<byte> buffer = new byte[16 * 1024];
				var position = 0;
				while (true)
				{
					var read = fs.Read(buffer);
					if (read == 0)
						break;
					for (var i = 0; i < read; i++)
					{
						data[position++] = buffer[i];
					}
				}
				return Encoding.UTF8.GetString(data);
			}
		}
	}
}
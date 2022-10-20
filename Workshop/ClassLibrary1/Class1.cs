using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ClassLibrary1
{
	[Generator]
	public class Class1 : ISourceGenerator
	{
		public void Execute(GeneratorExecutionContext context)
		{
			context.AddSource("Foo.cs", SourceText.From("cfndvjlcndfsjf"));
		}

		public void Initialize(GeneratorInitializationContext context)
		{

		}
	}
}

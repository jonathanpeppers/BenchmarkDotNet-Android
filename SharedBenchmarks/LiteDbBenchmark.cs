using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LiteDB;
using System;
using System.IO;

namespace SharedBenchmarks
{
	/// <summary>
	/// Based on: https://github.com/dotnet/maui/issues/3100
	/// </summary>
	[MemoryDiagnoser]
	[Orderer(SummaryOrderPolicy.FastestToSlowest)]
	public class LiteDbBenchmark
	{
		[Benchmark]
		public void EnsureIndex()
		{
			new LiteDbSample();
		}
	}
}

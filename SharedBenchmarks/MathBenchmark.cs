using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System;

namespace SharedBenchmarks
{
	/// <summary>
	/// Based on: https://github.com/dotnet/maui/issues/3100
	/// </summary>
	[MemoryDiagnoser]
	[Orderer(SummaryOrderPolicy.FastestToSlowest)]
	public class MathBenchmark
	{
		const int length = 1024;
		readonly float[] data = new float[length];
		readonly Random random = new Random();

		[Benchmark]
		public void RandomNext()
		{
			double value = 300;
			for (int i = 0; i < length; i++)
			{
				if (random.NextDouble() > .5)
				{
					value += random.NextDouble();
				}
				else
				{
					value -= random.NextDouble();
				}
				data[i] = (float)Math.Round(value, 2) * 2.625f;
			}
		}
	}
}

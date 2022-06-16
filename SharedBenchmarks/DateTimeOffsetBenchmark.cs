using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System;

namespace SharedBenchmarks;

/// <summary>
/// Based on: https://github.com/jonathanpeppers/maui-profiling/issues/16
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class DateTimeOffsetBenchmark
{
    [Benchmark]
    public void Now() => _ = DateTimeOffset.Now;
}

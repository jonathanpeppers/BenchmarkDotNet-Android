using BenchmarkDotNet.Attributes;
using System;

namespace SharedBenchmarks;

/// <summary>
/// Based on: https://github.com/jonathanpeppers/maui-profiling/issues/16
/// </summary>
public class DateTimeOffsetBenchmark
{
    [Benchmark]
    public void Now() => _ = DateTimeOffset.Now;

    [Benchmark]
    public void UtcNow() => _ = DateTimeOffset.UtcNow;
}

using BenchmarkDotNet.Attributes;
using System;
using Android.App;
using Hello;

namespace SharedBenchmarks;

public class StringBenchmark
{
	readonly HelloClass hello = new();

	[Benchmark]
	public void SetText() => hello.Text = "Hello World!";

	[Benchmark]
	public void SetFinalText() => hello.FinalText = "Hello World!";
}

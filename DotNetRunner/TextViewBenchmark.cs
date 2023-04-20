using BenchmarkDotNet.Attributes;
using System;
using Android.App;
using Android.Widget;

namespace SharedBenchmarks;

public class TextViewBenchmark
{
	readonly TextView textView = new TextView(Application.Context);

	[Benchmark]
	public void SetText() => textView.Text = "Hello World!";
}

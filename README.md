# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 7 to Xamarin.Android

To run the benchmarks with .NET 7 (default):

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

To run the benchmarks with .NET 8:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark -p:Targeting=net8.0

To run the benchmarks with "classic" Xamarin.Android:

    msbuild ./XamarinRunner/XamarinRunner.csproj -restore -p:Configuration=Release -t:Benchmark

## Results

An example of running `TextViewBenchmark` on a Pixel 5 device:

|  Method |     Mean |     Error |    StdDev | Allocated |
|-------- |---------:|----------:|----------:|----------:|
| SetText | 7.588 us | 0.0088 us | 0.0078 us |     112 B |

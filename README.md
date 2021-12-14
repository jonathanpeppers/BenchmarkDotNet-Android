# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 6 to Xamarin.Android

To run the benchmarks with .NET 6:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

To run the benchmarks with "classic" Xamarin.Android:

    msbuild ./XamarinRunner/XamarinRunner.csproj -restore -p:Configuration=Release -t:Benchmark

## Results

An example of running `LiteDbBenchmark.cs` on a Pixel 6 Pro device:

|              Method |     Mean | Error | Allocated |
|-------------------- |---------:|------:|----------:|
| .NET 6  EnsureIndex | 295.2 ms |    NA |    157 KB |
| Xamarin EnsureIndex | 224.0 ms |    NA |         - |

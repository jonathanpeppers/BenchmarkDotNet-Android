# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 6 to Xamarin.Android

To run the benchmarks with .NET 6:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

To run the benchmarks with "classic" Xamarin.Android:

    msbuild ./XamarinRunner/XamarinRunner.csproj -restore -p:Configuration=Release -t:Benchmark

## Results

An example of running `LiteDbBenchmark.cs` on a Pixel 6 Pro device:

|              Method |     Mean |   Error |  StdDev |   Gen 0 |  Gen 1 |  Gen 2 | Allocated |
|-------------------- |---------:|--------:|--------:|--------:|-------:|-------:|----------:|
| .NET 6  EnsureIndex | 798.5 µs | 4.18 µs | 3.70 µs | 11.7188 | 5.8594 | 5.8594 |    152 KB |
| Xamarin EnsureIndex | 893.7 µs | 3.65 µs | 3.41 µs | 14.6484 | 6.8359 | 6.8359 |         - |

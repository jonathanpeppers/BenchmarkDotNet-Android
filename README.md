# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 6 to Xamarin.Android

To run the benchmarks with .NET 6:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

To run the benchmarks with "classic" Xamarin.Android:

    msbuild ./XamarinRunner/XamarinRunner.csproj -restore -p:Configuration=Release -t:Benchmark

## Results

An example of running `MathBenchmark.cs` on a Pixel 6 Pro device:

|              Method |     Mean |   Error |  StdDev |   Gen 0 |  Gen 1 |  Gen 2 | Allocated |
|-------------------- |---------:|--------:|--------:|--------:|-------:|-------:|----------:|
| .NET 6  EnsureIndex | 553.8 µs | 4.89 µs | 4.33 µs | 11.7188 | 5.8594 | 5.8594 |    134 KB |
| Xamarin EnsureIndex | 692.0 µs | 4.77 µs | 4.46 µs | 14.6484 | 6.8359 | 6.8359 |         - |

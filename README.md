# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 6 to Xamarin.Android

To run the benchmarks with .NET 6:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

To run the benchmarks with "classic" Xamarin.Android:

    msbuild ./XamarinRunner/XamarinRunner.csproj -restore -p:Configuration=Release -t:Benchmark

## Results

An example of running `MathBenchmark.cs` on a Pixel 6 Pro device:

|             Method |     Mean |    Error |   StdDev | Allocated |
|------------------- |---------:|---------:|---------:|----------:|
| .NET 6  RandomNext | 77.68 µs | 0.119 µs | 0.111 µs |         - |
| Xamarin RandomNext | 90.32 µs | 0.045 µs | 0.038 µs |         - |

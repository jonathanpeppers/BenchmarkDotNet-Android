# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 6 to Xamarin.Android

To run the benchmarks with .NET 6:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

To run the benchmarks with "classic" Xamarin.Android:

    msbuild ./XamarinRunner/XamarinRunner.csproj -restore -p:Configuration=Release -t:Benchmark

## Results

An example of running all 3 benchmarks on a Pixel 5 device:

|                     Method |     Mean |     Error |    StdDev | Allocated |
|--------------------------- |---------:|----------:|----------:|----------:|
| .NET 6  Random.Next        | 65.90 μs | 0.0720 μs | 0.0640 μs |         - |
| .NET 6  DateTimeOffset.Now | 1.046 μs | 0.0016 μs | 0.0015 μs |         - |
| .NET 6  Regex Compiled     | 134.7 μs | 3.0500 μs | 8.7600 μs |     20 KB |
| Xamarin Random.Next        | 84.55 μs | 0.0280 μs | 0.0260 μs |         - |
| Xamarin DateTimeOffset.Now | 4.938 μs | 0.0050 μs | 0.0046 μs |         - |
| Xamarin Regex Compiled     | 16.49 μs | 0.1990 μs | 0.1760 μs |         - |

*Note that `RegexOptions.Compiled` doesn't do anything in Xamarin.Android or mono/mono.*

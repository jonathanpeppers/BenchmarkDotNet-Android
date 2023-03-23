# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 7 to Xamarin.Android

To run the benchmarks with .NET 7 (default):

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

To run the benchmarks with .NET 8:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark -p:Targeting=net8.0

To run the benchmarks with "classic" Xamarin.Android:

    msbuild ./XamarinRunner/XamarinRunner.csproj -restore -p:Configuration=Release -t:Benchmark

## Results

An example of running all 3 benchmarks on a Pixel 5 device:

|                 Method |      Mean |     Error |    StdDev | Allocated |
|----------------------- |----------:|----------:|----------:|----------:|
| .NET 6  Random.Next    |  65.90 μs | 0.0720 μs | 0.0640 μs |         - |
| Xamarin Random.Next    |  84.55 μs | 0.0280 μs | 0.0260 μs |         - |
| .NET 6  DTO.Now        |   1.05 μs | 0.0016 μs | 0.0015 μs |         - |
| Xamarin DTO.Now        |   4.94 μs | 0.0050 μs | 0.0046 μs |         - |
| .NET 6  DTO.UtcNow     |   0.20 μs | 0.1100 ns | 0.0900 ns |         - |
| Xamarin DTO.UtcNow     |   0.22 μs | 0.0700 ns | 0.0600 ns |         - |
| .NET 6  Regex Compiled | 134.70 μs | 3.0500 μs | 8.7600 μs |     20 KB |
| Xamarin Regex Compiled |  16.49 μs | 0.1990 μs | 0.1760 μs |         - |

*Note that `RegexOptions.Compiled` doesn't do anything in Xamarin.Android or mono/mono.*

If you uncomment `#define COLD_START` in `MainInstrumentation.cs` and rerun:

|                     Method |      Mean | Allocated |
|--------------------------- |----------:|----------:|
| .NET 6  Random.Next        |  1.072 ms |         - |
| Xamarin Random.Next        |  1.434 ms |         - |
| .NET 6  DateTimeOffset.Now |  3.367 ms |         - |
| Xamarin DateTimeOffset.Now |  3.396 ms |         - |
| .NET 6  Regex Compiled     | 91.160 ms |     22 KB |
| Xamarin Regex Compiled     |  5.132 ms |         - |

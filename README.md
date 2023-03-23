# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 7 to Xamarin.Android

To run the benchmarks with .NET 7:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

To run the benchmarks with "classic" Xamarin.Android:

    msbuild ./XamarinRunner/XamarinRunner.csproj -restore -p:Configuration=Release -t:Benchmark

## Results

An example of running benchmarks on a Pixel 5 device:

|                Method |     Mean |   Error |  StdDev | Allocated |
|---------------------- |---------:|--------:|--------:|----------:|
| ContructorInfo_Invoke | 279.7 ns | 1.63 ns | 1.81 ns |      16 B |
|     MethodInfo_Invoke | 448.8 ns | 0.62 ns | 0.58 ns |         - |

*Note that `RegexOptions.Compiled` doesn't do anything in Xamarin.Android or mono/mono.*

If you uncomment `#define COLD_START` in `MainInstrumentation.cs` and rerun:



# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 7 to Xamarin.Android

To run the benchmarks with .NET 7:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

To run the benchmarks with .NET 8:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark -p:Targeting=net8.0

## Results

An example of running benchmarks on a Pixel 5 device:

| Version |                Method |     Mean |    Error |   StdDev | Allocated |
| ---     |---------------------- |---------:|---------:|---------:|----------:|
| .NET 7  | ContructorInfo_Invoke | 328.5 ns | 14.81 ns | 42.98 ns |      16 B |
| .NET 7  |     MethodInfo_Invoke | 479.2 ns |  1.36 ns |  1.21 ns |         - |
| .NET 8  | ContructorInfo_Invoke |  62.8 ns |  1.88 ns |  5.46 ns |      16 B |
| .NET 8  |     MethodInfo_Invoke | 300.5 ns |  0.72 ns |  0.64 ns |         - |

If you rerun with `-p:ColdStart=true`, this will simulate the cost at startup:

| Version |                Method |       Mean | Allocated |
| ---     |---------------------- |-----------:|----------:|
| .NET 7  | ContructorInfo_Invoke |   628.4 us |     552 B |
| .NET 7  |     MethodInfo_Invoke | 1,081.1 us |     800 B |
| .NET 8  | ContructorInfo_Invoke |   777.0 us |   4.98 KB |
| .NET 8  |     MethodInfo_Invoke |   961.3 us |   2.38 KB |

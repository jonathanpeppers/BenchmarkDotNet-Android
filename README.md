# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 7 to Xamarin.Android

To run the benchmarks with .NET 7:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

Should print: `[Host] : .NET 7.0.4 using MonoVM, Arm64`

To run the benchmarks with .NET 8:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark -p:Targeting=net8.0

Should print: `[Host] : .NET 8.0.0 using MonoVM, Arm64`

## Results

An example of running benchmarks on a Pixel 5 device:

| Version |                Method |     Mean |   Error |  StdDev | Allocated |
| ---     |---------------------- |---------:|--------:|--------:|----------:|
| .NET 7  | ContructorInfo_Invoke | 283.3 ns | 3.33 ns | 6.73 ns |      16 B |
| .NET 7  |     MethodInfo_Invoke | 448.5 ns | 1.15 ns | 1.02 ns |         - |
| .NET 8  | ContructorInfo_Invoke |  70.7 ns | 2.45 ns | 7.06 ns |      16 B |
| .NET 8  |     MethodInfo_Invoke | 296.7 ns | 1.02 ns | 0.96 ns |         - |

If you rerun with `-p:ColdStart=true`, this will simulate the cost at startup:

| Version |                Method |       Mean | Allocated |
| ---     |---------------------- |-----------:|----------:|
| .NET 7  | ContructorInfo_Invoke |   530.5 us |     552 B |
| .NET 7  |     MethodInfo_Invoke |   457.6 us |     128 B |
| .NET 8  | ContructorInfo_Invoke |   607.7 us |   4.09 KB |
| .NET 8  |     MethodInfo_Invoke | 1,202.1 us |   2.38 KB |

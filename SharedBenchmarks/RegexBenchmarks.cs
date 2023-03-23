using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace SharedBenchmarks;

/// <summary>
/// Based on: https://github.com/jonathanpeppers/maui-profiling/issues/16
/// https://github.com/microsoft/appcenter-sdk-dotnet/blob/b19ec99e16e554eb1382342b2852b16f7d8f0084/SDK/AppCenter/Microsoft.AppCenter.Shared/AppCenter.cs#L26
/// </summary>
public class RegexBenchmarks
{
    [Benchmark]
    public void RegexOptionsCompiled() => _ = new Regex(@"([^;=]+)=([^;]+)(?:;\s*)?", RegexOptions.Compiled);
}

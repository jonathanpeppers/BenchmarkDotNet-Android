# BenchmarkDotNet-Android

BenchmarkDotNet projects for comparing .NET 7 to Xamarin.Android

To run the benchmarks with .NET 7 (default):

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark

To run the benchmarks with .NET 8:

    dotnet build ./DotNetRunner/DotNetRunner.csproj -c Release -t:Benchmark -p:Targeting=net8.0

To run the benchmarks with "classic" Xamarin.Android:

    msbuild ./XamarinRunner/XamarinRunner.csproj -restore -p:Configuration=Release -t:Benchmark

## Results

An example of running `StringBenchmark` on a Pixel 5 device, before and
after [java.interop#1101](https://github.com/xamarin/java.interop/pull/1101).

|              Method |     Mean |     Error |    StdDev | Allocated |
|-------------------- |---------:|----------:|----------:|----------:|
| Before SetFinalText | 6.632 us | 0.0101 us | 0.0079 us |     112 B |
| Before SetText      | 6.672 us | 0.0116 us | 0.0103 us |     112 B |
| After SetFinalText  | 1.361 us | 0.0022 us | 0.0019 us |         - |
| After SetText       | 6.618 us | 0.0081 us | 0.0076 us |     112 B |

Note that `After SetText` is probably just *the same* and there is
some variance between runs.

The generate C# code before:

```csharp
public string? FinalText {
    get { return FinalTextFormatted == null ? null : FinalTextFormatted.ToString (); }
    set {
        var jls = value == null ? null : new global::Java.Lang.String (value);
        FinalTextFormatted = jls;
        if (jls != null) jls.Dispose ();
    }
}

public string? Text {
    get { return TextFormatted == null ? null : TextFormatted.ToString (); }
    set {
        var jls = value == null ? null : new global::Java.Lang.String (value);
        TextFormatted = jls;
        if (jls != null) jls.Dispose ();
    }
}
```

The generated C# after:

```csharp
public unsafe string? FinalText {
    get { return FinalTextFormatted == null ? null : FinalTextFormatted.ToString (); }
    set {
        const string __id = "setFinalText.(Ljava/lang/CharSequence;)V";
        global::Java.Interop.JniObjectReference native_p0 = global::Java.Interop.JniEnvironment.Strings.NewString (value);
        try {
            JniArgumentValue* __args = stackalloc JniArgumentValue [1];
            __args [0] = new JniArgumentValue (native_p0);
            _members.InstanceMethods.InvokeNonvirtualVoidMethod (__id, this, __args);
        } finally {
            global::Java.Interop.JniObjectReference.Dispose (ref native_p0);
        }
    }
}

public string? Text {
    get { return TextFormatted == null ? null : TextFormatted.ToString (); }
    set {
        var jls = value == null ? null : new global::Java.Lang.String (value);
        TextFormatted = jls;
        if (jls != null) jls.Dispose ();
    }
}
```

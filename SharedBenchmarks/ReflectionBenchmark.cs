using BenchmarkDotNet.Attributes;
using System;
using System.Reflection;

namespace SharedBenchmarks;

public class ReflectionBenchmark
{
	readonly ConstructorInfo ctor = typeof(MyType).GetConstructor(Type.EmptyTypes);
	readonly MethodInfo method = typeof(MyType).GetMethod("MyMethod");
	readonly MyType myObject = new();

	[Benchmark]
	public void ContructorInfo_Invoke() => ctor.Invoke(null);

    [Benchmark]
	public void MethodInfo_Invoke() => method.Invoke(myObject, null);

	class MyType
	{
        public void MyMethod() { }
    }
}

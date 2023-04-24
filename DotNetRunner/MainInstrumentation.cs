using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using System;
using System.Threading.Tasks;

namespace SharedBenchmarks
{
    [Instrumentation(Name = "com.dotnet.MainInstrumentation")]
    public class MainInstrumentation : Instrumentation
    {
        const string Tag = "DOTNET";

        protected MainInstrumentation(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer) { }

        public override void OnCreate(Bundle? arguments)
        {
            base.OnCreate(arguments);

            Start();
        }

        public async override void OnStart()
        {
            base.OnStart();

            var success = await Task.Factory.StartNew(Run);
            Log.Debug(Tag, $"Benchmark complete, success: {success}");
            Finish(success ? Result.Ok : Result.Canceled, new Bundle());
        }

        static bool Run()
        {
            bool success = false;
            try
            {
                var config = ManualConfig.CreateMinimumViable()
#if COLD_START
                    .AddJob(Job.Default.WithToolchain(new InProcessEmitToolchain(TimeSpan.FromMinutes(10), logOutput: true))
                        .WithIterationCount(1)
                        .WithStrategy(RunStrategy.ColdStart))
#else
                    .AddJob(Job.Default.WithToolchain(new InProcessEmitToolchain(TimeSpan.FromMinutes(10), logOutput: true)))
#endif
                    .AddDiagnoser(MemoryDiagnoser.Default)
                    .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical));
                config.UnionRule = ConfigUnionRule.AlwaysUseGlobal; // Overriding the default

                var types = new[]
                {
                    typeof (StringBenchmark),
                };
                foreach (var type in types)
                {
                    BenchmarkRunner.Run(type, config.WithOptions(ConfigOptions.DisableLogFile));
                }
                success = true;
            }
            catch (Exception ex)
            {
                Log.Error(Tag, $"Error: {ex}");
            }
            return success;
        }
    }
}
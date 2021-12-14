using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LiteDB;
using System;
using System.IO;

namespace SharedBenchmarks
{
	/// <summary>
	/// Based on: https://github.com/dotnet/maui/issues/3100
	/// </summary>
	[MemoryDiagnoser]
	[Orderer(SummaryOrderPolicy.FastestToSlowest)]
	public class LiteDbBenchmark
	{
		public class Customer
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public int Age { get; set; }
			public string[] Phones { get; set; }
			public bool IsActive { get; set; }
		}

		Customer customer = new Customer
		{
			Name = "John Doe",
			Phones = new string[] { "8000-0000", "9000-0000" },
			Age = 39,
			IsActive = true
		};
		string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "db.litedb");

		[Benchmark]
		public void EnsureIndex()
		{
			using (var db = new LiteDatabase(path))
			{
				var col = db.GetCollection<Customer>("customers");
				col.EnsureIndex(x => x.Name, true);
			}
		}
	}
}

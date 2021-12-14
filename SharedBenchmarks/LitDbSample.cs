using Newtonsoft.Json;
using System;
using System.IO;

namespace SharedBenchmarks
{
	public class LiteDbSample
	{
		LiteDB.LiteDatabase database;
		LiteDB.ILiteCollection<Pool> pools;

		public LiteDbSample()
		{
			var dbFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "db.litedb");

			using (var database = new LiteDB.LiteDatabase(dbFile))
			{
				pools = database.GetCollection<Pool>();
				pools.EnsureIndex(p => p.Timestamp);
				pools.EnsureIndex(p => p.Name);
			}
		}
	}

	public class Pool : BaseDocument
	{
		public Pool()
		{
		}

		public const string TYPE_NAME = "pool";

		[JsonProperty("type")]
		public override string Type => TYPE_NAME;

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("overview")]
		public Overview Overview { get; set; } = new Overview();

		[JsonProperty("contactName")]
		public string ContactName { get; set; }
		[JsonProperty("contactPhone")]
		public string ContactPhone { get; set; }
		[JsonProperty("contactEmail")]
		public string ContactEmail { get; set; }
		[JsonProperty("address")]
		public string Address { get; set; }
		[JsonProperty("lat")]
		public double? Latitude { get; set; }
		[JsonProperty("lon")]
		public double? Longitude { get; set; }
		[JsonProperty("notes")]
		public string Notes { get; set; }
		[JsonProperty("volume")]
		public long Volume { get; set; }

		[JsonProperty("fcTarget")]
		public double? FreeChlorineTarget { get; set; }
		[JsonProperty("fcSLAMTarget")]
		public double? FreeChlorineSLAMTarget { get; set; }
		[JsonProperty("cyaTarget")]
		public double? CyaTarget { get; set; }
		[JsonProperty("phTarget")]
		public double? PhTarget { get; set; }
		[JsonProperty("taTarget")]
		public double? TotalAlkalinityTarget { get; set; }
		[JsonProperty("chTarget")]
		public double? CalciumHardnessTarget { get; set; }
		[JsonProperty("saltMin")]
		public double? SaltMin { get; set; }
		[JsonProperty("saltMax")]
		public double? SaltMax { get; set; }
		[JsonProperty("saltTarget")]
		public double? SaltTarget { get; set; }
		[JsonProperty("borMin")]
		public double? BoratesMin { get; set; }
		[JsonProperty("borMax")]
		public double? BoratesMax { get; set; }
		[JsonProperty("borTarget")]
		public double? BoratesTarget { get; set; }

		[JsonProperty("cyaEntered")]
		public double? CyaEntered { get; set; }

		[JsonIgnore]
		public bool UseCyaEntered { get; set; } = false;

		[JsonIgnore]
		public double CsiMin { get { return -0.6d; } }

		[JsonIgnore]
		public double CsiMax { get { return 0.6d; } }

		[JsonProperty("trackSalt")]
		public bool TrackSalt { get; set; }
		[JsonProperty("trackBor")]
		public bool TrackBorates { get; set; }
		[JsonProperty("trackCC")]
		public bool TrackCombinedChlorine { get; set; }
		[JsonProperty("trackCSI")]
		public bool TrackCSI { get; set; }
		[JsonProperty("trackFlowRate")]
		public bool TrackFlowRate { get; set; }
		[JsonProperty("trackPressure")]
		public bool TrackPressure { get; set; }
		[JsonProperty("trackBackwashed")]
		public bool TrackBackwashed { get; set; }
		[JsonProperty("trackBrushed")]
		public bool TrackBrushed { get; set; }
		[JsonProperty("trackVacuumed")]
		public bool TrackVacuumed { get; set; }
		[JsonProperty("trackCleanedFilter")]
		public bool TrackCleanedFilter { get; set; }
		[JsonProperty("trackWaterTemp")]
		public bool TrackWaterTemp { get; set; }
		[JsonProperty("trackSWGCellPercent")]
		public bool TrackSWGCellPercent { get; set; }
		[JsonProperty("trackPumpRunTime")]
		public bool TrackPumpRunTime { get; set; }

		[JsonProperty("remindBackwashed")]
		public bool RemindBackwashed { get; set; }
		[JsonProperty("remindBackwashedDays")]
		public int RemindBackwashedDays { get; set; } = 7;
		[JsonProperty("remindBrushed")]
		public bool RemindBrushed { get; set; }
		[JsonProperty("remindBrushedDays")]
		public int RemindBrushedDays { get; set; } = 7;
		[JsonProperty("remindVacuumed")]
		public bool RemindVacuumed { get; set; }
		[JsonProperty("remindVacuumedDays")]
		public int RemindVacuumedDays { get; set; } = 7;
		[JsonProperty("remindCleanedFilter")]
		public bool RemindCleanedFilter { get; set; }
		[JsonProperty("remindCleanedFilterDays")]
		public int RemindCleanedFilterDays { get; set; } = 14;



		[JsonProperty("logWeather")]
		public bool LogWeather { get; set; } = true;
		[JsonProperty("weatherLocId")]
		public string WeatherLocationId { get; set; }

		[JsonProperty("zip")]
		public string Zip { get; set; }


		[JsonProperty("alwaysShowPoolVolume")]
		public bool AlwaysShowPoolVolume { get; set; } = false;

		[JsonProperty("hideIdealRangeAlerts")]
		public bool HideIdealRangeAlerts { get; set; } = false;

		[JsonProperty("hideRecommendedRangeAlerts")]
		public bool HideRecommendedRangeAlerts { get; set; } = false;

		[JsonProperty("shareDataOptOut")]
		public bool ShareDataOptOut { get; set; } = false;

		[JsonProperty("shareWithCode")]
		public bool ShareWithCode { get; set; }

		[JsonProperty("shareCode")]
		public string ShareCode { get; set; }

		[JsonProperty("shareWithTfp")]
		public bool ShareWithTfp { get; set; }

		[JsonProperty("swgLbsPerDay")]
		public double? SwgLbsPerDay { get; set; }

		[JsonProperty("swgModelId")]
		public string SwgModelId { get; set; }

		[JsonProperty("overrideFCTarget")]
		public double? OverrideFCTarget { get; set; }




		public override string ToString()
		{
			return string.Format("[PoolConfig: Id={0}]", Id);
		}


		public bool IsVacuumingOverdue()
		{
			return this.RemindVacuumed
					   && (DateTime.UtcNow - (this.Overview?.LastVacuumed ?? DateTime.MinValue)).TotalDays
					   > this.RemindVacuumedDays;
		}

		public bool IsBrushingOverdue()
		{
			return this.RemindBrushed
					   && (DateTime.UtcNow - (this.Overview?.LastBrushed ?? DateTime.MinValue)).TotalDays
					   > this.RemindBrushedDays;
		}

		public bool IsBackwashingOverdue()
		{
			return this.RemindBackwashed
					   && (DateTime.UtcNow - (this.Overview?.LastBackwashed ?? DateTime.MinValue)).TotalDays
					   > this.RemindBackwashedDays;
		}

		public bool IsCleanFilterOverdue()
		{
			return this.RemindCleanedFilter
					   && (DateTime.UtcNow - (this.Overview?.LastCleanedFilter ?? DateTime.MinValue)).TotalDays
					   > this.RemindCleanedFilterDays;
		}
	}

	public class Overview
	{
		[JsonProperty("fc")]
		public double? FreeChlorine { get; set; }
		[JsonProperty("cc")]
		public double? CombinedChlorine { get; set; }
		[JsonProperty("cya")]
		public double? Cya { get; set; }
		[JsonProperty("ch")]
		public double? CalciumHardness { get; set; }
		[JsonProperty("ph")]
		public double? Ph { get; set; }
		[JsonProperty("ta")]
		public double? TotalAlkalinity { get; set; }
		[JsonProperty("salt")]
		public double? Salt { get; set; }
		[JsonProperty("bor")]
		public double? Borates { get; set; }
		[JsonProperty("tds")]
		public double? TDS { get; set; }
		[JsonProperty("csi")]
		public double? CSI { get; set; }
		[JsonProperty("flowRate")]
		public double? FlowRate { get; set; }
		[JsonProperty("pressure")]
		public double? Pressure { get; set; }
		[JsonProperty("waterTemp")]
		public double? WaterTemp { get; set; }
		[JsonProperty("pumpRunTime")]
		public int? PumpRunTime { get; set; }
		[JsonProperty("swgCellPercent")]
		public int? SWGCellPercent { get; set; }

		[JsonProperty("fcTs")]
		public DateTime? LastFC { get; set; }
		[JsonProperty("fcLogId")]
		public string LastLogIdFC { get; set; }

		[JsonProperty("ccTs")]
		public DateTime? LastCC { get; set; }
		[JsonProperty("ccLogId")]
		public string LastLogIdCC { get; set; }

		[JsonProperty("cyaTs")]
		public DateTime? LastCYA { get; set; }
		[JsonProperty("cyaLogId")]
		public string LastLogIdCYA { get; set; }

		[JsonProperty("chTs")]
		public DateTime? LastCH { get; set; }
		[JsonProperty("chLogId")]
		public string LastLogIdCH { get; set; }

		[JsonProperty("phTs")]
		public DateTime? LastPH { get; set; }
		[JsonProperty("phLogId")]
		public string LastLogIdPH { get; set; }

		[JsonProperty("taTs")]
		public DateTime? LastTA { get; set; }
		[JsonProperty("taLogId")]
		public string LastLogIdTA { get; set; }

		[JsonProperty("saltTs")]
		public DateTime? LastSalt { get; set; }
		[JsonProperty("saltLogId")]
		public string LastLogIdSalt { get; set; }

		[JsonProperty("borTs")]
		public DateTime? LastBorates { get; set; }
		[JsonProperty("borLogId")]
		public string LastLogIdBorates { get; set; }

		[JsonProperty("tdsTs")]
		public DateTime? LastTDS { get; set; }
		[JsonProperty("tdsLogId")]
		public string LastLogIdTDS { get; set; }

		[JsonProperty("csiTs")]
		public DateTime? LastCSI { get; set; }
		[JsonProperty("csiLogId")]
		public string LastLogIdCSI { get; set; }

		[JsonProperty("flowRateTs")]
		public DateTime? LastFlowRate { get; set; }
		[JsonProperty("flowRateLogId")]
		public string LastLogIdFlowRate { get; set; }

		[JsonProperty("pressureTs")]
		public DateTime? LastPressure { get; set; }
		[JsonProperty("pressureLogId")]
		public string LastLogIdPressure { get; set; }

		[JsonProperty("waterTempTs")]
		public DateTime? LastWaterTemp { get; set; }
		[JsonProperty("waterTempLogId")]
		public string LastLogIdWaterTemp { get; set; }

		[JsonProperty("pumpRuntimeTs")]
		public DateTime? LastPumpRuntime { get; set; }
		[JsonProperty("pumpRuntimeLogId")]
		public string LastLogIdPumpRuntime { get; set; }

		[JsonProperty("swgCellPercentTs")]
		public DateTime? LastSWGCellPercent { get; set; }
		[JsonProperty("swgCellPercentLogId")]
		public string LastLogIdSWGCellPercent { get; set; }

		[JsonProperty("backwashedTs")]
		public DateTime? LastBackwashed { get; set; }
		[JsonProperty("backwashedLogId")]
		public string LastLogIdBackwashed { get; set; }

		[JsonProperty("brushedTs")]
		public DateTime? LastBrushed { get; set; }
		[JsonProperty("brushedLogId")]
		public string LastLogIdBrushed { get; set; }

		[JsonProperty("cleanedFilterTs")]
		public DateTime? LastCleanedFilter { get; set; }
		[JsonProperty("cleanedFilterLogId")]
		public string LastLogIdCleanedFilter { get; set; }

		[JsonProperty("vacuumedTs")]
		public DateTime? LastVacuumed { get; set; }
		[JsonProperty("vacuumedLogId")]
		public string LastLogIdVacuumed { get; set; }


		[JsonProperty("openedTs")]
		public DateTime? LastOpened { get; set; }
		[JsonProperty("openedLogId")]
		public string LastLogIdOpened { get; set; }

		[JsonProperty("closedTs")]
		public DateTime? LastClosed { get; set; }
		[JsonProperty("closedLogId")]
		public string LastLogIdClosed { get; set; }
	}

	public abstract class BaseDocument : BaseModel
	{
		[JsonProperty("type")]
		public abstract string Type { get; }

		[JsonProperty("userId")]
		public string UserId { get; set; }

		[JsonProperty("origin")]
		public string Origin { get; set; }
	}


	public abstract class BaseModel
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("_ts")]
		[JsonConverter(typeof(UnixEpochDateTimeConverter))]
		public DateTime Timestamp { get; set; }

		[JsonProperty("deleted")]
		public bool Deleted { get; set; }

		public override bool Equals(object obj)
		{
			var castObj = obj as BaseModel;

			if (castObj == null || string.IsNullOrEmpty(castObj.Id) || string.IsNullOrEmpty(Id))
				return false;

			return Id.Equals(castObj.Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

	}

	public class UnixEpochDateTimeConverter : Newtonsoft.Json.Converters.DateTimeConverterBase
	{
		static readonly DateTime epochDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Integer)
				throw new Exception("Invalid Token");

			var s = (long)reader.Value;
			return epochDate + TimeSpan.FromSeconds(s);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var d = ((DateTime)value) - epochDate;

			writer.WriteValue((long)d.TotalSeconds);
		}
	}
}

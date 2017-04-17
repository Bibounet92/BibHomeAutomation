using System;
using System.Linq;
namespace BibHomeAutomationNavigation.Netatmo
{
	public enum Scale
	{
		Max,
		ThirtyMinutes,
		OneHour,
		ThreeHours,
		OneDay,
		OneWeek,
		OneMonth
	}

	public static class ScaleExtensions
	{
		public static string GetScaleName(this Scale scale)
		{
			switch (scale)
			{
				case Scale.Max:
					return "max";
				case Scale.OneDay:
					return "30min";
				case Scale.OneHour:
					return "1hour";
				case Scale.OneMonth:
					return "3hours";
				case Scale.OneWeek:
					return "1week";
				case Scale.ThirtyMinutes:
					return "30min";
				case Scale.ThreeHours:
					return "3hours";
				default:
					throw new ArgumentOutOfRangeException(nameof(scale), scale, null);
			}
		}
	}

	public enum NetatmoScope
	{
		read_station,
		read_thermostat,
		write_thermostat,
		read_camera,
		access_camera
	}

	public static class NetatmoScopeExtensions
	{
		public static string ToScopeString(this NetatmoScope[] scopes)
		{
			var scopeString = scopes.Aggregate("", (current, netatmoScope) => string.IsNullOrEmpty(current) ? current + $"{netatmoScope}" : current + $" {netatmoScope}");
			return scopeString;
		}
	}

	public enum MeasurementType
	{
		Temperature,
		Co2,
		Humidity,
		Pressure,
		Noise,
		min_temp,
		date_min_temp,
		max_temp,
		date_max_temp,
		min_hum,
		date_min_hum,
		max_hum,
		date_max_hum,
		min_pressure,
		date_min_pressure,
		max_pressure,
		date_max_pressure,
		min_noise,
		date_min_noise,
		max_noise,
		date_max_noise,
		date_min_co2,
		date_max_co2,
		sum_rain,
		WindStrength,
		WindAngle,
		GustStrength,
		GustAngle,
		date_max_gust
	}

	public static class MeasurementTypeExtensions
	{
		public static string ToMeasurementTypesString(this MeasurementType[] types)
		{
			return types.Aggregate("", (current, measurementType) => current + (string.IsNullOrEmpty(current) ? measurementType.ToString() : $",{measurementType}"));
		}
	}
}

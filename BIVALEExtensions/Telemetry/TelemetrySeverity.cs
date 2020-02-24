namespace BIVALEExtensions.Telemetry
{
	/// <summary>
	/// Severity level for app insights used as an abstraction to keep calling code tidy.
	/// </summary>
	public enum TelemetrySeverity
	{
		Verbose = 0,

		Information = 1,

		Warning = 2,

		Error = 3,

		Critical = 4,
	}
}

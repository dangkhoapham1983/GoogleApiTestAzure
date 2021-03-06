﻿using Microsoft.Azure;
using System.Configuration;

namespace BIVALE.Extensions
{
	public static class Configuration
	{
		// NOTE: for settings that may need to be modified after deployment, we use CloudConfigurationManager. 
		public static string SubscriptionKey = CloudConfigurationManager.GetSetting("IMGroupAPI.SubscriptionKey");
		public static string UserAgent = ConfigurationManager.AppSettings["Telemetry.Reference"];
		public static string BaseUri = CloudConfigurationManager.GetSetting("IMGroupAPI.BaseUri");
	}
}

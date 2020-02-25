using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIVALE.Simulator.Serivces
{
	public class GoogleOauthTokenService
	{
		public static string OauthToken { get; set; }
		public static string ExpiredTime { get; set; }
	}
}
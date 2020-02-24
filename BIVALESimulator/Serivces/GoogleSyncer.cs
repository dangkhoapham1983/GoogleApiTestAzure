using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using static Google.Apis.Auth.OAuth2.Web.AuthorizationCodeWebApp;

namespace BIVALESimulator.Serivces
{
	public class GoogleSyncer
	{
		public static string GetOauthTokenUri(Controller controller)
		{
			var authResult = GetAuthResult(controller);
			return authResult.RedirectUri;
		}

		public static bool SyncToGoogle(Controller controller)
		{
			try
			{
				var authResult = GetAuthResult(controller);
				if (!authResult.Credential.Token.IsExpired(Google.Apis.Util.SystemClock.Default))
				{
					Console.WriteLine("The access token is OK, continue");
				}
				else
				{
					Console.WriteLine("The access token has expired but we can't refresh it :(");
				}

				return true;
			}
			catch (Exception ex)
			{
				GoogleOauthTokenService.OauthToken = null;
				return false;
			}
		}

		private static AuthResult GetAuthResult(Controller controller)
		{
			var dataStore = new DataStore();
			var clientID = WebConfigurationManager.AppSettings["GoogleClientID"];
			var clientSecret = WebConfigurationManager.AppSettings["GoogleClientSecret"];
			var appFlowMetaData = new GoogleAppFlowMetaData(dataStore, clientID, clientSecret);
			var factory = new AuthorizationCodeMvcAppFactory(appFlowMetaData, controller);
			var cancellationToken = new CancellationToken();
			var authCodeMvcApp = factory.Create();
			var authResultTask = authCodeMvcApp.AuthorizeAsync(cancellationToken);
			authResultTask.Wait();
			var result = authResultTask.Result;
			return result;
		}
	}
}
using BIVALESimulator.Serivces;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using BIVALESimulator.Models;

namespace BIVALESimulator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ReturnURL(string Email, string FirstName, string LastName, string GoogleID, string ProfileURL)
        {
			//Do your code for Signin or Signup
			var data = new UserProfile();
			data.Email = Email;
			data.FirstName = FirstName;
			data.LastName = LastName;
			data.GoogleID = GoogleID;
			data.ProfileURL = ProfileURL;

			//string[] scopes = new string[] { "https://mail.google.com/", "https://www.googleapis.com/auth/userinfo.profile", "https://www.googleapis.com/auth/userinfo.email" };

			//UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
			//new ClientSecrets
			//{
			//	ClientId = WebConfigurationManager.AppSettings["GoogleClientID"],
			//	ClientSecret = WebConfigurationManager.AppSettings["GoogleClientSecret"],
			//},
			// scopes,
			// "user",
			// CancellationToken.None).Result;

			//if (credential.Token.IsExpired(credential.Flow.Clock))
			//{
			//	Console.WriteLine("The access token has expired, refreshing it");
			//	if (credential.RefreshTokenAsync(CancellationToken.None).Result)
			//	{
			//		Console.WriteLine("The access token is now refreshed");
			//	}
			//	else
			//	{
			//		Console.WriteLine("The access token has expired but we can't refresh it :(");
			//	}
			//}
			//else
			//{
			//	Console.WriteLine("The access token is OK, continue");
			//}

			return Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SyncToGoogle()
		{
			if (string.IsNullOrWhiteSpace(GoogleOauthTokenService.OauthToken))
			{
				var redirectUri = GoogleSyncer.GetOauthTokenUri(this);
				return Redirect(redirectUri);
			}
			else
			{
				var success = GoogleSyncer.SyncToGoogle(this);
				if (!success)
				{
					return Json("Token was revoked. Try again.");
				}
			}
			return Redirect("~/");
		}
	}
}
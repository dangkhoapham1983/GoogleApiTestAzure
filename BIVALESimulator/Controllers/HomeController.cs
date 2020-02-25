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
        public JsonResult ReturnURL(UserProfile data)
        {
			//Do your code for Signin or Signup
			data.ExpiredTime = DateTime.Now.AddSeconds(data.ExpiredIn);
			if (IsExpired(data.ExpiredTime)){
				return Json("Expired", JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);
			}
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

		private bool IsExpired(DateTime obj)
		{
			if (obj < DateTime.Now)
			{
				return true;
			}
			return false;
		}
	}
}
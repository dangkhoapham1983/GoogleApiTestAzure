using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
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

			return Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);
        }

    }
}
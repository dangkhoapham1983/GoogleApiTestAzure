using BIVALE.Simulator.Serivces;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using BIVALE.Simulator.Models;
using BIVALE.Extensions.Services;
using BIVALE.DTO;
using BIVALE.Simulator.Helpers;

namespace BIVALE.Simulator.Controllers
{
    public class HomeController : Controller
    {
		private const string GRID_PARTIAL_PATH = "~/Views/Home/_IndexGrid.cshtml";

		private IGridMvcHelper gridMvcHelper;

		public HomeController()
		{
			this.gridMvcHelper = new GridMvcHelper();
		}

		public ActionResult Index()
        {
			var test = this.Request;
			return View(new List<UserDTO>());
        }

		[HttpPost]
        public ActionResult ReturnURL(UserProfile data)
        {
			return Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);

			//Do your code for Signin or Signup
			//data.ExpiredTime = DateTime.Now.AddSeconds(data.ExpiredIn);

			//if (IsExpired(data.ExpiredTime))
			//{
			//	return Json("Expired", JsonRequestBehavior.AllowGet);
			//}
			//else
			//{
			//	var obj = new SocialNetworkServices();
			//	List<UserDTO> result = obj.GetUserDTO();
			//	//return PartialView(result);
			//	return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);


			//	//var obj = new SocialNetworkServices();
			//	//var result = obj.GetUserDTO().AsQueryable().OrderBy(f => f.FirstName);
			//	//var grid = this.gridMvcHelper.GetAjaxGrid(result);

			//	//return PartialView(GRID_PARTIAL_PATH, grid);
			//}
		}

		[ChildActionOnly]
		public ActionResult GetGrid()
		{
			var obj = new SocialNetworkServices();
			var result = obj.GetUserDTO().AsQueryable().OrderBy(f => f.MAIL_ADDRESS);
			var grid = this.gridMvcHelper.GetAjaxGrid(result);

			return PartialView(GRID_PARTIAL_PATH, grid);
		}

		[HttpGet]
		public ActionResult GridPager(int? page)
		{
			var obj = new SocialNetworkServices();
			var result = obj.GetUserDTO().AsQueryable().OrderBy(f => f.MAIL_ADDRESS);
			var grid = this.gridMvcHelper.GetAjaxGrid(result, page);
			object jsonData = this.gridMvcHelper.GetGridJsonData(grid, GRID_PARTIAL_PATH, this);

			return Json(jsonData, JsonRequestBehavior.AllowGet);
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
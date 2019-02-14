using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StateManagement.Models;

namespace StateManagement.Controllers
{
	public class HomeController : Controller
	{
		public List<User> Logins = new List<User>();

		public ActionResult Index()
		{
			ViewBag.CurrentUser = Session["CurrentUser"];
			return View();
		}



		public ActionResult Contact()
		{
			ViewBag.CurrentUser = Session["CurrentUser"];
			return View();
		}
		public ActionResult RegisterUser()
		{
			return View();
		}
		public ActionResult UserDetails(User u)
		{
			if (Session["CurrentUser"] != null)
			{
				Logins.Add(u);
				u = (User)Session["CurrentUser"];
				Session["CurrentUser"] = u;
				ViewBag.CurrentUser = u;
				
				return View();
			}
			else
			{
				Logins.Add(u);
				ViewBag.CurrentUser = u;
				Session["CurrentUser"] = u;

				return RedirectToAction("Index");
				
			}
		}

		public ActionResult LogOut()
		{
			Session.Remove("CurrentUser");
			return RedirectToAction("Index");
		}
		public ActionResult Login()
		{
			
			return View();
		}
	}
}
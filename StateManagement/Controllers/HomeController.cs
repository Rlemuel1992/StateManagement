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

		public List<Products> ItemsList = new List<Products>
		{
			new Products("Hot Chocolate", "Milk, Cocoa, Sugar, Fat", 1.99, 1),
			new Products("Latte", "Milk, Coffee", 1.99, 1),
			new Products("Coffee", "Coffee, Water", 1.00, 1),
			new Products("Tea",  "Black Tea", 1.00, 1),
			new Products("Frozen Lemonade",  "Lemon, Sugar, Ice", 1.99, 1),

		};
		public List<Products> ShoppingCart = new List<Products>();



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
				u = (User)Session["CurrentUser"];
				ViewBag.CurrentUser = u;

				return View();
			}
			else
			{
				var LoginsList = (Session["LoginsList"] as List<User>) ?? new List<User>();
				LoginsList.Add(u);
				ViewBag.CurrentUser = u;
				Session["CurrentUser"] = u;
				Session["LoginsList"] = LoginsList;
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
		public ActionResult ValidateLogin(User TryLogin)
		{
			var LoginsList = (Session["LoginsList"] as List<User>) ?? new List<User>();

			if (Session["CurrentUser"] == null)
			{

				var tempUser = LoginsList.Find(u => u.UserName == TryLogin.UserName);
				if (tempUser != null)
				{
					if (tempUser.Password == TryLogin.Password)
					{
						Session["CurrentUser"] = tempUser;
						ViewBag.UserName = TryLogin.UserName;
						return RedirectToAction("UserDetails");
					}

					else if (tempUser.Password != TryLogin.Password)
					{
						ViewBag.Error = "Wrong Password";
						return View("Error");
					}

				}
				else
				{
					ViewBag.Error = "Wrong Username!";
					return View("Error");
				}
			}

			ViewBag.Error = "Already logged in!!";
			return View("Error");

		}

		public ActionResult Error()
		{

			return View();
		}


		public ActionResult ListItems()
		{
			
			ViewBag.CurrentUser = Session["CurrentUser"];
			ViewBag.ItemsList = ItemsList;
			return View();
		}
		public ActionResult AddItem(int quantity, string itemName)
		{
			if (Session["ShoppingCart"] != null)
			{
				ShoppingCart = (List<Products>)Session["ShoppingCart"];
			}
			foreach (Products item in ItemsList)
			{
				if (item.ItemName == itemName)
				{
					for (int i = 0; i < quantity; i++)
					{
						ShoppingCart.Add(item);
					}

				}
			}


			Session["ShoppingCart"] = ShoppingCart;
			return RedirectToAction("ListItems");
		}

		public ActionResult CheckOut()
		{
			ShoppingCart = (List<Products>)Session["ShoppingCart"];
			ViewBag.ShoppingList = ShoppingCart;
			ViewBag.CurrentUser = Session["CurrentUser"];
			double SubTotal = 0;
			double TaxRate = 1.06;
			double GrandTotal = 0;
			foreach (var item in ShoppingCart)
			{
				SubTotal += item.Price;
			}
			GrandTotal = SubTotal * TaxRate;
			ViewBag.GrandTotal = Math.Round(GrandTotal, 2);
			return View();
		}
		public ActionResult RemoveItem(string id)
		{
			ShoppingCart = (List<Products>)Session["ShoppingCart"];
			int index = IsExist(id);
			ShoppingCart.RemoveAt(index);
			Session["ShoppingCart"] = ShoppingCart;
			return RedirectToAction("CheckOut");
		}
		public ActionResult RemoveAll()
		{
			Session["ShoppingCart"] = null;
			return RedirectToAction("ListItems");
		}
		private int IsExist(string id)
		{
			ShoppingCart = (List<Products>)Session["ShoppingCart"];
			for (int i = 0; i < ShoppingCart.Count; i++)
				if (ShoppingCart[i].ItemName.Equals(id))
					return i;
			return -1;
		}

		public ActionResult PotionSeller()
		{
			return View();
		}
	}
}
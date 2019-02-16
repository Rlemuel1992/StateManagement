using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateManagement.Models
{
	public class Products
	{
		public string ItemName { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }

		public Products(string ItemName, string Description, double Price)
		{
			this.ItemName = ItemName;
			this.Description = Description;
			this.Price = Price;
		}
		public Products() { }
	}
}
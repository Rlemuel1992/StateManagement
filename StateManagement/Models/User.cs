using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateManagement.Models
{
	public class User
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email {get; set;}
		public int Age { get; set; }

		public User(string UserName, string Email, string Password, int Age)
		{
			this.UserName = UserName;
			this.Email = Email;
			this.Password = Password;
			this.Age = Age;
		}

		public User() { }
	}
}
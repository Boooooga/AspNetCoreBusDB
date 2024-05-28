using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class User
	{
		public int Id { get; set; }
		public bool IsBlocked { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public DateTime RegistrationDate { get; set; }
		public UserRole Role { get; set; }

		public User(int id, bool isBlocked, string login, string password, DateTime registrationDate, UserRole role)
		{
			Id = id;
			IsBlocked = isBlocked;
			Login = login;
			Password = password;
			RegistrationDate = registrationDate;
			Role = role;
		}
	}
}

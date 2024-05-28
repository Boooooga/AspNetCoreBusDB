using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Ckassa.Attributes;

namespace Tools.Finances.Ckassa.Requests
{
	/// <summary>
	/// Запрос на регистрацию пользователя.
	/// </summary>
	public class RegisterUserRequest
	{
		/// <summary>
		/// Телефон.
		/// </summary>
		[SignOrder(0)]
		[JsonProperty("login")]
		public string Phone { get; set; }

		/// <summary>
		/// E-Mail.
		/// </summary>
		[SignOrder(1)]
		public string Email { get; set; }

		/// <summary>
		/// Имя.
		/// </summary>
		[SignOrder(2)]
		public string Name { get; set; }

		/// <summary>
		/// Фамилия.
		/// </summary>
		[SignOrder(3)]
		[JsonProperty("surName")]
		public string Surname { get; set; }

		/// <summary>
		/// Отчество.
		/// </summary>
		[SignOrder(4)]
		public string MiddleName { get; set; }

		public RegisterUserRequest(string phone)
		{
			Phone = phone;
		}

		public RegisterUserRequest(string phone, string email, string name, string surname, string middleName)
		{
			Phone = phone;
			Email = email;
			Name = name;
			Surname = surname;
			MiddleName = middleName;
		}
	}
}

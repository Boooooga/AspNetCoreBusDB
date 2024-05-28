using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Enums;

namespace Tools.Finances.Ckassa.Responses
{
	/// <summary>
	/// Ответ на запрос получения информации о пользователе.
	/// </summary>
	public class GetUserInfoResponse
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

		/// <summary>
		/// Статус.
		/// </summary>
		[SignOrder(5)]
		[JsonProperty("state")]
		public UserState UserState { get; set; }

		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		[SignOrder(6)]
		public string UserToken { get; set; }
	}
}

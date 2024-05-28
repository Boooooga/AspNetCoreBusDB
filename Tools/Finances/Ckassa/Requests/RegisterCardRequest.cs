using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Enums;

namespace Tools.Finances.Ckassa.Requests
{
	/// <summary>
	/// Запрос на добавление карты.
	/// </summary>
	public class RegisterCardRequest
	{
		/// <summary>
		/// Тип формы, отображаемой при регистрации карты.
		/// </summary>
		[SignOrder(0)]
		[JsonProperty("clientType")]
		public CardRegistrationFormType FormType { get; set; }

		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		[SignOrder(1)]
		public string UserToken { get; set; }

		public RegisterCardRequest(CardRegistrationFormType formType, string userToken)
		{
			FormType = formType;
			UserToken = userToken;
		}
	}
}

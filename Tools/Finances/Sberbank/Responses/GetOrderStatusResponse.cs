using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Sberbank.Enums;

namespace Tools.Finances.Sberbank.Responses
{
	/// <summary>
	/// Класс, описывающий ответ на запрос информации о заказе (<see cref="Requests.GetOrderStatusRequest"/>).
	/// </summary>
	/// <inheritdoc />
	public class GetOrderStatusResponse : DefaultBaseResponse
	{
		/// <summary>
		/// Cостояние заказа в платежной системе.
		/// </summary>
		public OrderStatus? OrderStatus { get; set; }

		/// <summary>
		/// Идентификатор заказа в системе магазина.
		/// </summary>
		public int OrderNumber { get; set; }

		/// <summary>
		/// Сумма платежа в копейках (центах).
		/// </summary>
		public long Amount { get; set; }

		/// <summary>
		/// Маскированный номер карты, которая использовалась для оплаты. Указан только после оплаты заказа.
		/// </summary>
		public string Pan { get; set; }

		/// <summary>
		/// Срок истечения действия карты в формате YYYYMM. Указан только после оплаты заказа.
		/// </summary>
		[JsonProperty("expiration")]
		public string CardExpirationDate { get; set; }

		/// <summary>
		/// Имя держателя карты. Указано только после оплаты заказа.
		/// </summary>
		public string CardholderName { get; set; }

		/// <summary>
		/// Валюта платежа.
		/// </summary>
		public Currency? Currency { get; set; }

		/// <summary>
		/// Код авторизации МПС. Поле фиксированной длины (6 символов), может содержать цифры и латинские буквы.
		/// </summary>
		public string ApprovalCode { get; set; }

		/// <summary>
		/// IP-адрес пользователя, который оплачивал заказ.
		/// </summary>
		public string Ip { get; set; }
	}
}

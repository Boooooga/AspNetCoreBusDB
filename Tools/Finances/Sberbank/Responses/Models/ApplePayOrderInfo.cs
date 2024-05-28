using System;
using System.Collections.Generic;
using System.Text;
using Tools.Finances.Sberbank.Enums;

namespace Tools.Finances.Sberbank.Responses.Models
{
	/// <summary>
	/// Класс, содержащий подробную информацию о заказе, оплаченном через Apple Pay.
	/// </summary>
	//TODO: Добавить всю информацию, возвращаемую платежным шлюзом.
	public class ApplePayOrderInfo
	{
		/// <summary>
		/// Код ответа процессинга.
		/// </summary>
		public int ActionCode { get; set; }

		/// <summary>
		/// Расшифровка кода ответа процессинга (<see cref="ActionCode"/>).
		/// </summary>
		public string ActionCodeDescription { get; set; }

		/// <summary>
		/// Сумма платежа в минимальных единицах валюты.
		/// </summary>
		public long Amount { get; set; }

		/// <summary>
		/// Валюта платежа.
		/// </summary>
		public Currency? Currency { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Tools.Finances.Sberbank.Enums;

namespace Tools.Finances.Sberbank.Requests
{
	/// <summary>
	/// Класс, описывающий запрос на отмену оплаты предавторизованного заказа.
	/// </summary>
	/// <inheritdoc />
	public class CancelOrderPaymentRequest : BaseRequest
	{
		/// <summary>
		/// Идентификатор заказа на стороне платежного шлюза.
		/// </summary>
		public string OrderId { get; set; }

		/// <summary>
		/// Используемый язык.
		/// </summary>
		public Language? Language { get; set; }
	}
}

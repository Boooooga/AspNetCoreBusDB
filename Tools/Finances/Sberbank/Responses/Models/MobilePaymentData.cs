using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Finances.Sberbank.Responses.Models
{
	/// <summary>
	/// Класс, содержащий данные заказа, оплаченного посредством системы мобильных платежей.
	/// </summary>
	public class MobilePaymentData
    {
		/// <summary>
		/// Идентификатор заказа на стороне платежного шлюза.
		/// </summary>
		public string OrderId { get; set; }
	}
}

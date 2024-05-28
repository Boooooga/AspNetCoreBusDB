using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Finances.Sberbank.Requests
{
	/// <summary>
	/// Класс, описывающий запрос на завершение оплаты предавторизованного заказа.
	/// </summary>
	/// <inheritdoc />
	public class CompleteOrderPaymentRequest : BaseRequest
    {
	    /// <summary>
	    /// Идентификатор заказа на стороне платежного шлюза.
	    /// </summary>
		public string OrderId { get; set; }

	    /// <summary>
	    /// Сумма платежа в копейках (центах).
	    /// </summary>
	    public long Amount { get; set; }
	}
}

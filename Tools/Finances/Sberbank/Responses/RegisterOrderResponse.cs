using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Finances.Sberbank.Responses
{
	/// <summary>
	/// Класс, описывающий ответ на запрос создания нового заказа (<see cref="Requests.RegisterOrderRequest"/>).
	/// </summary>
	/// <inheritdoc />
	public class RegisterOrderResponse : DefaultBaseResponse
	{
		/// <summary>
		/// Идентификатор заказа на стороне платежного шлюза.
		/// </summary>
		public string OrderId { get; set; }

		/// <summary>
		/// URL платежной формы, на который надо перенаправить браузер клиента.
		/// </summary>
		public string FormUrl { get; set; }
	}
}

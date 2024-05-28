using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Sberbank.Enums;

namespace Tools.Finances.Sberbank.Requests
{
	/// <summary>
	/// Класс, описывающий запрос на получение информации о заказе.
	/// </summary>
	/// <inheritdoc />
	public class GetOrderStatusRequest : BaseRequest
	{
		/// <summary>
		/// Идентификатор заказа в платежной системе.
		/// </summary>
		public string OrderId { get; set; }

		/// <summary>
		/// Используемый язык.
		/// </summary>
		public Language? Language { get; set; }
	}
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, содержащий информацию о покупателе.
	/// </summary>
	public class CustomerDetails
	{
		/// <summary>
		/// Электронная почта покупателя. Обязательна, если не указан телефон.
		/// </summary>
		[JsonProperty(PropertyName = "email")]
		public string EMail { get; set; }

		/// <summary>
		/// Номер телефона покупателя. Обязателен, если не указан адрес электронной почты.
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// Способ связи с покупателем.
		/// </summary>
		public string Contact { get; set; }

		/// <summary>
		/// Способ доставки.
		/// </summary>
		public DeliveryInfo DeliveryInfo { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Serialization;
using Tools.Serialization.Converters;

namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, содержащий дополнительную информацию о заказе и корзину товаров.
	/// </summary>
	public class OrderBundle
	{
		/// <summary>
		/// Дата создания заказа.
		/// </summary>
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime OrderCreationDate { get; set; }

		/// <summary>
		/// Данные о покупателе.
		/// </summary>
		public CustomerDetails CustomerDetails { get; set; }

		/// <summary>
		/// Товарные позиции корзины.
		/// </summary>
		public CartItems CartItems { get; set; }
	}
}

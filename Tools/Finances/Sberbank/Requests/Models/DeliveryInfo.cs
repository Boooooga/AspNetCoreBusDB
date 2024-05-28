using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Finances.Sberbank.Enums;

namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, описывающий способ доставки заказа.
	/// </summary>
	public class DeliveryInfo
	{
		/// <summary>
		/// Тип доставки.
		/// </summary>
		public string DeliveryType { get; set; }

		/// <summary>
		/// Страна доставки.
		/// </summary>
		public Country Country { get; set; } = Country.RU;

		/// <summary>
		/// Город доставки.
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Адрес доставки.
		/// </summary>
		public string PostAddress { get; set; }
	}
}

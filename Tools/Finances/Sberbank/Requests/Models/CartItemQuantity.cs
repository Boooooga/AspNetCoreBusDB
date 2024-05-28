using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Sberbank.Other;

namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, содержащий информацию о количестве товаров и мере измерения для товарной позиции.
	/// </summary>
	public class CartItemQuantity
	{
		/// <summary>
		/// Количество товаров.
		/// </summary>
		public decimal Value { get; set; }

		/// <summary>
		/// Единицы измерения.
		/// </summary>
		[JsonConverter(typeof(TrimStringConverter), 20)]
		public string Measure { get; set; }
	}
}

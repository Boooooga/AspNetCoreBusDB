using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, описывающий корзину товаров.
	/// </summary>
	public class CartItems
	{
		/// <summary>
		/// Товарные позиции корзины.
		/// </summary>
		public List<CartItem> Items { get; set; }
	}
}

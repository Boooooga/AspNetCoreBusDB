using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Finances.Sberbank.Requests.Models
{ 
	/// <summary>
	/// Класс, содержащий информацию о скидке на товарную позицию.
	/// </summary>
	public class CartItemDiscount
	{
		/// <summary>
		/// Тип скидки.
		/// </summary>
		public string DiscountType { get; set; }

		/// <summary>
		/// Значение скидки.
		/// </summary>
		public long DiscountValue { get; set; }
	}
}

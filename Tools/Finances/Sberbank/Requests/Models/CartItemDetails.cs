using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, содержащий дополнительную информацию о товарной позиции.
	/// </summary>
	public class CartItemDetails
	{
		/// <summary>
		/// Дополнительные параметры товарной позиции.
		/// </summary>
		[JsonProperty(PropertyName = "itemDetailsParams")]
		public List<CartItemDetailsParameter> Parameters { get; set; }
	}
}

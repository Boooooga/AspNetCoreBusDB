using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Models;

namespace Tools.Finances.Ckassa.Responses
{
	/// <summary>
	/// Ответ на запрос получения списка карт.
	/// </summary>
	public class GetCardsResponse
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		[SignOrder(0)]
		public string UserToken { get; set; }

		/// <summary>
		/// Идентификатор магазина.
		/// </summary>
		[SignOrder(1)]
		public string ShopToken { get; set; }

		/// <summary>
		/// Список карт.
		/// </summary>
		[SignOrder(2)]
		public List<Card> Cards { get; set; }
	}
}

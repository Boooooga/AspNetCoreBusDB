using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Enums;

namespace Tools.Finances.Ckassa.Responses
{
	/// <summary>
	/// Ответ на запрос деактивации карты.
	/// </summary>
	public class DeactivateCardResponse
	{   
		/// <summary>
		/// Результат исполнения запроса.
		/// </summary>
		[SignOrder(0)]
		[JsonProperty("resultState")]
		public OperationResult Result { get; set; }

		/// <summary>
		/// Пояснение результата.
		/// </summary>
		[SignOrder(1)]
		[JsonProperty("desc")]
		public string ResultDescription { get; set; }

		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		[SignOrder(2)]
		public string UserToken { get; set; }

		/// <summary>
		/// Идентификатор магазина.
		/// </summary>
		[SignOrder(3)]
		public string ShopToken { get; set; }
	}
}

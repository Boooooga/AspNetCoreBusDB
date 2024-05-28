using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Finances.Ckassa.Attributes;

namespace Tools.Finances.Ckassa.Requests
{
	/// <summary>
	/// Запрос на деактивацию карты.
	/// </summary>
	public class DeactivateCardRequest
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		[SignOrder(0)]
		public string UserToken { get; set; }

		/// <summary>
		/// Идентификатор карты.
		/// </summary>
		[SignOrder(1)]
		public string CardToken { get; set; }

		public DeactivateCardRequest(string userToken, string cardToken)
		{
			UserToken = userToken;
			CardToken = cardToken;
		}
	}
}

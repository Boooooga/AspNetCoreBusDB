using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Enums;

namespace Tools.Finances.Ckassa.Models
{
	/// <summary>
	/// Банковская карта пользователя.
	/// </summary>
	public class Card
	{
		/// <summary>
		/// Маскированный номер карты.
		/// </summary>
		[SignOrder(0)]
		[JsonProperty("cardMask")]
		public string MaskedPan { get; set; }

		/// <summary>
		/// Уникальный идентификатор карты.
		/// </summary>
		[SignOrder(1)]
		[JsonProperty("cardToken")]
		public string Token { get; set; }

		/// <summary>
		/// Платежная система.
		/// </summary>
		[SignOrder(2)]
		[JsonProperty("cardType")]
		public PaymentSystem PaymentSystem { get; set; }

		/// <summary>
		/// Статус карты.
		/// </summary>
		[SignOrder(3)]
		public CardState State { get; set; }
	}
}

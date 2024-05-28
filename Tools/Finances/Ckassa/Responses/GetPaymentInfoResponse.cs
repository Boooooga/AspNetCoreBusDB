using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Enums;

namespace Tools.Finances.Ckassa.Responses
{
	/// <summary>
	/// Ответ на запрос получения информации о платеже.
	/// </summary>
	public class GetPaymentInfoResponse
	{
		/// <summary>
		/// Статус платежа.
		/// </summary>
		[SignOrder(0)]
		public PaymentState State { get; set; }

		/// <summary>
		/// Общая сумма платежа (в копейках).
		/// </summary>
		[SignOrder(1)]
		public long TotalAmount { get; set; }

		/// <summary>
		/// Время создания платежа.
		/// </summary>
		[SignOrder(2)]
		[JsonProperty("createdDate")]
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Код провайдера услуг.
		/// </summary>
		[SignOrder(3)]
		[JsonProperty("providerServCode")]
		public string ProviderCode { get; set; }

		/// <summary>
		/// Название провайдера услуг.
		/// </summary>
		[SignOrder(4)]
		public string ProviderName { get; set; }

		/// <summary>
		/// Код ошибки проводки платежа.
		/// </summary>
		[SignOrder(5)]
		public int? ErrorCode { get; set; }

		/// <summary>
		/// Текст ошибки проводки платежа.
		/// </summary>
		[SignOrder(6)]
		[JsonProperty("error")]
		public string ErrorText { get; set; }

		/// <summary>
		/// Сообщение пользователю.
		/// </summary>
		[SignOrder(7)]
		[JsonProperty("message")]
		public string UserMessage { get; set; }

		/// <summary>
		/// Услуга оказана.
		/// </summary>
		[SignOrder(8)]
		[JsonProperty("provisionServices")]
		public bool ServiceProvided { get; set; }

		/// <summary>
		/// Время проводки платежа.
		/// </summary>
		[SignOrder(9)]
		[JsonProperty("procDate")]
		public DateTime? ProcessingTime { get; set; }
	}
}

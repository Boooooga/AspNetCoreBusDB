using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Tools.Finances.Sberbank.Enums;
using Tools.Finances.Sberbank.Serializers;

namespace Tools.Finances.Sberbank.Requests
{
	/// <summary>
	/// Базовый класс для всех классов, описывающих запросы на оплату через системы мобильных платежей.
	/// </summary>
	/// <inheritdoc />
	public class MobilePaymentBaseRequest : BaseRequest
	{
		/// <summary>
		/// Логин продавца в платёжном шлюзе.
		/// </summary>
		[JsonProperty(PropertyName = "merchant")]
		public string MerchantLogin { get; set; }

		/// <summary>
		/// Внутренний номер заказа в системе магазина.
		/// </summary>
		public string OrderNumber { get; set; }

		/// <summary>
		/// Описание заказа.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Используемый язык.
		/// </summary>
		public Language? Language { get; set; }

		/// <summary>
		/// Дополнительные параметры заказа, сохраняемые для просмотра из личного кабинета продавца.
		/// </summary>
		public Dictionary<string, string> AdditionalParameters { get; set; }

		/// <summary>
		/// Используется ли предварительная авторизация средств (двухстадийная оплата).
		/// </summary>
		[JsonProperty(PropertyName = "preAuth")]
		public bool? UsePreAuthorization { get; set; }

		/// <summary>
		/// Закодированные данные платежа, полученные из системы мобильных платежей.
		/// </summary>
		public string PaymentToken { get; set; }

		internal override RequestSerializer CreateSerializer()
		{
			return new MobilePaymentSerializer();
		}
	}
}

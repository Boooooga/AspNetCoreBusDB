using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Sberbank.Enums;
using Tools.Finances.Sberbank.Requests.Models;

namespace Tools.Finances.Sberbank.Requests
{
	/// <summary>
	/// Класс, описывающий запрос на создание нового заказа.
	/// </summary>
	/// <inheritdoc />
	//TODO: Добавить всю информацию, запрашиваемую платежным шлюзом.
	public class RegisterOrderRequest : BaseRequest
	{
		/// <summary>
		/// Внутренний номер заказа в системе магазина.
		/// </summary>
		public string OrderNumber { get; set; }

		/// <summary>
		/// Сумма платежа в минимальных единицах валюты.
		/// </summary>
		public long Amount { get; set; }

		/// <summary>
		/// Валюта платежа.
		/// </summary>
		public Currency? Currency { get; set; }

		/// <summary>
		/// Адрес, на который требуется перенаправить пользователя в случае успешной оплаты.
		/// </summary>
		public string ReturnUrl { get; set; }

		/// <summary>
		/// Адрес, на который требуется перенаправить пользователя в случае неуспешной оплаты.
		/// </summary>
		public string FailUrl { get; set; }

		/// <summary>
		/// Описание заказа.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Используемый язык.
		/// </summary>
		public Language? Language { get; set; }

		/// <summary>
		/// Тип страницы платежного интерфейса, отображаемой клиенту.
		/// </summary>
		public string PageView { get; set; }

		/// <summary>
		/// Продолжительность жизни заказа в секундах.
		/// </summary>
		[JsonProperty("sessionTimeoutSecs")]
		public int? SessionTimeout { get; set; }

		/// <summary>
		/// Корзина товаров.
		/// </summary>
		public OrderBundle OrderBundle { get; set; }

		/// <summary>
		/// Система налогообложения.
		/// </summary>
		public TaxSystem? TaxSystem { get; set; }
	}
}

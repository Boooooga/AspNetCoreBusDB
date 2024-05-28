using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Sberbank.Enums;
using Tools.Finances.Sberbank.Other;

namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, описывающий товарную позицию в корзине.
	/// </summary>
	public class CartItem
	{
		/// <summary>
		/// Уникальный идентификатор товарной позиции внутри корзины.
		/// </summary>
		public string PositionId { get; set; }

		/// <summary>
		/// Наименование или описание товарной позиции в свободной форме.
		/// </summary>
		[JsonConverter(typeof(TrimStringConverter), 100)]
		public string Name { get; set; }

		/// <summary>
		/// Сумма стоимости всех товарных позиций одного positionId в деньгах в минимальных единицах валюты.
		/// </summary>
		public long ItemAmount { get; set; }

		/// <summary>
		/// Валюта товарной позиции. Если не указана, считается равным валюте заказа.
		/// </summary>
		public Currency? ItemCurrency { get; set; }

		/// <summary>
		/// Номер (идентификатор) товарной позиции в системе магазина. Параметр должен быть уникальным в рамках запроса.
		/// </summary>
		public string ItemCode { get; set; }

		/// <summary>
		/// Cтоимость одной товарной позиции в минимальных единицах валюты.
		/// </summary>
		public long ItemPrice { get; set; }

		/// <summary>
		/// Налог
		/// </summary>
		public CartItemTax Tax { get; set; }

		/// <summary>
		/// Количество товаров в заказе, принадлежащих данной позиции.
		/// </summary>
		public CartItemQuantity Quantity { get; set; }

		/// <summary>
		/// Информация о скидке.
		/// </summary>
		public CartItemDiscount Discount { get; set; }

		/// <summary>
		/// Агентская комиссия за продажу товара.
		/// </summary>
		public CartItemAgentInterest AgentInterest { get; set; }

		/// <summary>
		/// Дополнительная информация о товарной позиции.
		/// </summary>
		public CartItemDetails ItemDetails { get; set; }
	}
}

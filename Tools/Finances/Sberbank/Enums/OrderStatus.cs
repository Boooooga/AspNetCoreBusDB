using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Finances.Sberbank.Enums
{
	/// <summary>
	/// Статусы заказа, возвращаемые платежным шлюзом.
	/// </summary>
	public enum OrderStatus
	{
		/// <summary>
		/// Заказ зарегистрирован, но не оплачен.
		/// </summary>
		Registered = 0,

		/// <summary>
		/// Предавторизованная сумма захолдирована.
		/// </summary>
		Hold = 1,

		/// <summary>
		/// Проведена полная авторизация суммы заказа.
		/// </summary>
		AuthorizationDone = 2,

		/// <summary>
		/// Авторизация отменена.
		/// </summary>
		AuthorizationCancelled = 3,

		/// <summary>
		/// По транзакции была проведена операция возврата.
		/// </summary>
		Refunded = 4,

		/// <summary>
		/// Инициирована авторизация через ACS банка-эмитента.
		/// </summary>
		AuthorizationInAcsStarted = 5,

		/// <summary>
		/// Авторизация отклонена.
		/// </summary>
		AuthorizationDeclined = 6,
	}
}

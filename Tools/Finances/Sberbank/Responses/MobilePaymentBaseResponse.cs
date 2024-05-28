using System;
using System.Collections.Generic;
using System.Text;
using Tools.Finances.Sberbank.Responses.Models;

namespace Tools.Finances.Sberbank.Responses
{
	/// <summary>
	/// Базовый класс для всех классов, описывающих ответы платежного шлюза на запросы регистрации оплаты через мобильные платежные системы.
	/// </summary>
	/// <inheritdoc />
	public abstract class MobilePaymentBaseResponse : BaseResponse
	{
		/// <summary>
		/// Успешно ли выполнен запрос.
		/// </summary>
		public bool Success { get; set; }

		/// <summary>
		/// Данные заказа.
		/// </summary>
		public MobilePaymentData Data { get; set; }

		/// <summary>
		/// Ошибка, возникшая при выполнении запроса.
		/// </summary>
		public ErrorInfo Error { get; set; }

		public override ErrorInfo GetErrorInfo()
		{
			return Error;
		}
	}
}

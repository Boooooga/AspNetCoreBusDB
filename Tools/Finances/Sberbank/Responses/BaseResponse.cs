using System;
using System.Collections.Generic;
using System.Text;
using Tools.Finances.Sberbank.Responses.Models;

namespace Tools.Finances.Sberbank.Responses
{
	/// <summary>
	/// Базовый класс для всех классов, описывающих ответы платежного шлюза.
	/// </summary>
	public abstract class BaseResponse
	{
		/// <summary>
		/// Возвращает информацию об ошибке, возникшей при выполнении запроса к платежному шлюзу
		/// </summary>
		/// <returns>Ошибка, возникшая при выполнении запроса к платежному шлюзу (null, если запрос исполнен успешно).</returns>
		public abstract ErrorInfo GetErrorInfo();

		/// <summary>
		/// Проверяет, успешно ли обработан запрос
		/// </summary>
		/// <returns>True, если обработка запроса прошла без ошибок, и False в противном случае.</returns>
		public virtual bool IsSuccessful()
		{
			var errorInfo = GetErrorInfo();
			return errorInfo == null || errorInfo.Code == 0;
		}
	}
}

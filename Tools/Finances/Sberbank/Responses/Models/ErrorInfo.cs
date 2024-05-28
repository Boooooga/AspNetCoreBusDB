using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Finances.Sberbank.Responses.Models
{
	/// <summary>
	/// Класс, описывающий ошибку, возникшую при выполнении запроса к платежному шлюзу.
	/// </summary>
	public class ErrorInfo
    {
	    /// <summary>
	    /// Код ошибки.
	    /// </summary>
		public int Code { get; set; }

		/// <summary>
		/// Подробное техническое описание ошибки.
		/// </summary>
		public string Description { get; set; }

	    /// <summary>
	    /// Описание ошибки для отображения пользователю.
	    /// </summary>
		public string Message { get; set; }
    }
}

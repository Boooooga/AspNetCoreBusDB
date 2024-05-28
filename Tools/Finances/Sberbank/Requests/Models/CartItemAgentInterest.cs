using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Finances.Sberbank.Requests.Models
{
	/// <summary>
	/// Класс, содержащий информацию об агентской комиссии за продажу товара.
	/// </summary>
	public class CartItemAgentInterest
	{
		/// <summary>
		/// Тип агентской комиссии.
		/// </summary>
		public string InterestType { get; set; }

		/// <summary>
		/// Значение агентской комиссии.
		/// </summary>
		public long InterestValue { get; set; }
	}
}

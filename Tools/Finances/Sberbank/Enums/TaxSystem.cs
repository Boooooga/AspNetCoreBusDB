namespace Tools.Finances.Sberbank.Enums
{
	/// <summary>
	/// Системы налогообложения.
	/// </summary>
	public enum TaxSystem
	{
		/// <summary>
		/// Общая система налогообложения.
		/// </summary>
		General = 0,

		/// <summary>
		/// Упрощенная система налогообложения, доходы.
		/// </summary>
		Simplified = 1,

		/// <summary>
		/// Упрощенная система налогообложения, доходы минус расходы.
		/// </summary>
		SimplifiedWithoutExpenses = 2,

		/// <summary>
		/// Единый налог на вмененный доход.
		/// </summary>
		SingleTaxOnImputedIncome = 3,

		/// <summary>
		/// Единый сельскохозяйственный налог.
		/// </summary>
		AgriculturalTax = 4,

		/// <summary>
		/// Патентная система налогообложения.
		/// </summary>
		Patent = 5,
	}
}
namespace Tools.Finances.Sberbank.Enums
{
	/// <summary>
	/// Типы налогообложения для товарной позиции.
	/// </summary>
	public enum TaxType
	{
		/// <summary>
		/// Без НДС.
		/// </summary>
		WithoutVat = 0,

		/// <summary>
		/// НДС 0%.
		/// </summary>
		Vat0 = 1,

		/// <summary>
		/// НДС 10%.
		/// </summary>
		Vat10 = 2,

		/// <summary>
		/// НДС 18%.
		/// </summary>
		Vat18 = 3,

		/// <summary>
		/// НДС 10/110.
		/// </summary>
		Vat10110 = 4,

		/// <summary>
		/// НДС 18/118.
		/// </summary>
		Vat18118 = 5,
	}
}
namespace Tools.Finances.Ckassa.Enums
{
	/// <summary>
	/// Статус карты.
	/// </summary>
	public enum CardState
	{
		/// <summary>
		/// Доступна для оплаты.
		/// </summary>
		Active,

		/// <summary>
		/// Недоступна для оплаты, требуется активация.
		/// </summary>
		Inactive,

		/// <summary>
		/// Недоступна для оплаты и активации.
		/// </summary>
		Deleted
	}
}
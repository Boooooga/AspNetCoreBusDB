namespace Tools.Misc.AppStore.Enums
{
	public enum ReceiptStatus
	{
		Valid = 0,
		InvalidHttpMethod = 21000,
		MalformedReceipt = 21002,
		AuthenticationFailed = 21003,
		InvalidSecret = 21004,
		ServerNotAvailable = 21005,
		SubscriptionExpired = 21006,
		NonSandboxEnvironment = 21007,
		NonProductionEnvironment = 21008,
		InternalError = 21009,
		UserNotFound = 21010
	}
}
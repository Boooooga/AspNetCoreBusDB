namespace Common.Configuration.Mail
{
	public class SmtpConfiguration
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public bool UseSsl { get; set; }
		public string UserName { get; set; }
		public string UserPassword { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Common.Configuration.Mail
{
	public static class MailExtensions
	{
		public static async Task SendAsync(this SmtpClient client, SmtpConfiguration configuration, string senderName,
			IEnumerable<string> recipients, string subject, string body, bool sendBlindCopies = true)
		{
			await client.ConnectAsync(configuration.Host, configuration.Port, configuration.UseSsl);
			await client.AuthenticateAsync(configuration.UserName, configuration.UserPassword);
			var message = new MimeMessage
			{
				Subject = subject,
				Body = new TextPart(TextFormat.Html)
				{
					Text = body
				}
			};
			message.From.Add(new MailboxAddress(senderName, configuration.UserName));
			recipients = recipients.ToList();
			if (sendBlindCopies && recipients.Count() > 1)
			{
				message.To.Add(new MailboxAddress((string)null, recipients.First()));
				message.Bcc.AddRange(recipients.Select(item => new MailboxAddress((string)null, item)).Skip(1));
			}
			else
			{
				message.To.AddRange(recipients.Select(item => new MailboxAddress((string)null, item)));
			}
			await client.SendAsync(message);
		}

		public static async Task SendAsync(this SmtpClient client, SmtpConfiguration configuration, string senderName,
			string recipients, string subject, string body, bool sendBlindCopies = true)
		{
			await SendAsync(client, configuration, senderName,
				recipients.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()),
				subject, body, sendBlindCopies);
		}
	}
}

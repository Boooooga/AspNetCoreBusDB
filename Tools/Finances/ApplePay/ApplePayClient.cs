 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
 using System.Threading.Tasks;
 using Newtonsoft.Json;
 using Tools.Finances.ApplePay.Models;
 using Tools.Finances.ApplePay.Other;

 namespace Tools.Finances.ApplePay
{
	public class ApplePayClient
	{
		private readonly X509Certificate2 certificate;

		public ApplePayClient(X509Certificate2 certificate)
		{
			this.certificate = certificate ?? throw new ArgumentNullException(nameof(certificate), "Необходимо предоставить сертификат.");
			if (this.certificate.PrivateKey == null)
			{
				throw new ArgumentNullException(nameof(certificate), "Сертификат должен содержать закрытый ключ.");
			}
		}

		public ApplePayClient(StoreName certificateStoreName, StoreLocation certificateStoreLocation, string certificateThumbprint)
		{
			var store = new X509Store(certificateStoreName, certificateStoreLocation);
			store.Open(OpenFlags.ReadOnly);
			certificate = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false).Cast<X509Certificate2>().FirstOrDefault();
			if (certificate == null)
			{
				throw new Exception("Сертификат не найден.");
			}
			if (certificate.PrivateKey == null)
			{
				throw new Exception("Сертификат должен содержать закрытый ключ.");
			}
		}

		public async Task<string> GetPaymentSession(string validationUrl, GetPaymentSessionRequest requestData)
		{
			var handler = new HttpClientHandler
			{
				ClientCertificates =
				{
					certificate
				},
				SslProtocols = SslProtocols.Tls12
			};
			using var client = new HttpClient(handler);
			var response = await client.PostAsync(validationUrl, new StringContent(JsonConvert.SerializeObject(requestData,
				new JsonSerializerSettings
				{
					ContractResolver = new FirstLowerContractResolver()
				}), Encoding.UTF8, "application/json"));
			return await response.Content.ReadAsStringAsync();
		}
	}
}

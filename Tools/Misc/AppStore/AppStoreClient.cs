using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Tools.Misc.AppStore.Enums;
using Tools.Misc.AppStore.Requests;
using Tools.Misc.AppStore.Responses;
using UnixDateTimeConverter = Tools.Serialization.Converters.UnixDateTimeConverter;

namespace Tools.Misc.AppStore
{
	public class AppStoreClient
	{
		public Enums.StoreEnvironment Environment { get; }

		public AppStoreClient(Enums.StoreEnvironment environment)
		{
			Environment = environment;
		}

		public async Task<VerifyReceiptResponse> VerifyReceiptAsync(VerifyReceiptRequest request)
		{
			var serializer = new JsonSerializer
			{
				ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new SnakeCaseNamingStrategy()
				},
				NullValueHandling = NullValueHandling.Ignore,
			};
			serializer.Converters.Add(new StringEnumConverter());
			serializer.Converters.Add(new UnixDateTimeConverter(serializedValueType: UnixDateTimeConverter.SerializedValueType.String));
			using var client = new HttpClient(new HttpClientHandler
			{
				SslProtocols = SslProtocols.Tls12
			});
			var address = Environment == Enums.StoreEnvironment.Production
				? "https://buy.itunes.apple.com/verifyReceipt"
				: "https://sandbox.itunes.apple.com/verifyReceipt";
			await using var requestStringWriter = new StringWriter();
			using var requestJsonWriter = new JsonTextWriter(requestStringWriter);
			serializer.Serialize(requestJsonWriter, request);
			var httpResponse = await client.PostAsync(address, new StringContent(requestStringWriter.ToString(), Encoding.UTF8, "application/json"));
			var responseString = await httpResponse.Content.ReadAsStringAsync();
			using var responseStringReader = new StringReader(responseString);
			using var responseJsonReader = new JsonTextReader(responseStringReader);
			return serializer.Deserialize<VerifyReceiptResponse>(responseJsonReader);
		}
	}
}

using Newtonsoft.Json;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Enums;

namespace Tools.Finances.Ckassa.Responses
{
    /// <summary>
	/// Ответ на запрос создания анонимного платежа.
	/// </summary>
    public class CreateAnonymousPaymentResponse
    {
		/// <summary>
		/// Идентификатор магазина.
		/// </summary>
		[SignOrder(0)]
		public string ShopToken { get; set; }

		/// <summary>
		/// Идентификатор платежа.
		/// </summary>
		[JsonProperty("regPayNum")]
		[SignOrder(1)]
		public string PaymentId { get; set; }

		/// <summary>
		/// HTTP-метод перехода на форму оплаты.
		/// </summary>
		[JsonProperty("methodType")]
		[SignOrder(2)]
		public HttpMethod HttpMethod { get; set; }

		/// <summary>
		/// URL страницы оплаты (для рекуррентного платежа - ссылка на чек).
		/// </summary>
		[JsonProperty("payUrl")]
		[SignOrder(3)]
		public string PaymentUrl { get; set; }
	}
}

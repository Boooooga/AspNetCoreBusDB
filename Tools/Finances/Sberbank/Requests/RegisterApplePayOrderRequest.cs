using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Tools.Finances.Sberbank.Enums;

namespace Tools.Finances.Sberbank.Requests
{
	/// <summary>
	/// Класс, описывающий запрос на создание заказа с оплатой через Apple Pay.
	/// </summary>
	/// <inheritdoc />
	public class RegisterApplePayOrderRequest : MobilePaymentBaseRequest
	{
	}
}

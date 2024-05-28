using System;
using System.Collections.Generic;
using System.Text;
using Tools.Finances.Sberbank.Enums;
using Tools.Finances.Sberbank.Serializers;

namespace Tools.Finances.Sberbank.Requests
{
	/// <summary>
	/// Базовый класс для всех классов, описывающих запросы к платежному шлюзу.
	/// </summary>
	public abstract class BaseRequest
	{
		/// <summary>
		/// Создает объект, отвечающий за сериализацию данного запроса.
		/// </summary>
		internal virtual RequestSerializer CreateSerializer()
		{
			return new DefaultSerializer();
		}
	}
}

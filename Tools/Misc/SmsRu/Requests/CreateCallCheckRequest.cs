using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Misc.SmsRu.Requests
{
	public class CreateCallCheckRequest
	{
		public string Phone { get; set; }

		public CreateCallCheckRequest(string phone)
		{
			Phone = phone;
		}
	}
}

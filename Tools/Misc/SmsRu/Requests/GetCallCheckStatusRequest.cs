using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Misc.SmsRu.Requests
{
	public class GetCallCheckStatusRequest
	{
		public string CheckId { get; set; }

		public GetCallCheckStatusRequest(string checkId)
		{
			CheckId = checkId;
		}
	}
}

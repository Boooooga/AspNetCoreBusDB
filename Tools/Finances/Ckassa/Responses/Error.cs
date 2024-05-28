using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Finances.Ckassa.Responses
{
	public class Error
	{
		public string Message { get; set; }

		public string UserMessage { get; set; }

		public int Code { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Finances.Ckassa.Responses
{
	public class ResponseWrapper<TResponseData, TError> where TError: Error, new()
	{
		public TResponseData Data { get; set; }

		public TError Error { get; set; }
	}
}

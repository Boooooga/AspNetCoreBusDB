using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Search
{
	public class CheckupsSearchParams : BaseSearchParams
	{
		public CheckupsSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
		}
	}
}

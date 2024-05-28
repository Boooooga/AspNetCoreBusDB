using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class BusRoute
	{
		public int Id { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan FinishTime { get; set; }

		public BusRoute(int id, TimeSpan startTime, TimeSpan finishTime)
		{
			Id = id;
			StartTime = startTime;
			FinishTime = finishTime;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class ArrivalTime
	{
		public int Id { get; set; }
		public TimeSpan Arrival { get; set; }
		public int IdBusRouteStop { get; set; }

		public ArrivalTime(int id, TimeSpan arrival, int idBusRouteStop)
		{
			Id = id;
			Arrival = arrival;
			IdBusRouteStop = idBusRouteStop;
		}
	}
}

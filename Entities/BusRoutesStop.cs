using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class BusRoutesStop
	{
		public int Id { get; set; }
		public int IdStop { get; set; }
		public int IdRoute { get; set; }

		public BusRoutesStop(int id, int idStop, int idRoute)
		{
			Id = id;
			IdStop = idStop;
			IdRoute = idRoute;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class RouteList
	{
		public int Id { get; set; }
		public DateTime RouteDate { get; set; }
		public int IdBus { get; set; }
		public int IdDriver { get; set; }
		public int? IdConductor { get; set; }
		public int IdRoute { get; set; }

		public RouteList(int id, DateTime routeDate, int idBus, int idDriver, int? idConductor, int idRoute)
		{
			Id = id;
			RouteDate = routeDate;
			IdBus = idBus;
			IdDriver = idDriver;
			IdConductor = idConductor;
			IdRoute = idRoute;
		}
	}
}

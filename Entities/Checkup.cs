using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Checkup
	{
		public int Id { get; set; }
		public DateTime CheckupDate { get; set; }
		public bool IsOk { get; set; }
		public int IdBus { get; set; }
		public int IdMechanic { get; set; }

		public Checkup(int id, DateTime checkupDate, bool isOk, int idBus, int idMechanic)
		{
			Id = id;
			CheckupDate = checkupDate;
			IsOk = isOk;
			IdBus = idBus;
			IdMechanic = idMechanic;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Stop
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Adress { get; set; }

		public Stop(int id, string name, string adress)
		{
			Id = id;
			Name = name;
			Adress = adress;
		}
	}
}

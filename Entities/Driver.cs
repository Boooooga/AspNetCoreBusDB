using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Driver
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public int Experience { get; set; }

		public Driver(int id, string name, int age, int experience)
		{
			Id = id;
			Name = name;
			Age = age;
			Experience = experience;
		}
	}
}

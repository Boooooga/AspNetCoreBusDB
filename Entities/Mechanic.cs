using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Mechanic
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }

		public Mechanic(int id, string name, int age)
		{
			Id = id;
			Name = name;
			Age = age;
		}
	}
}

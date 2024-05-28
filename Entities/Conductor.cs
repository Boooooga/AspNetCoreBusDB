using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Conductor
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }

		public Conductor(int id, string name, int age)
		{
			Id = id;
			Name = name;
			Age = age;
		}
	}
}

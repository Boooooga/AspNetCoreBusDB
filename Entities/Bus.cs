using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;

namespace Entities
{
	public class Bus
	{
		public int Id { get; set; }
		public string Model { get; set; }
		public int Capacity { get; set; }

		public Bus(int id, string model, int capacity)
		{
			Id = id;
			Model = model;
			Capacity = capacity;
		}
	}
}

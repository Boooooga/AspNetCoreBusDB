using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class DriverModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Age")]
		public int Age { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Experience")]
		public int Experience { get; set; }

		public static DriverModel FromEntity(Driver obj)
		{
			return obj == null ? null : new DriverModel
			{
				Id = obj.Id,
				Name = obj.Name,
				Age = obj.Age,
				Experience = obj.Experience,
			};
		}

		public static Driver ToEntity(DriverModel obj)
		{
			return obj == null ? null : new Driver(obj.Id, obj.Name, obj.Age, obj.Experience);
		}

		public static List<DriverModel> FromEntitiesList(IEnumerable<Driver> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Driver> ToEntitiesList(IEnumerable<DriverModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

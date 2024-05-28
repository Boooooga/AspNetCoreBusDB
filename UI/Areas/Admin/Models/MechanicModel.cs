using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class MechanicModel
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

		public static MechanicModel FromEntity(Mechanic obj)
		{
			return obj == null ? null : new MechanicModel
			{
				Id = obj.Id,
				Name = obj.Name,
				Age = obj.Age,
			};
		}

		public static Mechanic ToEntity(MechanicModel obj)
		{
			return obj == null ? null : new Mechanic(obj.Id, obj.Name, obj.Age);
		}

		public static List<MechanicModel> FromEntitiesList(IEnumerable<Mechanic> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Mechanic> ToEntitiesList(IEnumerable<MechanicModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class ConductorModel
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

		public static ConductorModel FromEntity(Conductor obj)
		{
			return obj == null ? null : new ConductorModel
			{
				Id = obj.Id,
				Name = obj.Name,
				Age = obj.Age,
			};
		}

		public static Conductor ToEntity(ConductorModel obj)
		{
			return obj == null ? null : new Conductor(obj.Id, obj.Name, obj.Age);
		}

		public static List<ConductorModel> FromEntitiesList(IEnumerable<Conductor> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Conductor> ToEntitiesList(IEnumerable<ConductorModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

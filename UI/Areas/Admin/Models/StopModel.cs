using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class StopModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Adress")]
		public string Adress { get; set; }

		public static StopModel FromEntity(Stop obj)
		{
			return obj == null ? null : new StopModel
			{
				Id = obj.Id,
				Name = obj.Name,
				Adress = obj.Adress,
			};
		}

		public static Stop ToEntity(StopModel obj)
		{
			return obj == null ? null : new Stop(obj.Id, obj.Name, obj.Adress);
		}

		public static List<StopModel> FromEntitiesList(IEnumerable<Stop> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Stop> ToEntitiesList(IEnumerable<StopModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class BusModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Model")]
		public string Model { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Capacity")]
		public int Capacity { get; set; }

		public static BusModel FromEntity(Bus obj)
		{
			return obj == null ? null : new BusModel
			{
				Id = obj.Id,
				Model = obj.Model,
				Capacity = obj.Capacity,
			};
		}

		public static Bus ToEntity(BusModel obj)
		{
			return obj == null ? null : new Bus(obj.Id, obj.Model, obj.Capacity);
		}

		public static List<BusModel> FromEntitiesList(IEnumerable<Bus> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Bus> ToEntitiesList(IEnumerable<BusModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

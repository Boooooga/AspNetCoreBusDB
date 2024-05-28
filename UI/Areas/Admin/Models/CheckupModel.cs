using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class CheckupModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "CheckupDate")]
		public DateTime CheckupDate { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "IsOk")]
		public bool IsOk { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "IdBus")]
		public int IdBus { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "IdMechanic")]
		public int IdMechanic { get; set; }

		public static CheckupModel FromEntity(Checkup obj)
		{
			return obj == null ? null : new CheckupModel
			{
				Id = obj.Id,
				CheckupDate = obj.CheckupDate,
				IsOk = obj.IsOk,
				IdBus = obj.IdBus,
				IdMechanic = obj.IdMechanic,
			};
		}

		public static Checkup ToEntity(CheckupModel obj)
		{
			return obj == null ? null : new Checkup(obj.Id, obj.CheckupDate, obj.IsOk, obj.IdBus, obj.IdMechanic);
		}

		public static List<CheckupModel> FromEntitiesList(IEnumerable<Checkup> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<Checkup> ToEntitiesList(IEnumerable<CheckupModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class BusRoutesStopModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "IdStop")]
		public int IdStop { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "IdRoute")]
		public int IdRoute { get; set; }

		public static BusRoutesStopModel FromEntity(BusRoutesStop obj)
		{
			return obj == null ? null : new BusRoutesStopModel
			{
				Id = obj.Id,
				IdStop = obj.IdStop,
				IdRoute = obj.IdRoute,
			};
		}

		public static BusRoutesStop ToEntity(BusRoutesStopModel obj)
		{
			return obj == null ? null : new BusRoutesStop(obj.Id, obj.IdStop, obj.IdRoute);
		}

		public static List<BusRoutesStopModel> FromEntitiesList(IEnumerable<BusRoutesStop> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<BusRoutesStop> ToEntitiesList(IEnumerable<BusRoutesStopModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

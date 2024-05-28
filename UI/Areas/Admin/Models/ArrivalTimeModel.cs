using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class ArrivalTimeModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Arrival")]
		public TimeSpan Arrival { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "IdBusRouteStop")]
		public int IdBusRouteStop { get; set; }

		public static ArrivalTimeModel FromEntity(ArrivalTime obj)
		{
			return obj == null ? null : new ArrivalTimeModel
			{
				Id = obj.Id,
				Arrival = obj.Arrival,
				IdBusRouteStop = obj.IdBusRouteStop,
			};
		}

		public static ArrivalTime ToEntity(ArrivalTimeModel obj)
		{
			return obj == null ? null : new ArrivalTime(obj.Id, obj.Arrival, obj.IdBusRouteStop);
		}

		public static List<ArrivalTimeModel> FromEntitiesList(IEnumerable<ArrivalTime> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<ArrivalTime> ToEntitiesList(IEnumerable<ArrivalTimeModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

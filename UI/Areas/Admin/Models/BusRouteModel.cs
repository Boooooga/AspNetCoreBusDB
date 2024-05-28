using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class BusRouteModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "StartTime")]
		public TimeSpan StartTime { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "FinishTime")]
		public TimeSpan FinishTime { get; set; }

		public static BusRouteModel FromEntity(BusRoute obj)
		{
			return obj == null ? null : new BusRouteModel
			{
				Id = obj.Id,
				StartTime = obj.StartTime,
				FinishTime = obj.FinishTime,
			};
		}

		public static BusRoute ToEntity(BusRouteModel obj)
		{
			return obj == null ? null : new BusRoute(obj.Id, obj.StartTime, obj.FinishTime);
		}

		public static List<BusRouteModel> FromEntitiesList(IEnumerable<BusRoute> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<BusRoute> ToEntitiesList(IEnumerable<BusRouteModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

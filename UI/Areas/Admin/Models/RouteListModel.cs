using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;

namespace UI.Areas.Admin.Models
{
	public class RouteListModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "RouteDate")]
		public DateTime RouteDate { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "IdBus")]
		public int IdBus { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "IdDriver")]
		public int IdDriver { get; set; }

		[Display(Name = "IdConductor")]
		public int? IdConductor { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "IdRoute")]
		public int IdRoute { get; set; }

		public static RouteListModel FromEntity(RouteList obj)
		{
			return obj == null ? null : new RouteListModel
			{
				Id = obj.Id,
				RouteDate = obj.RouteDate,
				IdBus = obj.IdBus,
				IdDriver = obj.IdDriver,
				IdConductor = obj.IdConductor,
				IdRoute = obj.IdRoute,
			};
		}

		public static RouteList ToEntity(RouteListModel obj)
		{
			return obj == null ? null : new RouteList(obj.Id, obj.RouteDate, obj.IdBus, obj.IdDriver, obj.IdConductor,
				obj.IdRoute);
		}

		public static List<RouteListModel> FromEntitiesList(IEnumerable<RouteList> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<RouteList> ToEntitiesList(IEnumerable<RouteListModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

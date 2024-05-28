using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.Enums;
using Common.Search;
using Dal.DbModels;

namespace Dal
{
	public class ArrivalTimeDal : BaseDal<DefaultDbContext, ArrivalTime, Entities.ArrivalTime, int, ArrivalTimeSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public ArrivalTimeDal()
		{
		}

		protected internal ArrivalTimeDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.ArrivalTime entity, ArrivalTime dbObject, bool exists)
		{
			dbObject.Arrival = entity.Arrival;
			dbObject.IdBusRouteStop = entity.IdBusRouteStop;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<ArrivalTime>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<ArrivalTime> dbObjects, ArrivalTimeSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.ArrivalTime>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<ArrivalTime> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<ArrivalTime, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.ArrivalTime, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.ArrivalTime ConvertDbObjectToEntity(ArrivalTime dbObject)
		{
			return dbObject == null ? null : new Entities.ArrivalTime(dbObject.Id, dbObject.Arrival,
				dbObject.IdBusRouteStop);
		}
	}
}

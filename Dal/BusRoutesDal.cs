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
	public class BusRoutesDal : BaseDal<DefaultDbContext, BusRoute, Entities.BusRoute, int, BusRoutesSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public BusRoutesDal()
		{
		}

		protected internal BusRoutesDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.BusRoute entity, BusRoute dbObject, bool exists)
		{
			dbObject.StartTime = entity.StartTime;
			dbObject.FinishTime = entity.FinishTime;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<BusRoute>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<BusRoute> dbObjects, BusRoutesSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.BusRoute>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<BusRoute> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<BusRoute, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.BusRoute, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.BusRoute ConvertDbObjectToEntity(BusRoute dbObject)
		{
			return dbObject == null ? null : new Entities.BusRoute(dbObject.Id, dbObject.StartTime,
				dbObject.FinishTime);
		}
	}
}

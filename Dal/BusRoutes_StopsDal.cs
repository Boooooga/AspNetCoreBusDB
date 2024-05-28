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
	public class BusRoutes_StopsDal : BaseDal<DefaultDbContext, BusRoutesStop, Entities.BusRoutesStop, int, BusRoutes_StopsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public BusRoutes_StopsDal()
		{
		}

		protected internal BusRoutes_StopsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.BusRoutesStop entity, BusRoutesStop dbObject, bool exists)
		{
			dbObject.IdStop = entity.IdStop;
			dbObject.IdRoute = entity.IdRoute;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<BusRoutesStop>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<BusRoutesStop> dbObjects, BusRoutes_StopsSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.BusRoutesStop>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<BusRoutesStop> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<BusRoutesStop, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.BusRoutesStop, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.BusRoutesStop ConvertDbObjectToEntity(BusRoutesStop dbObject)
		{
			return dbObject == null ? null : new Entities.BusRoutesStop(dbObject.Id, dbObject.IdStop, dbObject.IdRoute);
		}
	}
}

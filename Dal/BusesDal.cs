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
	public class BusesDal : BaseDal<DefaultDbContext, Bus, Entities.Bus, int, BusesSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public BusesDal()
		{
		}

		protected internal BusesDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Bus entity, Bus dbObject, bool exists)
		{
			dbObject.Model = entity.Model;
			dbObject.Capacity = entity.Capacity;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Bus>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Bus> dbObjects, BusesSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Bus>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Bus> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Bus, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Bus, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Bus ConvertDbObjectToEntity(Bus dbObject)
		{
			return dbObject == null ? null : new Entities.Bus(dbObject.Id, dbObject.Model, dbObject.Capacity);
		}
	}
}

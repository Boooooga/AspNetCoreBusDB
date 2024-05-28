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
	public class CheckupsDal : BaseDal<DefaultDbContext, Checkup, Entities.Checkup, int, CheckupsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public CheckupsDal()
		{
		}

		protected internal CheckupsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Checkup entity, Checkup dbObject, bool exists)
		{
			dbObject.CheckupDate = entity.CheckupDate;
			dbObject.IsOk = entity.IsOk;
			dbObject.IdBus = entity.IdBus;
			dbObject.IdMechanic = entity.IdMechanic;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Checkup>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Checkup> dbObjects, CheckupsSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Checkup>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Checkup> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Checkup, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Checkup, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Checkup ConvertDbObjectToEntity(Checkup dbObject)
		{
			return dbObject == null ? null : new Entities.Checkup(dbObject.Id, dbObject.CheckupDate, dbObject.IsOk,
				dbObject.IdBus, dbObject.IdMechanic);
		}
	}
}

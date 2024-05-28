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
	public class StopsDal : BaseDal<DefaultDbContext, Stop, Entities.Stop, int, StopsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public StopsDal()
		{
		}

		protected internal StopsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Stop entity, Stop dbObject, bool exists)
		{
			dbObject.Name = entity.Name;
			dbObject.Adress = entity.Adress;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Stop>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Stop> dbObjects, StopsSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Stop>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Stop> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Stop, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Stop, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Stop ConvertDbObjectToEntity(Stop dbObject)
		{
			return dbObject == null ? null : new Entities.Stop(dbObject.Id, dbObject.Name, dbObject.Adress);
		}
	}
}

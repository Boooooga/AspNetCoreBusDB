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
	public class ConductorsDal : BaseDal<DefaultDbContext, Conductor, Entities.Conductor, int, ConductorsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public ConductorsDal()
		{
		}

		protected internal ConductorsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Conductor entity, Conductor dbObject, bool exists)
		{
			dbObject.Name = entity.Name;
			dbObject.Age = entity.Age;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Conductor>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Conductor> dbObjects, ConductorsSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Conductor>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Conductor> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Conductor, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Conductor, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Conductor ConvertDbObjectToEntity(Conductor dbObject)
		{
			return dbObject == null ? null : new Entities.Conductor(dbObject.Id, dbObject.Name, dbObject.Age);
		}
	}
}

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
	public class MechanicsDal : BaseDal<DefaultDbContext, Mechanic, Entities.Mechanic, int, MechanicsSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public MechanicsDal()
		{
		}

		protected internal MechanicsDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Mechanic entity, Mechanic dbObject, bool exists)
		{
			dbObject.Name = entity.Name;
			dbObject.Age = entity.Age;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Mechanic>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Mechanic> dbObjects, MechanicsSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Mechanic>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Mechanic> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Mechanic, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Mechanic, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Mechanic ConvertDbObjectToEntity(Mechanic dbObject)
		{
			return dbObject == null ? null : new Entities.Mechanic(dbObject.Id, dbObject.Name, dbObject.Age);
		}
	}
}

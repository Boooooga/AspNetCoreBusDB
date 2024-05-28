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
	public class DriversDal : BaseDal<DefaultDbContext, Driver, Entities.Driver, int, DriversSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public DriversDal()
		{
		}

		protected internal DriversDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.Driver entity, Driver dbObject, bool exists)
		{
			dbObject.Name = entity.Name;
			dbObject.Age = entity.Age;
			dbObject.Experience = entity.Experience;
			return Task.CompletedTask;
		}
	
		protected override Task<IQueryable<Driver>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Driver> dbObjects, DriversSearchParams searchParams)
		{
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.Driver>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Driver> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<Driver, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.Driver, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.Driver ConvertDbObjectToEntity(Driver dbObject)
		{
			return dbObject == null ? null : new Entities.Driver(dbObject.Id, dbObject.Name, dbObject.Age,
				dbObject.Experience);
		}
	}
}

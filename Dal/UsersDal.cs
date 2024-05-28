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
	public class UsersDal : BaseDal<DefaultDbContext, User, Entities.User, int, UsersSearchParams, object>
	{
		protected override bool RequiresUpdatesAfterObjectSaving => false;

		public UsersDal()
		{
		}

		protected internal UsersDal(DefaultDbContext context) : base(context)
		{
		}

		protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, Entities.User entity, User dbObject, bool exists)
		{
			dbObject.IsBlocked = entity.IsBlocked;
			dbObject.Login = entity.Login;
			if (!exists || entity.Password != null)
				dbObject.Password = entity.Password;
			dbObject.RoleId = (int)entity.Role;
			if (!exists)
			{
				dbObject.RegistrationDate = entity.RegistrationDate;
			}
			return Task.CompletedTask;
		}

		protected override Task<IQueryable<User>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<User> dbObjects, UsersSearchParams searchParams)
		{
			if (searchParams.Roles != null)
			{
				var rolesArray = searchParams.Roles.Cast<int>().ToArray();
				dbObjects = dbObjects.Where(item => rolesArray.Contains(item.RoleId));
			}
			if (!string.IsNullOrEmpty(searchParams.SearchQuery))
			{
				dbObjects = dbObjects.Where(item => item.Login.Contains(searchParams.SearchQuery));
			}
			dbObjects = dbObjects.OrderBy(item => item.Login);
			return Task.FromResult(dbObjects);
		}

		protected override async Task<IList<Entities.User>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<User> dbObjects, object convertParams, bool isFull)
		{
			return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
		}

		protected override Expression<Func<User, int>> GetIdByDbObjectExpression()
		{
			return item => item.Id;
		}

		protected override Expression<Func<Entities.User, int>> GetIdByEntityExpression()
		{
			return item => item.Id;
		}

		internal static Entities.User ConvertDbObjectToEntity(User dbObject)
		{
			return dbObject == null ? null : new Entities.User(dbObject.Id, dbObject.IsBlocked, dbObject.Login,
				dbObject.Password, dbObject.RegistrationDate, (UserRole)dbObject.RoleId);
		}

		public Task<bool> ExistsAsync(string login)
		{
			return ExistsAsync(item => item.Login == login);
		}

		public async Task<Entities.User> GetAsync(string login)
		{
			return (await GetAsync(item => item.Login == login)).FirstOrDefault();
		}
	}
}

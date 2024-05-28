using System;
using System.Collections.Generic;
using System.Linq;
using Dal;
using Common.Enums;
using Common.Search;
using User = Entities.User;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BL
{
	public class UsersBL
	{
		public async Task<int> AddOrUpdateAsync(User entity)
		{
			entity.Password = Helpers.GetPasswordHash(entity.Password);
			entity.RegistrationDate = DateTime.Now;
			entity.Id = await new UsersDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new UsersDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(UsersSearchParams searchParams)
		{
			return new UsersDal().ExistsAsync(searchParams);
		}

		public Task<bool> ExistsAsync(string login)
		{
			return new UsersDal().ExistsAsync(login);
		}

		public Task<User> GetAsync(int id)
		{
			return new UsersDal().GetAsync(id);
		}

		public Task<User> GetAsync(string login)
		{
			return new UsersDal().GetAsync(login);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new UsersDal().DeleteAsync(id);
		}

		public Task<SearchResult<User>> GetAsync(UsersSearchParams searchParams)
		{
			return new UsersDal().GetAsync(searchParams);
		}

		public async Task<User> VerifyPasswordAsync(string login, string password)
		{
			var user = await GetAsync(login);
			return user != null && user.Password == Helpers.GetPasswordHash(password) ? user : null;
		}
	}
}


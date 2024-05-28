using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Driver = Entities.Driver;

namespace BL
{
	public class DriversBL
	{
		public async Task<int> AddOrUpdateAsync(Driver entity)
		{
			entity.Id = await new DriversDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new DriversDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(DriversSearchParams searchParams)
		{
			return new DriversDal().ExistsAsync(searchParams);
		}

		public Task<Driver> GetAsync(int id)
		{
			return new DriversDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new DriversDal().DeleteAsync(id);
		}

		public Task<SearchResult<Driver>> GetAsync(DriversSearchParams searchParams)
		{
			return new DriversDal().GetAsync(searchParams);
		}
	}
}


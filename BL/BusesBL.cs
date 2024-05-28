using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Bus = Entities.Bus;

namespace BL
{
	public class BusesBL
	{
		public async Task<int> AddOrUpdateAsync(Bus entity)
		{
			entity.Id = await new BusesDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new BusesDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(BusesSearchParams searchParams)
		{
			return new BusesDal().ExistsAsync(searchParams);
		}

		public Task<Bus> GetAsync(int id)
		{
			return new BusesDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new BusesDal().DeleteAsync(id);
		}

		public Task<SearchResult<Bus>> GetAsync(BusesSearchParams searchParams)
		{
			return new BusesDal().GetAsync(searchParams);
		}
	}
}


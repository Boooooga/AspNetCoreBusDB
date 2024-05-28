using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Stop = Entities.Stop;

namespace BL
{
	public class StopsBL
	{
		public async Task<int> AddOrUpdateAsync(Stop entity)
		{
			entity.Id = await new StopsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new StopsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(StopsSearchParams searchParams)
		{
			return new StopsDal().ExistsAsync(searchParams);
		}

		public Task<Stop> GetAsync(int id)
		{
			return new StopsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new StopsDal().DeleteAsync(id);
		}

		public Task<SearchResult<Stop>> GetAsync(StopsSearchParams searchParams)
		{
			return new StopsDal().GetAsync(searchParams);
		}
	}
}


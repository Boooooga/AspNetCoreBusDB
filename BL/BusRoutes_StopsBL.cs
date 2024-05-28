using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using BusRoutesStop = Entities.BusRoutesStop;

namespace BL
{
	public class BusRoutes_StopsBL
	{
		public async Task<int> AddOrUpdateAsync(BusRoutesStop entity)
		{
			entity.Id = await new BusRoutes_StopsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new BusRoutes_StopsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(BusRoutes_StopsSearchParams searchParams)
		{
			return new BusRoutes_StopsDal().ExistsAsync(searchParams);
		}

		public Task<BusRoutesStop> GetAsync(int id)
		{
			return new BusRoutes_StopsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new BusRoutes_StopsDal().DeleteAsync(id);
		}

		public Task<SearchResult<BusRoutesStop>> GetAsync(BusRoutes_StopsSearchParams searchParams)
		{
			return new BusRoutes_StopsDal().GetAsync(searchParams);
		}
	}
}


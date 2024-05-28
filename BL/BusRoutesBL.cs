using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using BusRoute = Entities.BusRoute;

namespace BL
{
	public class BusRoutesBL
	{
		public async Task<int> AddOrUpdateAsync(BusRoute entity)
		{
			entity.Id = await new BusRoutesDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new BusRoutesDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(BusRoutesSearchParams searchParams)
		{
			return new BusRoutesDal().ExistsAsync(searchParams);
		}

		public Task<BusRoute> GetAsync(int id)
		{
			return new BusRoutesDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new BusRoutesDal().DeleteAsync(id);
		}

		public Task<SearchResult<BusRoute>> GetAsync(BusRoutesSearchParams searchParams)
		{
			return new BusRoutesDal().GetAsync(searchParams);
		}
	}
}


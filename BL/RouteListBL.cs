using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using RouteList = Entities.RouteList;

namespace BL
{
	public class RouteListBL
	{
		public async Task<int> AddOrUpdateAsync(RouteList entity)
		{
			entity.Id = await new RouteListDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new RouteListDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(RouteListSearchParams searchParams)
		{
			return new RouteListDal().ExistsAsync(searchParams);
		}

		public Task<RouteList> GetAsync(int id)
		{
			return new RouteListDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new RouteListDal().DeleteAsync(id);
		}

		public Task<SearchResult<RouteList>> GetAsync(RouteListSearchParams searchParams)
		{
			return new RouteListDal().GetAsync(searchParams);
		}
	}
}


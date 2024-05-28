using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using ArrivalTime = Entities.ArrivalTime;

namespace BL
{
	public class ArrivalTimeBL
	{
		public async Task<int> AddOrUpdateAsync(ArrivalTime entity)
		{
			entity.Id = await new ArrivalTimeDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new ArrivalTimeDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(ArrivalTimeSearchParams searchParams)
		{
			return new ArrivalTimeDal().ExistsAsync(searchParams);
		}

		public Task<ArrivalTime> GetAsync(int id)
		{
			return new ArrivalTimeDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new ArrivalTimeDal().DeleteAsync(id);
		}

		public Task<SearchResult<ArrivalTime>> GetAsync(ArrivalTimeSearchParams searchParams)
		{
			return new ArrivalTimeDal().GetAsync(searchParams);
		}
	}
}


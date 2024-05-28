using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Checkup = Entities.Checkup;

namespace BL
{
	public class CheckupsBL
	{
		public async Task<int> AddOrUpdateAsync(Checkup entity)
		{
			entity.Id = await new CheckupsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new CheckupsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(CheckupsSearchParams searchParams)
		{
			return new CheckupsDal().ExistsAsync(searchParams);
		}

		public Task<Checkup> GetAsync(int id)
		{
			return new CheckupsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new CheckupsDal().DeleteAsync(id);
		}

		public Task<SearchResult<Checkup>> GetAsync(CheckupsSearchParams searchParams)
		{
			return new CheckupsDal().GetAsync(searchParams);
		}
	}
}


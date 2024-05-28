using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Conductor = Entities.Conductor;

namespace BL
{
	public class ConductorsBL
	{
		public async Task<int> AddOrUpdateAsync(Conductor entity)
		{
			entity.Id = await new ConductorsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new ConductorsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(ConductorsSearchParams searchParams)
		{
			return new ConductorsDal().ExistsAsync(searchParams);
		}

		public Task<Conductor> GetAsync(int id)
		{
			return new ConductorsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new ConductorsDal().DeleteAsync(id);
		}

		public Task<SearchResult<Conductor>> GetAsync(ConductorsSearchParams searchParams)
		{
			return new ConductorsDal().GetAsync(searchParams);
		}
	}
}


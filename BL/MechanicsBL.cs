using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Common.Enums;
using Common.Search;
using Mechanic = Entities.Mechanic;

namespace BL
{
	public class MechanicsBL
	{
		public async Task<int> AddOrUpdateAsync(Mechanic entity)
		{
			entity.Id = await new MechanicsDal().AddOrUpdateAsync(entity);
			return entity.Id;
		}

		public Task<bool> ExistsAsync(int id)
		{
			return new MechanicsDal().ExistsAsync(id);
		}

		public Task<bool> ExistsAsync(MechanicsSearchParams searchParams)
		{
			return new MechanicsDal().ExistsAsync(searchParams);
		}

		public Task<Mechanic> GetAsync(int id)
		{
			return new MechanicsDal().GetAsync(id);
		}

		public Task<bool> DeleteAsync(int id)
		{
			return new MechanicsDal().DeleteAsync(id);
		}

		public Task<SearchResult<Mechanic>> GetAsync(MechanicsSearchParams searchParams)
		{
			return new MechanicsDal().GetAsync(searchParams);
		}
	}
}


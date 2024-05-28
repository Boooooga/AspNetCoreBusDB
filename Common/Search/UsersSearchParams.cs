using System.Collections.Generic;
using Common.Enums;

namespace Common.Search
{
	public class UsersSearchParams : BaseSearchParams
	{
		public IEnumerable<UserRole> Roles { get; set; }
		public string SearchQuery { get; set; }

		public UsersSearchParams(IEnumerable<UserRole> roles = null, string searchQuery = null, int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
		{
			Roles = roles;
			SearchQuery = searchQuery;
		}
	}
}

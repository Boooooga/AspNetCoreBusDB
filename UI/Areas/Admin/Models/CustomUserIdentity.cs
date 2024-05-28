using System.Collections.Generic;
using System.Security.Claims;
using Common.Enums;

namespace UI.Areas.Admin.Models
{
	public class CustomUserIdentity : ClaimsIdentity
	{
		public int? Id { get; set; }

		public CustomUserIdentity(UserModel user, string authenticationType = "Cookie") : base(GetUserClaims(user), authenticationType)
		{
			Id = user?.Id;
		}

		private static List<Claim> GetUserClaims(UserModel user)
		{
			if (user == null || user.IsBlocked)
			{
				return new List<Claim>();
			}
			var result = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Login),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};
			if (user.Role == UserRole.Developer)
			{
				result.Add(new Claim(ClaimTypes.Role, UserRole.Admin.ToString()));
			}
			return result;
		}

	}
}

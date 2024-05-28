using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
	public enum UserRole
	{
		[Display(Name = "Разработчик")]
		Developer = 0,

		[Display(Name = "Администратор")]
		Admin = 1,
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Enums;
using Entities;
using User = Entities.User;

namespace UI.Areas.Admin.Models
{
	public class UserModel
	{
		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Заблокирован")]
		public bool IsBlocked { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Логин")]
		public string Login { get; set; }

		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Display(Name = "Дата регистрации")]
		public DateTime RegistrationDate { get; set; }

		[Required(ErrorMessage = "Укажите значение")]
		[Display(Name = "Роль")]
		public UserRole Role { get; set; }

		public static UserModel FromEntity(User obj)
		{
			return obj == null ? null : new UserModel
			{
				Id = obj.Id,
				IsBlocked = obj.IsBlocked,
				Login = obj.Login,
				Password = obj.Password,
				RegistrationDate = obj.RegistrationDate,
				Role = obj.Role,
			};
		}

		public static User ToEntity(UserModel obj)
		{
			return obj == null ? null : new User(obj.Id, obj.IsBlocked, obj.Login, obj.Password, obj.RegistrationDate, obj.Role);
		}

		public static List<UserModel> FromEntitiesList(IEnumerable<User> list)
		{
			return list?.Select(FromEntity).ToList();
		}

		public static List<User> ToEntitiesList(IEnumerable<UserModel> list)
		{
			return list?.Select(ToEntity).ToList();
		}
	}
}

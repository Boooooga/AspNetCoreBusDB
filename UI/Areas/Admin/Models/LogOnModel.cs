using System.ComponentModel.DataAnnotations;

namespace UI.Areas.Admin.Models
{
	public class LogOnModel
	{
		[Required(ErrorMessage="Укажите логин")]
		[Display(Name="Логин")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Укажите пароль")]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Display(Name = "Запомнить")]
		public bool Remember { get; set; }

		public string ReturnUrl { get; set; }
	}
}
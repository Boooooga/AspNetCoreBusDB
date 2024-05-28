using System.ComponentModel.DataAnnotations;

namespace UI.Areas.Admin.Models.ViewModels.FilterModels
{
	public class UsersFilterModel : BaseFilterModel
	{
		[Display(Name = "Поисковый запрос")]
		public string SearchQuery { get; set; }
	}
}

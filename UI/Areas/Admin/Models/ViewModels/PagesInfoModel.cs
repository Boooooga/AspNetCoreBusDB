namespace UI.Areas.Admin.Models.ViewModels
{
	public class PagesInfoModel
	{
		public int ItemsCount { get; set; }

		public int ItemsPerPage { get; set; }

		public int CurrentPage { get; set; }

		public int DisplayedPages { get; set; }

		public PagesInfoModel(int itemsCount, int itemsPerPage, int currentPage, int displayedPages)
		{
			ItemsCount = itemsCount;
			ItemsPerPage = itemsPerPage;
			CurrentPage = currentPage;
			DisplayedPages = displayedPages;
		}
	}
}
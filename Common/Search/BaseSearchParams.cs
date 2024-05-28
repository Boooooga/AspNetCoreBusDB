namespace Common.Search
{
	public class BaseSearchParams
	{
		public int StartIndex { get; set; }
		public int? ObjectsCount { get; set; }

		public BaseSearchParams(int startIndex = 0, int? objectsCount = null)
		{
			StartIndex = startIndex;
			ObjectsCount = objectsCount;
		}
	}
}

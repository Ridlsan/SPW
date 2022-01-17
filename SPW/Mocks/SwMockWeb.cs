using SPW.Common;

namespace SPW.Mocks
{

	/// <summary>
	/// Mocked Web
	/// </summary>
	public class SwMockWeb
	{
		/// <summary>
		/// Mocked lists.
		/// </summary>
		public virtual Vocabulary<(string listName, SwListTemplate template), SwMockList> MockLists { get; set; }
			= new Vocabulary<(string listName, SwListTemplate template), SwMockList>();

		public ISwDynamicList GetList(string listName, SwListTemplate template = SwListTemplate.List)
		{
			return MockLists[(listName, template)];
		}

		public ISwList<T> GetList<T>(string listName, SwListTemplate template = SwListTemplate.List) where T : SwListItem
		{
			return new SwMockList<T>(MockLists[(listName, template)]);
		}
	}
}
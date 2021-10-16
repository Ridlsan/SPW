using System.Data;

namespace SPW.Mocks
{
	public class SwMockWeb : ISwWeb
	{
		private readonly DataSet _lists;

		public SwMockWeb(string url, DataSet listsData)
		{
			_lists = listsData;
			ServerRelativeUrl = url;
		}

		/// <inheritdoc />
		public void Dispose()
		{
		}

		/// <inheritdoc />
		public string ServerRelativeUrl { get; set; }

		/// <inheritdoc />
		public SwDynamicList GetList(string listName, SwListTemplate template = SwListTemplate.List)
		{
			return new SwDynamicMockList(listName, _lists.Tables[listName + "#" + template], template);
		}

		/// <inheritdoc />
		public ISwList<T> GetList<T>(string listName, SwListTemplate template = SwListTemplate.List)
				where T : SwListItem
		{
			return new SwMockList<T>(this, _lists.Tables[listName + "#" + template], listName, template);
		}
	}
}
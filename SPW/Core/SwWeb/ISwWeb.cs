namespace SPW
{
	/// <summary>
	///   Wrapped instanced of SPWeb
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public interface ISwWeb
	{
		/// <summary>
		///   Server relative url
		/// </summary>
		string ServerRelativeUrl { get; }

		/// <summary>
		///   Gets dynamic list
		/// </summary>
		/// <param name="listName">Name of the list.</param>
		/// <param name="template">The template.</param>
		/// <returns>ISwDynamicList</returns>
		ISwDynamicList GetList(string listName, SwListTemplate template = SwListTemplate.List);

		/// <summary>
		///  Returns current user
		/// </summary>
		ISwUser CurrentUser { get; }

		/// <summary>
		///   Gets the strongly typed list
		/// </summary>
		/// <typeparam name="T">SwListItem</typeparam>
		/// <param name="listName">Name of the list.</param>
		/// <param name="template">The template.</param>
		/// <returns>ISwList</returns>
		ISwList<T> GetList<T>(string listName, SwListTemplate template = SwListTemplate.List)
			where T : SwListItem;


	}
}
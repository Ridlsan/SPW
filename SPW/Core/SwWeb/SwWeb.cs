namespace SPW
{
	using Microsoft.SharePoint;
	using SPW.Extensions;
	using SPW.Utils;
	using System;
	using System.Collections.Generic;

	/// <summary>
	///   Initiates SwWeb
	/// </summary>
	/// <seealso cref="SPW.ISwWeb" />
	internal class SwWeb : ISwWeb
	{
		/// <summary>
		///   SharePoint Web Instance
		/// </summary>
		internal readonly WebSiteContext _webContext;

		/// <summary>
		///   Cached lists on web
		/// </summary>
		private readonly Dictionary<(string listName, SPListTemplateType template), SPList> _lists =
			new Dictionary<(string listName, SPListTemplateType template), SPList>();

		private readonly ISwContext _context;

		/// <summary>
		///   Initializes a new instance of the <see cref="SwWeb" /> class.
		/// </summary>
		/// <param name="sWeb">SharePoint Web</param>
		internal SwWeb(ISwContext context, string url, string login)
		{
			this._context = context;
		}

		/// <summary>
		///   Server relative url
		/// </summary>
		public string ServerRelativeUrl => _webContext.Web.ServerRelativeUrl;

		public ISwUser CurrentUser => throw new NotImplementedException();

		/// <inheritdoc />
		public ISwDynamicList GetList(string listName, SwListTemplate template)
		{
			return new SwDynamicList(this, listName, template);
		}

		/// <inheritdoc />
		public ISwList<T> GetList<T>(string listName, SwListTemplate template = SwListTemplate.List)
			where T : SwListItem
		{
			return new SwList<T>(this, listName, template);
		}

		/// <summary>
		/// Gets SharePoint List using cache
		/// </summary>
		/// <param name="listName">Name of the list.</param>
		/// <param name="listType">Type of the list.</param>
		/// <returns></returns>
		internal SPList GetSPList(string listName, SwListTemplate listType)
		{
			var key = new Tuple<string, SwListTemplate>(listName, listType);
			if (!this._lists.ContainsKey(key))
			{
				this._lists.Add(key, this._webContext.Web.SwGetList(listName, listType));
			}

			return this._lists[key];
		}



		/// <summary>
		///		WebSiteContext
		/// </summary>
		private class WebSiteIndex
		{

		}

	}
}
namespace SPW
{
	using System;
	using Microsoft.SharePoint;
	using SPW.Common;
	using SPW.Extensions;
	using SPW.Utils;

	/// <summary>
	///   Initiates SwWeb.
	/// </summary>
	/// <seealso cref="SPW.ISwWeb" />
	internal class SwWeb : ISwWeb
	{
		private class ListsCollection : Vocabulary<(string listName, SwListTemplate template), ISwDynamicList>
		{
			public ListsCollection(SwWeb web)
			{
				this.OnMissingGet = (key) => new SwDynamicList(web, key.listName, key.template);
			}
		}

		private Lazy<SPWeb> SPWeb;

		/// <summary>
		///   Cached lists on web.
		/// </summary>
		private readonly ListsCollection lists;

		private readonly SwContext context;
		private readonly string url;
		private readonly string login;

		/// <summary>
		/// Initializes a new instance of the <see cref="SwWeb"/> class.
		/// </summary>
		/// <param name="context">SW Context.</param>
		/// <param name="url">Url.</param>
		/// <param name="login">Login.</param>
		internal SwWeb(SwContext context, string url, string login)
		{
			this.context = context;
			this.url = url;
			this.login = login;
			this.lists = new ListsCollection(this);
			this.SPWeb = new Lazy<SPWeb>(() => this.context.GetSpWeb(this.url, this.login));
		}

		/// <summary>
		///   Server relative url.
		/// </summary>
		public string ServerRelativeUrl => this.url;

		/// <inheritdoc />
		public ISwDynamicList GetList(string listName, SwListTemplate template)
		{
			return this.lists[(listName, template)];
		}

		/// <inheritdoc />
		public ISwList<T> GetList<T>(string listName, SwListTemplate template = SwListTemplate.List)
			where T : SwListItem
		{
			return new SwList<T>(this, listName, template);
		}

		/// <summary>
		/// Gets SharePoint List using cache.
		/// </summary>
		/// <param name="listName">Name of the list.</param>
		/// <param name="listTemplate">Type of the list.</param>
		/// <returns></returns>
		internal SPList GetSPList(string listName, SwListTemplate listTemplate)
		{
			return this.SPWeb.Value.SwGetList(listName, listTemplate);
		}

		/// <summary>
		///		WebSiteContext.
		/// </summary>
		private class WebSiteIndex
		{

		}

	}
}
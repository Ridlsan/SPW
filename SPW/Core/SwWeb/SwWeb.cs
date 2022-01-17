namespace SPW
{
	using Microsoft.SharePoint;
	using SPW.Common;
	using SPW.Extensions;
	using System;

	/// <summary>
	///   Initiates SwWeb.
	/// </summary>
	/// <seealso cref="SPW.ISwWeb" />
	internal class SwWeb : ISwWeb
	{
		/// <summary>
		/// Gets Context.
		/// </summary>
		public SwContext Context { get; }

		private class ListsCollection : Vocabulary<(string listName, SwListTemplate template), ISwDynamicList>
		{
			public ListsCollection(SwWeb web)
			{
				this.OnMissingGet = (key) => new SwDynamicList(web, key.listName, key.template);
			}
		}

		public Lazy<SPWeb> SPWeb { get; }

		/// <summary>
		///   Cached lists on web.
		/// </summary>
		private readonly ListsCollection lists;

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
			this.Context = context;
			this.url = url;
			this.login = login;
			this.lists = new ListsCollection(this);
			this.SPWeb = new Lazy<SPWeb>(() => this.Context.GetSpWeb(this.url, this.login));
			this.Users = new SwUsersCollection(this);
			this.Groups = new SwGroupsCollection(this);
		}

		/// <summary>
		///   Server relative url.
		/// </summary>
		public string ServerRelativeUrl => this.url;

		public ISwUser CurrentUser => new SwUser(this.SPWeb.Value.CurrentUser);

		public ISwUsersCollection Users { get; }

		public ISwGroupsCollection Groups { get; }

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
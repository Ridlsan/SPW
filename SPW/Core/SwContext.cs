namespace SPW
{
	using System;
	using System.Collections.Generic;
	using Microsoft.SharePoint;
	using SPW.Utils;

	/// <summary>
	///   SPW Context
	/// </summary>
	/// <seealso cref="ISwContext" />
	public class SwContext : ISwContext
	{
		/// <summary>
		///   The configuration provider.
		/// </summary>
		private readonly ISwConfigProvider configProvider;

		private readonly Dictionary<string, SPSite> sites = new Dictionary<string, SPSite>();

		private readonly Dictionary<(string url, string login), ISwWeb> webs =
			new Dictionary<(string url, string login), ISwWeb>();

		/// <summary>
		///   Initializes a new instance of the <see cref="SwContext" /> class.
		/// </summary>
		/// <param name="configProvider">The configuration provider.</param> 
		public SwContext(ISwConfigProvider configProvider)
		{
			this.configProvider = configProvider;
		}


		/// <summary>
		///  Return WebSiteContext.
		/// </summary>
		/// <param name="url">Url of the web realtive to site root url</param>
		/// <param name="login">User login to open web</param>
		/// <returns>Web site context</returns>
		internal WebSiteContext GetSpWeb(string url, string login = null)
		{
			return this.EnsureWeb(url, login);
		}

		private WebSiteContext EnsureWeb(string url, string login = null)
		{
			if (!this.webContexts.ContainsKey((url, login)))
			{
				if (login != null)
				{
					var currentUserWeb = this.EnsureWeb(url);
					var requestUser = currentUserWeb.Web.EnsureUser(login);
					var requestWeb = new WebSiteContext(this.configProvider.SiteUrl, url, requestUser.UserToken);
					this.webContexts.Add((url, login), requestWeb);
				}
				else
				{
					var currentContext = new WebSiteContext(this.configProvider.SiteUrl, url);
					this.webContexts.Add((url, login), currentContext);
				}
			}
			return this.webContexts[(url, login)];
		}

		public void Dispose()
		{
			foreach (var webSiteContext in this.webContexts.Values)
			{
				webSiteContext.Dispose();
			}
		}

		/// <inheritdoc />
		public ISwWeb GetWeb(string url, string login = null)
		{
			if (!this.webs.ContainsKey((url, login)))
			{
				this.webs.Add((url, login), new SwWeb(this, url, login));
			}

			return this.webs[(url, login)];
		}

		public void SubmitChanges()
		{
			throw new NotImplementedException();
		}
	}
}
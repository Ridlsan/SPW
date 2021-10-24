namespace SPW
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.SharePoint;
	using SPW.Utils;

	/// <summary>
	///  Cache for opened SPSites.
	/// </summary>
	internal class FarmCache : IDisposable
	{
		private readonly ISwConfigProvider config;
		private Dictionary<(string siteUrl, string login), SiteCache> userSitesCache =
			new Dictionary<(string siteUrl, string login), SiteCache>();

		private List<string> sortedSiteUrls;
		private bool disposedValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="FarmCache"/> class.
		/// </summary>
		/// <param name="config">Config provider.</param>
		public FarmCache(ISwConfigProvider config)
		{
			this.config = config;
			this.sortedSiteUrls = config.SiteUrls.OrderByDescending(c => c).ToList();
		}

		/// <summary>
		/// Returns Site instance from cache. Its always the same object for all webs site.
		/// </summary>
		/// <param name="webUrl">Web url.</param>
		/// <param name="login">Login.</param>
		/// <returns>SPSite instance</returns>
		internal SPWeb GetWeb(string webUrl, string login = null)
		{
			foreach (var siteUrl in this.sortedSiteUrls)
			{
				if (webUrl.StartsWith(siteUrl))
				{
					return this.EnsureSiteCache(siteUrl, login).GetWeb(webUrl);
				}
			}

			throw new Exception($"SiteUrl not found in config for web {webUrl}");
		}

		private SiteCache EnsureSiteCache(string siteUrl, string login = null)
		{
			string fullSiteUrl = this.config.ServerUrl + siteUrl;

			if (!this.userSitesCache.ContainsKey((fullSiteUrl, login)))
			{
				if (login == null)
				{
					var cachedSite = new SiteCache(fullSiteUrl);
					this.userSitesCache.Add((fullSiteUrl, login), cachedSite);
				}
				else if (login == SwConstants.SytemLogin)
				{
					this.userSitesCache.Add((fullSiteUrl, login), new SystemSiteCache(fullSiteUrl));
				}
				else
				{
					var sistemSite = this.EnsureSiteCache(fullSiteUrl, SwConstants.SytemLogin);
					var requestUser = sistemSite.RootWeb.EnsureUser(login);
					var cachedSite = new SiteCache(fullSiteUrl, requestUser.UserToken);
					this.userSitesCache.Add((fullSiteUrl, login), cachedSite);
				}
			}

			return this.userSitesCache[(fullSiteUrl, login)];

		}

		/// <inheritdoc/>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposing.
		/// </summary>
		/// <param name="disposing">If disposing or finilize.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					foreach (var site in this.userSitesCache.Values)
					{
						if (site != null)
						{
							site.Dispose();
						}
					}
				}

				this.userSitesCache = null;
				this.disposedValue = true;
			}
		}

	}
}

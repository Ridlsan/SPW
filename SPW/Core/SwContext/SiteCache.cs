namespace SPW
{
	using Microsoft.SharePoint;
	using SPW.Common;
	using System;

	/// <summary>
	///  Cache for opened SPSites.
	/// </summary>
	internal class SiteCache : IDisposable
	{
		private bool disposedValue;
		private Vocabulary<string, SPWeb> websCache = new Vocabulary<string, SPWeb>();

		private readonly string siteUrl;
		private readonly SPUserToken userToken;

		/// <summary>
		/// Initializes a new instance of the <see cref="SiteCache"/> class.
		/// </summary>
		/// <param name="siteUrl"></param>
		/// <param name="userToken"></param>
		public SiteCache(string siteUrl, SPUserToken userToken = null)
		{
			this.websCache.OnMissingGet = this.OnMissingWeb;
			this.site = new Lazy<SPSite>(this.CreateSite);
			this.siteUrl = siteUrl;
			this.userToken = userToken;
		}

		protected virtual SPSite CreateSite()
		{
			return new SPSite(this.siteUrl, this.userToken);
		}

		private Lazy<SPSite> site { get; }

		public SPSite Site => site.Value;

		/// <summary>
		/// Gets root web for SPSite.
		/// </summary>
		public SPWeb RootWeb
		{
			get
			{
				var web = this.Site.RootWeb;
				this.websCache.Add(web.ServerRelativeUrl, web);
				return web;
			}
		}

		/// <summary>
		/// Gets SPWeb from cache.
		/// </summary>
		/// <param name="webUrl">Webs Url.</param>
		/// <returns>SPWeb.</returns>
		public SPWeb GetWeb(string webUrl)
		{
			return this.websCache[webUrl];
		}

		/// <summary>
		/// Returns Web instance from cache. Its always the same object for all webs site.
		/// </summary>
		/// <param name="webUrl">Web url</param>
		/// <returns>SPWeb instance</returns>
		private SPWeb OnMissingWeb(string webUrl)
		{
			return this.site.Value.OpenWeb(webUrl);
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
					foreach (var web in this.websCache.Values)
					{
						web.Dispose();
					}

					if (this.site.IsValueCreated)
					{
						this.site.Value.Dispose();
					}
				}

				this.websCache = null;
				this.disposedValue = true;
			}
		}

	}
}

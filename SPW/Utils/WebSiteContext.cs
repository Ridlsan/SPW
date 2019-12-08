namespace SPW.Utils
{
	using System;

	using Microsoft.SharePoint;

	/// <summary>
	///   Provides with web and site initialization
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public class WebSiteContext : IDisposable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WebSiteContext"/> class.
		/// </summary>
		/// <param name="webUrl">Full url for webSite or relative, than the root will be added from configProvider</param>
		/// <param name="configProvider">The configuration provider.</param>
		public WebSiteContext(string webUrl, ISwConfigProvider configProvider)
		{
			var fullUrl = webUrl;
			if (!webUrl.StartsWith(configProvider.SiteUrl, StringComparison.InvariantCulture))
			{
				if (!webUrl.StartsWith("/"))
				{
					fullUrl = "/" + fullUrl;
				}

				fullUrl = configProvider.SiteUrl + fullUrl;
			}

			this.Site = new SPSite(fullUrl);
			this.Web = this.Site.OpenWeb();
			this.Web.AllowUnsafeUpdates = true;
		}

		/// <summary>
		///   Web Site.
		/// </summary>
		public SPSite Site { get; private set; }

		/// <summary>
		///   Web.
		/// </summary>
		public SPWeb Web { get; private set; }

		/// <inheritdoc />
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///   Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing">
		///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
		///   unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Site?.Dispose();
				this.Web?.Dispose();
			}
		}
	}
}
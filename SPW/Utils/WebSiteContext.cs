namespace SPW.Utils
{
	using Microsoft.SharePoint;
	using System;

	/// <summary>
	///   Provides with web and site initialization.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public class WebSiteContext : IDisposable
	{



		/// <summary>
		/// Initializes a new instance of the <see cref="WebSiteContext"/> class.
		/// </summary>
		/// <param name="webUrl">Full url for webSite or relative, than the root will be added from configProvider.</param>
		/// <param name="configProvider">The configuration provider.</param>
		public WebSiteContext(string serverUrl, string webUrl, SPUserToken userToken = null)
		{
			var fullUrl = webUrl;
			if (!webUrl.StartsWith(serverUrl, StringComparison.InvariantCulture))
			{
				if (!webUrl.StartsWith("/"))
				{
					fullUrl = "/" + fullUrl;
				}

				fullUrl = serverUrl + fullUrl;
			}

			this.Site = new SPSite(fullUrl, userToken);
			this.Web = this.Site.OpenWeb();
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
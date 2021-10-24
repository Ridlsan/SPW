namespace SPW
{
	using Microsoft.SharePoint;

	/// <summary>
	///  Site created with elevatedContext.
	/// </summary>
	internal class SystemSiteCache : SiteCache
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SystemSiteCache"/> class.
		/// </summary>
		/// <param name="siteUrl">Site url</param>
		public SystemSiteCache(string siteUrl)
			: base(siteUrl, SPUserToken.SystemAccount)
		{
		}
	}
}

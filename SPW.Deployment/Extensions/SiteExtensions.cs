using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPW.Deployment.Utils;
using System.Linq;

namespace SPW.Deployment.Extensions
{
	/// <summary>
	///     Extensions for sites
	/// </summary>
	public static class SiteExtension
	{
		/// <summary>
		/// Checks if SPSite exists, then creates it
		/// </summary>
		/// <param name="webApp">SPWebApplication for creation.</param>
		/// <param name="sWeb">Web Instance</param>
		/// <param name="dbUser">User for create</param>
		/// <returns></returns>
		public static SPSite EnsureSite(this SPWebApplication webApp, SwConcreteWeb sWeb, string dbUser)
		{
			var spSite = webApp.Sites[sWeb.Site.Url];
			var isSiteExists = spSite != null &&
												 spSite.ServerRelativeUrl == "/" + sWeb.Site.Url;
			if (!isSiteExists)
			{
				var contentDatabase = webApp.ContentDatabases.Cast<SPContentDatabase>()
						.Any(i => i.Name == sWeb.Site.DataBaseName) ?
						webApp.ContentDatabases.Cast<SPContentDatabase>().First(i => i.Name == sWeb.Site.DataBaseName) :
						webApp.ContentDatabases.Add(
								webApp.Sites[0].ContentDatabase.Server,
								sWeb.Site.DataBaseName,
								null,
								"",
								100,
								200,
								0
						);

				return contentDatabase.Sites.Add(sWeb.Site.Url, sWeb.Site.Title, "", 1049, "STS#1", dbUser, dbUser, "");
			}

			return spSite;
		}

		/// <summary>
		/// Checks if web exists, if not creates it
		/// </summary>
		/// <param name="site">Site collection</param>
		/// <param name="webUrl">WebSite server relative url</param>
		/// <param name="title">Title</param>
		/// <param name="description">Web description.</param>
		/// <param name="culture">Culture</param>
		/// <param name="template">Web Template, Default is Empty</param>
		/// <returns></returns>
		public static SPWeb EnsureWeb(this SPSite site, string webUrl, string title, string description, int culture,
				string template = "STS#1")
		{
			var web = site.OpenWeb(webUrl);
			if (!web.Exists)
			{
				web = site.AllWebs.Add(webUrl, title, "", (uint)culture, template, false, false);
			}

			var memo = new Mementer(web);
			web.Title = title;
			if (memo.IsChanged())
			{
				web.Update();
			}

			return web;
		}
	}
}
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SPW.Deployment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPW.Deployment.Library
{
	public class SwFarmBuilder
	{
		private readonly List<SwConcreteWeb> _registeredWebs = new List<SwConcreteWeb>();
		private readonly string _webAppUrl;
		private readonly string _dbUser;

		private SPWebApplication _app;

		public SwFarmBuilder(string webAppUrl, string dbUser)
		{
			_webAppUrl = webAppUrl;
			_dbUser = dbUser;
		}

		public void RegisterWeb(SwConcreteWeb swWeb)
		{
			_registeredWebs.Add(swWeb);
		}

		public void Build()
		{
			_app = FindWebApplication();
			foreach (var buildingWeb in _registeredWebs)
			{
				using (var spSite = _app.EnsureSite(buildingWeb, _dbUser))
				{
					var webUrl = (_webAppUrl + buildingWeb.ServerRelativeUrl).Replace(spSite.Url, "");
					var spWeb = spSite.EnsureWeb(
							webUrl,
							buildingWeb.Title,
							buildingWeb.Description,
							buildingWeb.CultureId
					);

					EnsureLists(buildingWeb, spWeb);
				}
			}
		}

		private void EnsureLists(SwConcreteWeb buildingWeb, SPWeb spWeb)
		{
			var properties = buildingWeb.GetType().GetProperties().Where(p => p.PropertyType == typeof(SwWeb));
			foreach (var propertyInfo in properties)
			{
				var list = (SwWeb)propertyInfo.GetValue(buildingWeb);
			}
		}

		private SPWebApplication FindWebApplication()
		{
			SPFarm farm = SPFarm.Local;
			SPWebService service = farm.Services.GetValue<SPWebService>("");
			foreach (SPWebApplication webApp in service.WebApplications)
			{
				SPAlternateUrlCollection urlColl = webApp.AlternateUrls;

				foreach (SPAlternateUrl url in urlColl)
				{
					if (url.ToString() == _webAppUrl)
					{
						return webApp;
					}
				}
			}

			throw new Exception("Web Application Not Found");
		}
	}
}
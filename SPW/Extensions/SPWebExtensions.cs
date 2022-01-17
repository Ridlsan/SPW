namespace SPW.Extensions
{
	using Microsoft.SharePoint;
	using Microsoft.SharePoint.Administration.Claims;
	using Microsoft.SharePoint.Utilities;
	using SPW.Utils;
	using System;

	/// <summary>
	///   Extensions for SPWeb class.
	/// </summary>
	public static class SpWebExtensions
	{
		/// <summary>
		/// Finds list by listName and listType.
		/// </summary>
		/// <param name="spWeb">Web site.</param>
		/// <param name="listName">Url name of the list that comes after /lists/ or webUrl for libraries.</param>
		/// <param name="listType">Type of the list.</param>
		/// <returns>SharePoint list.</returns>
		/// <exception cref="ArgumentNullException">spWeb.</exception>
		public static SPList SwGetList(this SPWeb spWeb, string listName, SwListTemplate listType)
		{
			if (spWeb == null)
			{
				throw new ArgumentNullException(nameof(spWeb));
			}

			var template = SharePointUtils.ListTemplateMatch[listType];

			if (template == SPListTemplateType.DataConnectionLibrary
					|| template == SPListTemplateType.DocumentLibrary
					|| template == SPListTemplateType.HomePageLibrary
					|| template == SPListTemplateType.PictureLibrary
					|| template == SPListTemplateType.WebPageLibrary)
			{
				var spLibrary = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.Url, "/") + listName + "/");

				return spLibrary;
			}

			var spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.Url, "/lists/") + listName + "/");
			return spList;
		}

		/// <summary>
		///     Возвращает пользователя по логину. Если в семействе нет пользователя еще, то создает его.
		/// </summary>
		/// <param name="sWeb">The s web.</param>
		/// <param name="login">The login.</param>
		/// <returns></returns>
		public static SPUser MnEnsureUser(this SPWeb sWeb, string login)
		{
			if (string.IsNullOrEmpty(login) || !login.Contains("@") && !login.Contains("\\"))
			{
				return null;
			}

			var user = sWeb.MnGetUser(login);
			if (user != null)
			{
				return user;
			}

			try
			{
				var cpm = SPClaimProviderManager.Local;
				var userClaim = cpm.ConvertIdentifierToClaim(login, SPIdentifierTypes.WindowsSamAccountName);
				return sWeb.EnsureUser(userClaim.ToEncodedString());
			}
			catch
			{
				try
				{
					return sWeb.SiteUsers[login];
				}
				catch
				{
					return null;
				}
			}
		}

		/// <summary>
		///     Получает пользователя из семейства сайтов, не создавая его, если его нет.
		/// </summary>
		/// <param name="sWeb">Узел</param>
		/// <param name="login">Логин</param>
		/// <returns></returns>
		public static SPUser MnGetUser(this SPWeb sWeb, string login)
		{
			if (string.IsNullOrEmpty(login) || !login.Contains("@") && !login.Contains("\\"))
			{
				return null;
			}

			try
			{
				if (login == "sharepoint\\system")
				{
					return sWeb.Site.SystemAccount;
				}

				return sWeb.SiteUsers[login];
			}
			catch
			{
				return null;
			}
		}
	}
}
namespace SPW.Extensions
{
	using Microsoft.SharePoint;
	using Microsoft.SharePoint.Utilities;
	using SPW.Utils;
	using System;

	/// <summary>
	///   Extensions for SPWeb class
	/// </summary>
	public static class SpWebExtensions
	{
		/// <summary>
		/// Finds list by listName and listType.
		/// </summary>
		/// <param name="spWeb">Web site</param>
		/// <param name="listName">Url name of the list that comes after /lists/ or webUrl for libraries</param>
		/// <param name="listType">Type of the list</param> 
		/// <returns>SharePoint list</returns>
		/// <exception cref="ArgumentNullException">spWeb</exception>
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
	}
}
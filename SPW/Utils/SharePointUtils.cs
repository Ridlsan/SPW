namespace SPW.Utils
{
	using Microsoft.SharePoint;
	using System.Collections.Generic;

	/// <summary>
	/// Utilities for SharePoint
	/// </summary>
	public static class SharePointUtils
	{
		/// <summary>
		/// Match for templates in SharePoint and in SPW
		/// </summary>
		public static Dictionary<SwListTemplate, SPListTemplateType> ListTemplateMatch =>
			new Dictionary<SwListTemplate, SPListTemplateType>
			{
				[SwListTemplate.Library] = SPListTemplateType.DocumentLibrary,
				[SwListTemplate.List] = SPListTemplateType.GenericList,
				[SwListTemplate.TaskList] = SPListTemplateType.Tasks
			};
	}
}
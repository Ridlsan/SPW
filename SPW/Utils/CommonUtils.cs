namespace SPW.Utils
{
	using Microsoft.SharePoint;

	/// <summary>
	///   Common utilities
	/// </summary>
	public static class CommonUtils
	{
		/// <summary>
		///   Converts the sp item to sw item.
		/// </summary>
		/// <param name="spItem">The sp item.</param>
		/// <param name="fieldNames">Fields to convert.</param>
		/// <returns>Constructed SwItemData</returns>
		public static SwItemData ConvertSpItemToSwItem(SPListItem spItem, params string[] fieldNames)
		{
			var swItem = new SwItemData();
			foreach (string fieldName in fieldNames)
			{
				swItem[fieldName] = spItem[fieldName];
			}

			swItem.Id = spItem.ID;
			return swItem;
		}
	}
}
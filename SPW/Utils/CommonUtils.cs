namespace SPW.Utils
{
	using Microsoft.SharePoint;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	///   Common utilities.
	/// </summary>
	public static class CommonUtils
	{
		/// <summary>
		///   Converts the sp item to sw item.
		/// </summary>
		/// <param name="spItem">The sp item.</param>
		/// <param name="fieldNames">Fields to convert.</param>
		/// <returns>Constructed SwItemData.</returns>
		public static SwItemData FillItemDataFromSpItem(SwItemData swItem, SPListItem spItem, IEnumerable<string> fieldNames = null)
		{
			if (fieldNames == null)
			{
				fieldNames = spItem.Fields.Cast<SPField>().Select(x => x.InternalName);
			}

			foreach (string fieldName in fieldNames)
			{
				swItem[fieldName] = spItem[fieldName];
			}

			return swItem;
		}

	}
}
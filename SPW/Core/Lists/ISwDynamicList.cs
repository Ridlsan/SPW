namespace SPW
{
	using System.Collections.Generic;

	/// <summary>
	///   Dynamic List properties.
	/// </summary>
	public interface ISwDynamicList
	{
		/// <summary>
		/// Creates the specified item.
		/// </summary>
		/// <param name="sItem">Item to create.</param>
		/// <returns>Added SharePoint item</returns>
		SwItemData Add(SwItemData sItem);

		/// <summary>
		/// Creates the specified item.
		/// </summary>
		/// <param name="sItem">Item to update.</param>
		/// <param name="props">Update properties.</param>
		/// <returns>
		/// Updated item.
		/// </returns>
		SwItemData Update(SwItemData sItem, UpdateProps props = null);

		/// <summary>
		///   Recycles item with specified id.
		/// </summary>
		/// <param name="sItemId">ID of the item.</param>
		void Recycle(int sItemId);

		/// <summary>
		///   Recycles item with specified id.
		/// </summary>
		/// <param name="sItemId">ID of the item.</param>
		void Delete(int sItemId);

		/// <summary>
		///   Gets item from list by ID.
		/// </summary>
		/// <param name="sItemId">ID of the item.</param>
		/// <param name="fieldNames">The field names.</param>
		/// <returns>Found item.</returns>
		SwItemData Find(int sItemId, params string[] fieldNames);

		/// <summary>
		///   Finds item with CAML query.
		/// </summary>
		/// <param name="camlQuery">The caml query.</param>
		/// <param name="viewFields">The view fields.</param>
		/// <returns>Collection of items.</returns>
		IEnumerable<SwItemData> GetItems(string camlQuery, string viewFields);

		/// <summary>
		/// Doeses the user has permissions.
		/// </summary>
		/// <param name="swItem">The sw item.</param>
		/// <param name="permission">The permission.</param>
		/// <param name="user">The user.</param>
		/// <returns>True if has permissions.</returns>
		bool DoesUserHasPermissions(SwItemData swItem, ListItemRights permission, ISwUser user = null);
	}
}
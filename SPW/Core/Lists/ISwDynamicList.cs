namespace SPW
{
	using System.Collections.Generic;

	/// <summary>
	///   Dynamic List properties.
	/// </summary>
	public interface ISwDynamicList
	{
		/// <summary>
		///   Creates the specified item.
		/// </summary>
		/// <param name="sItem">Item to create.</param>
		/// <returns>Created item.</returns>
		void InsertOnSubmit(SwItemData sItem);


		/// <summary>
		///   Creates the specified item.
		/// </summary>
		/// <param name="sItem">Item to create.</param>
		/// <returns>Created item.</returns>
		void UpdateOnSubmit(SwItemData sItem);

		/// <summary>
		///   Deletes item from list with specified identifier.
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
		///   Recycles item with specified id.
		/// </summary>
		/// <param name="sItemId">ID of the item.</param>
		void Recycle(int sItemId);

		/// <summary>
		///  Iterates items in list.
		/// </summary>
		IEnumerable<SwItemData> Items { get; }
	}
}
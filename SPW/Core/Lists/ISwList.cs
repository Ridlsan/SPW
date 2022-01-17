namespace SPW
{
	/// <summary>
	///   Strongly Typed List Instance.
	/// </summary>
	/// <typeparam name="T">Class that derives from SwListItem.</typeparam>
	public interface ISwList<T>
		where T : SwListItem
	{
		/// <summary>
		/// Creates the specified item.
		/// </summary>
		/// <param name="sItem">Item to create.</param>
		/// <returns>Created item.</returns>
		T Add(T sItem);

		/// <summary>
		/// Gets item from list by ID.
		/// </summary>
		/// <param name="sItemId">ID of the item.</param>
		/// <returns>Found item.</returns>
		T Find(int sItemId);

		/// <summary>
		/// Recycles item with specified id.
		/// </summary>
		/// <param name="sItemId">ID of the item.</param>
		void Recycle(int sItemId);

		/// <summary>
		/// Updates item in SP list with current values.
		/// </summary>
		/// <param name="sItem">Item that needs to updated.</param>
		/// <returns>Updated item.</returns>
		T Update(T sItem);
	}
}
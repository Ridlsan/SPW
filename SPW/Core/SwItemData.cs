namespace SPW
{
	using Microsoft.SharePoint;
	using System.Collections.Generic;

	/// <summary>
	///   Represents List Item Data in dynamic fashion
	/// </summary>
	/// <seealso>
	///   <cref>Dictionary{string, object}</cref>
	/// </seealso>
	public class SwItemData : Dictionary<string, object>
	{
		/// <summary>
		///  Item from SP
		/// </summary>
		internal SPListItem SpItem { get; set; }

		/// <summary>
		///		Id of the item
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		///		Title of the item
		/// </summary>
		public string Title { get; private set; }
	}
}
namespace SPW
{
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
		/// Id of the item
		/// </summary>
		public int Id { get; internal set; }
	}
}
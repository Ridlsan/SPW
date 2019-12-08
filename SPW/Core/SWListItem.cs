namespace SPW
{
	using SPW.SwListItemAttributes;

	/// <summary>
	///   Strongly Typed SharePoint item
	/// </summary>
	public class SwListItem
	{
		/// <summary>
		///   ID.
		/// </summary>
		[SwField]
		public int ID { get; internal set; }
	}
}
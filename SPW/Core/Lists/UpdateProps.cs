namespace SPW
{
	/// <summary>
	/// Properties to update item.
	/// </summary>
	public class UpdateProps
	{
		/// <summary>
		/// Should system check and give rights to element, so user can change it.
		/// </summary>
		public bool EnsureCanEdit { get; set; } = false;

		/// <summary>
		/// Type of update action.
		/// </summary>
		public UpdateType UpdateType { get; set; } = UpdateType.Common;

		/// <summary>
		/// Force to update if no fields are changed.
		/// </summary>
		public bool ForceUpdate { get; set; } = false;
	}


	/// <summary>
	/// Type of update.
	/// </summary>
	public enum UpdateType
	{
		/// <summary>
		/// The common
		/// </summary>
		Common,

		/// <summary>
		/// The system
		/// </summary>
		System,

		/// <summary>
		/// The not update version
		/// </summary>
		NotUpdateVersion
	}
}
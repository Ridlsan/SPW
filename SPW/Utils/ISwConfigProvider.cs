namespace SPW.Utils
{
	/// <summary>
	///   Config provider for SPW.
	/// </summary>
	public interface ISwConfigProvider
	{
		/// <summary>
		///   Returns site URL
		/// </summary>
		string SiteUrl { get; }
	}
}
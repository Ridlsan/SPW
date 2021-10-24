using System.Collections.Generic;

namespace SPW.Utils
{
	/// <summary>
	///   Config provider for SPW.
	/// </summary>
	public interface ISwConfigProvider
	{
		/// <summary>
		///   Returns site URL.
		/// </summary>
		string ServerUrl { get; }

		/// <summary>
		///  Returns all site server-relative urls.
		/// </summary>
		List<string> SiteUrls { get; }
	}
}
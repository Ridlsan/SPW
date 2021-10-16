using System;

namespace SPW
{
	/// <summary>
	///   Farm context, that allows to get webs
	/// </summary>
	public interface ISwContext : IDisposable
	{
		/// <summary>
		///   Gets web for specified url
		/// </summary>
		/// <param name="url">Site root relative url.</param>
		/// <returns>The ISwWeb</returns>
		ISwWeb GetWeb(string url, string login = null);

		/// <summary>
		/// Submits all unsaved items
		/// </summary>
		void SubmitChanges();

	}

}
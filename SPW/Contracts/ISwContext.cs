namespace SPW
{
	/// <summary>
	///   Farm context, that allows to get webs
	/// </summary>
	public interface ISwContext
	{
		/// <summary>
		///   Gets web for specified url
		/// </summary>
		/// <param name="url">Site root relative url.</param>
		/// <returns>The ISwWeb</returns>
		ISwWeb Web(string url);
	}
}
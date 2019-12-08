namespace SPW.Examples
{
	using System.Configuration;

	using SPW.Utils;

	/// <summary>
	///   Config Provider
	/// </summary>
	/// <seealso cref="SPW.Utils.ISwConfigProvider" />
	public class ConfigProvider : ISwConfigProvider
	{
		/// <inheritdoc />
		public string SiteUrl => ConfigurationManager.AppSettings["serverUrl"];
	}
}
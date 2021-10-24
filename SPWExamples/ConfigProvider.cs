namespace SPW.Examples
{
	using SPW.Utils;
	using System.Configuration;

	/// <summary>
	///   Config Provider
	/// </summary>
	/// <seealso cref="SPW.Utils.ISwConfigProvider" />
	public class ConfigProvider : ISwConfigProvider
	{
		/// <inheritdoc />
		public string ServerUrl => ConfigurationManager.AppSettings["serverUrl"];
	}
}
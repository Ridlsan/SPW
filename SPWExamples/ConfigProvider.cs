namespace SPW.Examples
{
	using Newtonsoft.Json;
	using SPW.Utils;
	using System.Collections.Generic;
	using System.Configuration;

	/// <summary>
	///   Config Provider
	/// </summary>
	/// <seealso cref="SPW.Utils.ISwConfigProvider" />
	public class ConfigProvider : ISwConfigProvider
	{
		/// <inheritdoc />
		public string ServerUrl
		{
			get
			{
				return "http://test";
			}
		}

		public List<string> SiteUrls
		{
			get
			{
				return new List<string>
				{
					""
				};
			}
		}
	}
}
namespace SPW
{
	using Microsoft.SharePoint;
	using SPW.Common;
	using SPW.Utils;

	/// <summary>
	///   SPW Context.
	/// </summary>
	/// <seealso cref="ISwContext" />
	public class SwContext : ISwContext
	{
		/// <summary>
		///   The configuration provider.
		/// </summary>
		private readonly ISwConfigProvider configProvider;

		private readonly FarmCache farm;

		private readonly Vocabulary<(string url, string login), SwWeb> createdSwWebs = new Vocabulary<(string url, string login), SwWeb>();

		/// <summary>
		///   Initializes a new instance of the <see cref="SwContext" /> class.
		/// </summary>
		/// <param name="configProvider">The configuration provider.</param>
		public SwContext(ISwConfigProvider configProvider)
		{
			this.configProvider = configProvider;
			this.farm = new FarmCache(this.configProvider);
			this.createdSwWebs.OnMissingGet = ((string url, string login) key) =>
			{
				return new SwWeb(this, key.url, key.login);
			};
		}

		/// <summary>
		///  Return WebSiteContext.
		/// </summary>
		/// <param name="url">Url of the web realtive to site root url.</param>
		/// <param name="login">User login to open web.</param>
		/// <returns>Web site context.</returns>
		internal SPWeb GetSpWeb(string url, string login = null)
		{
			return this.farm.GetWeb(url, login);
		}

		public void Dispose()
		{
			this.farm.Dispose();
		}

		/// <inheritdoc />
		public ISwWeb GetWeb(string url, string login = null)
		{
			return new SwWeb(this, url, login);
		}
	}


	/// <summary>
	/// File Operations.
	/// </summary>
	internal enum OperationType
	{
		None = 0,
		Add = 15,
		Update = 30,
		Recycle = 40,
		Delete = 50,
	}

}

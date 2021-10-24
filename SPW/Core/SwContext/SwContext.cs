namespace SPW
{
	using Microsoft.SharePoint;
	using SPW.Utils;

	/// <summary>
	///   SPW Context.
	/// </summary>
	/// <seealso cref="ISwContext" />
	internal class SwContext : ISwContext
	{
		/// <summary>
		///   The configuration provider.
		/// </summary>
		private readonly ISwConfigProvider configProvider;

		private readonly FarmCache farm;

		private readonly ChangesManager changesManager = new ChangesManager();

		/// <summary>
		///   Initializes a new instance of the <see cref="SwContext" /> class.
		/// </summary>
		/// <param name="configProvider">The configuration provider.</param>
		public SwContext(ISwConfigProvider configProvider)
		{
			this.configProvider = configProvider;
			this.farm = new FarmCache(this.configProvider);
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

		public void AddChange(SwDynamicList list, SwItemData swItem, OperationType operationType)
		{
			this.changesManager.AddChange(list, swItem, operationType);
		}

		public void SubmitChanges()
		{
			this.changesManager.RunChanges();
		}
	}

	/// <summary>
	/// File Operations.
	/// </summary>
	internal enum OperationType
	{
		Add = 0,
		Update = 1,
		Recycle = 2,
		Delete = 3,
	}
}
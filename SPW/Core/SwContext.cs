namespace SPW
{
	using SPW.Utils;

	/// <summary>
	///   SPW Context
	/// </summary>
	/// <seealso cref="ISwContext" />
	public class SwContext : ISwContext
	{
		/// <summary>
		///   The configuration provider
		/// </summary>
		private readonly ISwConfigProvider configProvider;

		/// <summary>
		///   Initializes a new instance of the <see cref="SwContext" /> class.
		/// </summary>
		/// <param name="configProvider">The configuration provider.</param>
		public SwContext(ISwConfigProvider configProvider)
		{
			this.configProvider = configProvider;
		}

		/// <inheritdoc />
		public ISwWeb Web(string url)
		{
			return new SwWeb(new WebSiteContext(url, this.configProvider).Web);
		}
	}
}
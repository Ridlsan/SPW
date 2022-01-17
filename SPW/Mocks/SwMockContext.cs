namespace SPW.Mocks
{
	using SPW.Common;

	/// <summary>
	/// Mocked context.
	/// </summary>
	/// <seealso cref="SPW.ISwContext" />
	public class SwMockContext : ISwContext
	{
		/// <summary>
		/// Mocked webs collection.
		/// </summary>
		public virtual Vocabulary<string, SwMockWeb> MockedWebs { get; set; }
			= new Vocabulary<string, SwMockWeb>();

		/// <inheritdoc/>
		public ISwWeb GetWeb(string url, string login = null)
		{
			return new SwMockUserWeb(this.MockedWebs[url], login, url);
		}

		/// <inheritdoc/>
		public void Dispose()
		{
		}
	}
}

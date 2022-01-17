namespace SPW
{
	public abstract class SwConcreteWeb
	{
		protected readonly ISwContext _context;

		public SwConcreteWeb(ISwContext context)
		{
			_context = context;
		}

		public abstract string Url { get; }

		/// <summary>
		/// Returns web isntance.
		/// </summary>
		public ISwWeb Web => this._context.GetWeb(this.Url);
	}
}
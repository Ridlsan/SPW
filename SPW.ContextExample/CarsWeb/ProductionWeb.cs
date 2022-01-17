namespace SPW.ContextExample.CarsWeb
{
	public class ProductionWeb : SwConcreteWeb
	{
		/// <inheritdoc />
		public ProductionWeb(ISwContext context)
				: base(context)
		{
		}


		/// <inheritdoc />
		public override string Url => "production";

		public ISwList<Car> Cars => this.Web.GetList<Car>("cars");
	}
}
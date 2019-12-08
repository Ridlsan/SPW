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
        public override string ServerRelativeUrl => "production";

        /// <inheritdoc />
        public override string Title => "Производство";

        public ISwList<Car> Cars => _sWeb.Value.GetList<Car>("cars");
    }
}
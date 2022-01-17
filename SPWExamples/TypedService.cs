using SPW.ContextExample.CarsWeb;
using System;
using System.Linq;

namespace SPW.Examples
{
	public class TypedService
	{
		private readonly ProductionWeb productionWeb;

		public TypedService(ProductionWeb productionWeb)
		{
			this.productionWeb = productionWeb;
		}

		public void CreateAndUpdate()
		{
			ISwList<Car> cars = productionWeb.Cars;
			var sItem = cars.Add(new Car { Produced = DateTime.Today });
			var newItem = cars.Find(sItem.ID);
			newItem.Produced = DateTime.Parse("01.01.2000");
			cars.Update(newItem);
		}

	}
}
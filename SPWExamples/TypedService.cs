using SPW.ContextExample.CarsWeb;
using SPW.Examples.Context;
using System;
using System.Linq;

namespace SPW.Examples
{
	public class TypedService
	{
		private readonly ISwContext _swContext;

		public TypedService(ISwContext swContext)
		{
			_swContext = swContext;
		}

		public void CreateAndUpdate()
		{
			using (var myWeb = new MyWeb(_swContext))
			{
				var sItem = myWeb.Cars.Create(new Car { Produced = DateTime.Today });
				var newItem = myWeb.Cars.Find(sItem.ID);
				newItem.Produced = DateTime.Parse("01.01.2000");
				myWeb.Cars.Update(newItem);
			}
		}

		public void LinqGetItem()
		{
			using (var myWeb = new MyWeb(_swContext))
			{
				var id = 1;
				var myCar = from car in myWeb.Cars
										select car;

				foreach (var car in myCar)
				{
					Console.WriteLine(car.Title);
				}
			}
		}
	}
}
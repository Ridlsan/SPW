using SPW.ContextExample.CarsWeb;
using SPW.ExamplesTests.MockContext.MyWeb;
using SPW.Mocks;
using System;
using System.Linq;
using Xunit;

namespace SPW.Examples.Tests
{
	public class TypedServiceTests
	{


		[Fact]
		public void RunTest()
		{
			var mockContext = new SwMockContext();
			var myWeb = new MockMyWeb();
			var carsList = myWeb.MockLists[("cars", SwListTemplate.List)];
			carsList.Items.Clear();
			mockContext.MockedWebs.Add("production", myWeb);

			var service = new TypedService(new ProductionWeb(mockContext));
			service.CreateAndUpdate();
			Assert.Single(carsList.Items);
			var addedRow = carsList.Items.First();
			Assert.Equal(DateTime.Parse("01.01.2000"), addedRow.Value.ValDate("Produced"));
		}
	}
}
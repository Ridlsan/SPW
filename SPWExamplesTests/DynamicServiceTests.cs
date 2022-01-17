using SPW.ExamplesTests.MockContext.MyWeb;
using SPW.Mocks;
using System;
using System.Linq;
using Xunit;
using static Xunit.Assert;

namespace SPW.Examples.Tests
{
	public class DynamicServiceTests
	{
		[Fact]
		public void RunTest()
		{
			var mockContext = new SwMockContext();
			var myWeb = new MockMyWeb();
			var carsList = myWeb.MockLists[("cars", SwListTemplate.List)];
			carsList.Items.Clear();
			mockContext.MockedWebs.Add("production", myWeb);

			var service = new DynamicService(mockContext);
			service.CreateAndUpdateItem();
			Assert.Single(carsList.Items);
			var addedRow = carsList.Items.First();
			Equal(DateTime.Parse("01.01.2000"), addedRow.Value.ValDate("Produced"));
		}
	}
}
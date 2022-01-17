using SPW.Common;
using SPW.Mocks;
using System;

namespace SPW.ExamplesTests.MockContext.MyWeb
{
	class MockCarsList : SwMockList
	{
		public MockCarsList()
		{
			this.Items.Add(2, new SwItemData
			{
				["Produced"] = DateTime.Now.AddDays(-4),
				[SpCommonList.Title] = "Volvo"
			});
		}
	}
}

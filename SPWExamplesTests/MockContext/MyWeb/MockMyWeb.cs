using SPW.Mocks;

namespace SPW.ExamplesTests.MockContext.MyWeb
{
	/// <summary>
	/// MockWeb
	/// </summary>
	/// <seealso cref="SPW.Mocks.SwMockWeb" />
	public class MockMyWeb : SwMockWeb
	{
		public MockMyWeb()
		{
			this.MockLists.Add(("cars", SwListTemplate.List), new MockCarsList());
		}
	}
}

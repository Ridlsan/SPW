using System.Collections.Generic;

namespace SPW.Mocks
{
	public class SwMockContext : ISwContext
	{
		public Dictionary<string, SwMockWeb> Webs = new Dictionary<string, SwMockWeb>();

		/// <inheritdoc />
		public ISwWeb Web(string url)
		{
			return Webs[url];
		}

		public void RegisterWeb(SwMockWeb swMockWeb)
		{
			Webs.Add(swMockWeb.ServerRelativeUrl, swMockWeb);
		}
	}
}
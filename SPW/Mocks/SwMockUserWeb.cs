namespace SPW.Mocks
{

	public class SwMockUserWeb : ISwWeb
	{
		private readonly SwMockWeb web;
		private readonly string login;
		private readonly string url;

		public SwMockUserWeb(SwMockWeb web, string login, string url)
		{
			this.web = web;
			this.login = login;
			this.url = url;
		}

		public string ServerRelativeUrl => this.url;

		public ISwUser CurrentUser => new SwMockUser(this.login);

		public ISwUsersCollection Users { get; } = new SwMockUserCollection();

		public ISwGroupsCollection Groups { get; set; } = new SwMockGroupCollection();

		public ISwDynamicList GetList(string listName, SwListTemplate template = SwListTemplate.List)
		{
			return this.web.GetList(listName, template);
		}

		public ISwList<T> GetList<T>(string listName, SwListTemplate template = SwListTemplate.List) where T : SwListItem
		{
			return this.web.GetList<T>(listName, template);

		}
	}
}
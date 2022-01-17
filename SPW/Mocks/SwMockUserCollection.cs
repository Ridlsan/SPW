namespace SPW.Mocks
{
	class SwMockUserCollection : ISwUsersCollection
	{
		public ISwUser this[string login] => new SwMockUser(login);
	}
}
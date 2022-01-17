namespace SPW.Mocks
{
	class SwMockGroupCollection : ISwGroupsCollection
	{
		public ISwGroup this[string groupName] => new SwMockGroup(groupName);
	}
}
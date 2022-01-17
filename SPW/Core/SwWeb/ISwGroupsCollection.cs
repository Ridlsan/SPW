namespace SPW
{
	public interface ISwGroupsCollection
	{
		ISwGroup this[string login] { get; }
	}
}
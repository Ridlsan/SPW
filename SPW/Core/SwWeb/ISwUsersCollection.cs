namespace SPW
{
	public interface ISwUsersCollection
	{
		ISwUser this[string login] { get; }
	}
}
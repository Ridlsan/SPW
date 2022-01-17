namespace SPW
{
	public interface ISwGroup : ISwPrincipal
	{
		bool ContainsUser(ISwUser swUser);

		void AddUser(ISwUser swUser);
	}
}
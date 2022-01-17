namespace SPW
{
	public interface ISwUser : ISwPrincipal
	{

		/// <summary>
		/// Gets the login.
		/// </summary>
		string Login { get; }
	}
}
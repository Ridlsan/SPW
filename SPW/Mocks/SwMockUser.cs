namespace SPW.Mocks
{
	public class SwMockUser : ISwUser
	{
		/// <summary>
		/// User login.
		/// </summary>
		public string Login { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SwMockUser"/> class.
		/// </summary>
		/// <param name="login">The login.</param>
		public SwMockUser(string login)
		{
			this.Login = login;
		}
	}
}

using SPW.Extensions;

namespace SPW
{
	public class SwUsersCollection : ISwUsersCollection
	{
		private SwWeb SWeb { get; }

		internal SwUsersCollection(SwWeb sWeb)
		{
			this.SWeb = sWeb;
		}

		/// <summary>
		///     Gets or sets the <see cref="object" /> with the specified field.
		/// </summary>
		/// <value>
		///     The <see cref="object" />.
		/// </value>
		/// <param name="login">The field.</param>
		/// <returns></returns>
		public ISwUser this[string login]
		{
			get
			{
				var user = this.SWeb.SPWeb.Value.MnEnsureUser(login);
				if (user == null)
				{
					return null;
				}

				return new SwUser(user);
			}
		}
	}
}
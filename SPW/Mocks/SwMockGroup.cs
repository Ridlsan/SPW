using System.Collections.Generic;

namespace SPW.Mocks
{
	public class SwMockGroup : ISwGroup
	{
		/// <summary>
		/// User login.
		/// </summary>
		public string GroupName { get; }

		/// <summary>
		/// User login.
		/// </summary>
		public List<string> Users { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SwMockGroup"/> class.
		/// </summary>
		/// <param name="groupName">The login.</param>
		public SwMockGroup(string groupName)
		{
			this.GroupName = groupName;
		}

		public bool ContainsUser(ISwUser swUser)
		{
			return this.Users.Contains(swUser.Login);
		}

		public void AddUser(ISwUser swUser)
		{
			this.Users.Add(swUser.Login);
		}
	}
}

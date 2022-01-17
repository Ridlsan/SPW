using Microsoft.SharePoint;
using SPW.Extensions;

namespace SPW
{
	public class SwUser : ISwUser
	{

		internal SPUser SpUser { get; private set; }

		public string Login => this.SpUser.Login();

		internal SwUser(SPUser user)
		{
			this.SpUser = user;
		}
	}
}

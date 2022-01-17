using Microsoft.SharePoint;
using System;
using System.Linq;

namespace SPW
{
	public class SwGroup : ISwGroup
	{
		internal SPGroup SpGroup { get; private set; }
		internal SwGroup(SPGroup group)
		{
			this.SpGroup = group;
		}

		public bool ContainsUser(ISwUser swUser)
		{
			var user = (SwUser)swUser;
			return user.SpUser.Groups.Cast<SPGroup>()
							.Any(
									g =>
											string.Equals(g.Name, this.SpGroup.Name ?? "", StringComparison.CurrentCultureIgnoreCase)
							);
		}

		public void AddUser(ISwUser swUser)
		{
			this.SpGroup.AddUser(((SwUser)swUser).SpUser);
		}
	}
}

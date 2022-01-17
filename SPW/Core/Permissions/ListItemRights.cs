using System;

namespace SPW
{
	[Flags]
	public enum ListItemRights
	{
		None = 0,
		View = 1,
		Edit = 2,
		Delete = 4,
	}
}

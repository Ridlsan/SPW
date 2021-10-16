using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;

namespace SPW.Extensions
{
	/// <summary>
	///   Extensions for SPUser
	/// </summary>
	public static class SPUserExtensions
	{
		/// <summary>
		///     Настоящий логин пользователя, без Claim Provider меток.
		/// </summary>
		/// <param name="user">SPUser</param>
		/// <returns></returns>
		public static string Login(this SPUser user)
		{
			if (user.LoginName.Contains("|"))
			{
				var manager = SPClaimProviderManager.Local;
				if (manager != null)
				{
					return SPClaimProviderManager.IsEncodedClaim(user.LoginName) ?
							manager.DecodeClaim(user.LoginName).Value :
							user.LoginName;
				}

				return user.LoginName.ToLowerInvariant();
			}

			return user.LoginName.ToLowerInvariant();
		}
	}
}

using SPW.Extensions;

namespace SPW
{
	public class SwGroupsCollection : ISwGroupsCollection
	{
		private SwWeb SWeb { get; }
		internal SwGroupsCollection(SwWeb sWeb)
		{
			SWeb = sWeb;
		}

		/// <summary>
		///     Gets or sets the <see cref="object" /> with the specified field.
		/// </summary>
		/// <value>
		///     The <see cref="object" />.
		/// </value>
		/// <param name="groupName">The field.</param>
		/// <returns></returns>
		public new ISwGroup this[string groupName]
		{
			get
			{
				try
				{
					var group = this.SWeb.SPWeb.Value.SiteGroups[groupName];
					return new SwGroup(group);
				}
				catch (System.Exception)
				{
					return null;
				}
			}
		}
	}
}
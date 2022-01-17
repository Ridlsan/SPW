using System.Collections.Generic;
using System.Linq;

namespace SPW.Mocks
{
	public delegate bool OnGetPermissions(SwItemData swItem, ListItemRights permission, ISwUser user = null);

	public class SwMockList : ISwDynamicList
	{

		public virtual Dictionary<int, SwItemData> Items { get; set; }
		= new Dictionary<int, SwItemData>();

		public SwItemData Add(SwItemData sItem)
		{
			sItem.ID = this.Items.Any() ? this.Items.Max(i => i.Key) + 1 : 1;
			this.Items.Add(sItem.ID, sItem);
			return sItem;
		}

		public void Delete(int sItemId)
		{
			Items.Remove(sItemId);
		}

		public OnGetPermissions OnGetPermissions { get; set; }

		public bool DoesUserHasPermissions(SwItemData swItem, ListItemRights permission, ISwUser user = null)
		{
			return this.OnGetPermissions(swItem, permission, user);
		}

		public SwItemData Find(int sItemId, params string[] fieldNames)
		{
			return this.Items[sItemId];
		}

		public IEnumerable<SwItemData> GetItems(string camlQuery, string viewFields)
		{
			return this.Items.Values;
		}

		public void Recycle(int sItemId)
		{
			Items.Remove(sItemId);

		}

		public SwItemData Update(SwItemData sItem)
		{
			this.Items[sItem.ID] = sItem;
			return sItem;
		}
	}
}
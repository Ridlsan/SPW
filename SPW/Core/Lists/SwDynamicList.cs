namespace SPW
{
	using Microsoft.SharePoint;
	using SPW.CamlBuilder;
	using SPW.Extensions;
	using SPW.Utils;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Dynamic list implementation.
	/// </summary>
	/// <seealso cref="SPW.SwListBase" />
	/// <seealso cref="SPW.ISwDynamicList" />
	internal class SwDynamicList : SwListBase, ISwDynamicList
	{
		private Dictionary<int, SPListItem> populatedItems = new Dictionary<int, SPListItem>();

		/// <summary>
		/// Initializes a new instance of the <see cref="SwDynamicList"/> class.
		/// </summary>
		/// <param name="swWeb">IswWeb.</param>
		/// <param name="listName">Name of the list (url part after "/lists/" or webUrl for libs).</param>
		/// <param name="template">Template of the list.</param>
		public SwDynamicList(SwWeb swWeb, string listName, SwListTemplate template = SwListTemplate.List)
			: base(swWeb, listName, template)
		{
		}

		private SwItemData Find(int sItemId, IEnumerable<string> fields)
		{
			return CommonUtils.FillItemDataFromSpItem(new SwItemData(), this.GetItemById(sItemId), fields);
		}

		private SPListItem GetItemById(int sItemId)
		{
			if (this.populatedItems.ContainsKey(sItemId))
			{
				return this.populatedItems[sItemId];
			}

			return this.SpList.Value.GetItemById(sItemId);
		}

		/// <inheritdoc />
		public IEnumerable<SwItemData> GetItems(string query, string viewFields)
		{
			var spQuery = new SPQuery { Query = query, ViewFields = viewFields };
			var items = this.SpList.Value.GetItems(spQuery);
			var fieldRefs = $"<ViewFields>{viewFields}</ViewFields>".ParseXML<ViewFields>();
			var fieldNames = fieldRefs.FieldRef.Select(i => i.Name);
			return items.Cast<SPListItem>().Select(i => CommonUtils.FillItemDataFromSpItem(new SwItemData(), i, fieldNames.ToArray()));
		}

		/// <inheritdoc />
		public SwItemData Update(SwItemData swItem, UpdateProps props = null)
		{
			var spNewItem = this.SpList.Value.GetItemById(swItem.ID);
			foreach (var fieldName in swItem.Keys)
			{
				spNewItem[fieldName] = swItem[fieldName];
			}

			if (!spNewItem.DoesUserHavePermissions(SPBasePermissions.EditListItems))
			{
				var list = this.Web.Context.GetWeb(this.Web.ServerRelativeUrl, SwConstants.SytemLogin).GetList(this.ListName, this.Template) as SwDynamicList;
				var item = list.Find(swItem.ID);
			}

			// TODO replace with enhanced update
			spNewItem.Update();
			return CommonUtils.FillItemDataFromSpItem(swItem, spNewItem);
		}

		/// <inheritdoc/>
		public SwItemData Add(SwItemData sItem)
		{
			var spNewItem = this.SpList.Value.AddItem();
			foreach (var fieldName in sItem.Keys)
			{
				spNewItem[fieldName] = sItem[fieldName];
			}

			spNewItem.Update();
			return CommonUtils.FillItemDataFromSpItem(sItem, spNewItem);
		}

		/// <inheritdoc/>
		public SwItemData Find(int sItemId, params string[] fieldNames)
		{
			return this.Find(sItemId, fieldNames.ToList());
		}

		/// <inheritdoc/>
		public bool DoesUserHasPermissions(SwItemData swItem, ListItemRights permission, ISwUser user = null)
		{
			var spItem = this.GetItemById(swItem.ID);
			var rights = SPBasePermissions.EmptyMask;

			if ((permission & ListItemRights.Delete) != 0)
			{
				rights |= SPBasePermissions.DeleteListItems;
			}

			if ((permission & ListItemRights.Edit) != 0)
			{
				rights |= SPBasePermissions.EditListItems;
			}

			if ((permission & ListItemRights.View) != 0)
			{
				rights |= SPBasePermissions.ViewListItems;
			}

			if (user != null)
			{
				return spItem.DoesUserHavePermissions((user as SwUser).SpUser, rights);
			}

			return spItem.DoesUserHavePermissions(rights);
		}

	}
}
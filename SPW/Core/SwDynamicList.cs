namespace SPW
{
	using System.Collections.Generic;
	using System.Linq;

	using Microsoft.SharePoint;

	using SPW.CamlBuilder;
	using SPW.Extensions;
	using SPW.Utils;

	internal class SwDynamicList : SwListBase, ISwDynamicList
	{
		public SwDynamicList(ISwWeb swWeb, string listName, SwListTemplate template = SwListTemplate.List)
			: base(swWeb, listName, template)
		{
		}

		/// <inheritdoc />
		public SwItemData Create(SwItemData sItem)
		{
			var spNewItem = this.SpList.Value.AddItem();
			foreach (var fieldName in sItem.Keys)
			{
				spNewItem[fieldName] = sItem[fieldName];
			}

			spNewItem.Update();
			return CommonUtils.ConvertSpItemToSwItem(spNewItem, sItem.Keys.ToArray());
		}

		public SwItemData Find(int sItemId, params string[] values)
		{
			return CommonUtils.ConvertSpItemToSwItem(this.SpList.Value.GetItemById(sItemId), values);
		}

		/// <inheritdoc />
		public IEnumerable<SwItemData> GetItems(string query, string viewFields)
		{
			var spQuery = new SPQuery { Query = query, ViewFields = viewFields };

			var items = this.SpList.Value.GetItems(spQuery);
			var fieldRefs = $"<ViewFields>{viewFields}</ViewFields>".ParseXML<ViewFields>();
			var fieldNames = fieldRefs.FieldRef.Select(i => i.Name);
			return items.Cast<SPListItem>().Select(i => CommonUtils.ConvertSpItemToSwItem(i, fieldNames.ToArray()));
		}

		/// <inheritdoc />
		public SwItemData Update(SwItemData sItem)
		{
			var spNewItem = this.SpList.Value.GetItemById(sItem.Id);
			foreach (var fieldName in sItem.Keys)
			{
				spNewItem[fieldName] = sItem[fieldName];
			}

			spNewItem.Update();
			return CommonUtils.ConvertSpItemToSwItem(spNewItem, sItem.Keys.ToArray());
		}
	}
}
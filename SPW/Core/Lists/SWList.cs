namespace SPW
{
	using Microsoft.SharePoint;
	using SPW.Utils;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class SwList<T> : SwListBase, ISwList<T>
		where T : SwListItem
	{
		internal SwList(SwWeb swWeb, string listName, SwListTemplate template = SwListTemplate.List)
			: base(swWeb, listName, template)
		{
		}

		/// <inheritdoc />
		public T Create(T sItem)
		{
			var sList = this.SpList.Value;
			var spItem = sList.AddItem();
			var fields = ReflectionUtils.GetProperties<T>();
			foreach (var field in fields)
			{
				spItem[field.Name] = field.GetValue(sItem);
			}

			spItem.Update();
			return CreateItemFromSpItem(spItem);
		}

		/// <inheritdoc />
		public T EnsureItem(IQueryable<T> query, T sItem)
		{
			return sItem; // TODO make done
		}

		public T Find(int sItemId)
		{
			var sList = this.SpList.Value;
			var spItem = sList.GetItemById(sItemId);
			return CreateItemFromSpItem(spItem);
		}

		/// <inheritdoc />
		public IEnumerable<T> GetItems(string query)
		{
			var sList = this.SpList.Value;
			var spQuery = new SPQuery();
			spQuery.Query = query;
			var spItems = sList.GetItems(spQuery);
			return spItems.Cast<SPListItem>().Select(i => CreateItemFromSpItem(i));
		}

		/// <inheritdoc />
		public T Update(T sItem)
		{
			var sList = this.SpList.Value;
			var spItem = sList.GetItemById(sItem.ID);
			var fields = ReflectionUtils.GetProperties<T>();
			foreach (var field in fields)
			{
				spItem[field.Name] = field.GetValue(sItem);
			}

			spItem.Update();
			return CreateItemFromSpItem(spItem);
		}

		private static T CreateItemFromSpItem(SPListItem spItem)
		{
			var sItem = Activator.CreateInstance<T>();
			var fields = ReflectionUtils.GetProperties<T>();
			foreach (var field in fields)
			{
				var value = FieldTypeMapper.Instance.ConvertValue(spItem[field.Name], field.PropertyType);
				field.SetValue(sItem, value);
			}

			sItem.ID = spItem.ID;
			return sItem;
		}

		public T InsertOnSubmit(T sItem)
		{
			throw new NotImplementedException();
		}
	}
}
using SPW.Utils;
using System;
using System.Linq;

namespace SPW.Mocks
{
	internal class SwMockList<T> : ISwList<T> where T : SwListItem
	{
		private readonly SwMockList list;

		public SwMockList(SwMockList list)
		{
			this.list = list;
		}

		public T Add(T sItem)
		{
			var swItemData = new SwItemData();
			var fields = ReflectionUtils.GetProperties<T>();
			foreach (var field in fields)
			{
				swItemData[field.Name] = field.GetValue(sItem);
			}
			list.Add(swItemData);
			swItemData.ID = this.list.Items.Values.Select(i => i.ID).Max() + 1;
			return SwMockList<T>.Convert(swItemData);
		}

		public static T Convert(SwItemData swItemData)
		{
			var sItem = Activator.CreateInstance<T>();
			var fields = ReflectionUtils.GetProperties<T>();
			foreach (var field in fields)
			{
				var value = FieldTypeMapper.Instance.ConvertValue(swItemData[field.Name], field.PropertyType);
				field.SetValue(sItem, value);
			}

			return sItem;
		}

		public T Find(int sItemId)
		{
			return Convert(this.list.Find(sItemId));
		}

		public void Recycle(int sItemId)
		{
			this.list.Recycle(sItemId);
		}

		public T Update(T sItem)
		{
			var itemData = this.list.Find(sItem.ID);
			var fields = ReflectionUtils.GetProperties<T>();
			foreach (var field in fields)
			{
				itemData[field.Name] = field.GetValue(sItem);
			}

			return SwMockList<T>.Convert(itemData);
		}
	}
}
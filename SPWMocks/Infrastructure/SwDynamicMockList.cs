using SPW.Mocks.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SPW.Mocks
{
	public class SwDynamicMockList : SwMockListBase, SwDynamicList
	{
		public SwDynamicMockList(string listName, DataTable listData,
				SwListTemplate template = SwListTemplate.List)
				: base(listName, listData, template)
		{
		}


		private SwItemEntry ConvertDataRowToSwItem(DataRow dataItem, params string[] fieldNames)
		{
			var swItem = new SwItemEntry();

			foreach (var fieldName in fieldNames)
			{
				swItem[fieldName] = dataItem[fieldName];
			}

			var field = typeof(SwItemEntry).GetProperties().First(p => p.Name == "ID");
			field.SetValue(swItem, dataItem.Field<int>("ID"));
			return swItem;
		}

		/// <inheritdoc />
		public SwItemEntry Find(int sItemId, params string[] fieldNames)
		{
			var spItem = ListData.GetById(sItemId);
			return ConvertDataRowToSwItem(spItem, fieldNames);
		}

		/// <inheritdoc />
		public IEnumerable<SwItemEntry> GetItems(string query, string viewFields)
		{
			throw new NotImplementedException();
		}


		/// <inheritdoc />
		public SwItemEntry Create(SwItemEntry sItem)
		{
			var newRow = ListData.NewRow();
			foreach (var fieldName in sItem.Keys)
			{
				FillDataRow(sItem, fieldName, newRow);
			}

			newRow["ID"] = ++CurrentIndex;
			ListData.Rows.Add(newRow);
			return ConvertDataRowToSwItem(newRow, sItem.Keys.ToArray());
		}

		private static void FillDataRow(SwItemEntry sItem, string fieldName, DataRow newRow)
		{
			if (sItem[fieldName] == null)
			{
				newRow[fieldName] = DBNull.Value;
			}
			else
			{
				newRow[fieldName] = sItem[fieldName];
			}
		}

		/// <inheritdoc />
		public SwItemEntry Update(SwItemEntry sItem)
		{
			var dataRow = ListData.GetById(sItem.Id);
			foreach (var fieldName in sItem.Keys)
			{
				FillDataRow(sItem, fieldName, dataRow);
			}

			return ConvertDataRowToSwItem(dataRow, sItem.Keys.ToArray());
		}
	}
}
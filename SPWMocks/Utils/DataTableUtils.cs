using System;
using System.Data;
using System.Linq;

namespace SPW.Mocks.Utils
{
	public static class DataTableUtils
	{
		public static DataTable CreateTable<T>(string name, SwListTemplate template = SwListTemplate.List)
		{
			var table = new DataTable(name + "#" + template);
			var fields = typeof(T).GetProperties();
			foreach (var field in fields)
			{
				if (Nullable.GetUnderlyingType(field.PropertyType) == null)
				{
					table.Columns.Add(new DataColumn(field.Name, field.PropertyType));
				}
				else
				{
					table.Columns.Add(new DataColumn(field.Name, Nullable.GetUnderlyingType(field.PropertyType)));
				}
			}

			return table;
		}

		public static void AddData<T>(this DataTable table, T item, int id = 0)
		{
			var row = table.NewRow();
			var fields = typeof(T).GetProperties();
			foreach (var field in fields)
			{
				row[field.Name] = field.GetValue(item);
			}

			row["ID"] = id;
			table.Rows.Add(row);
		}

		public static DataRow GetById(this DataTable listData, int sItemId)
		{
			return listData.AsEnumerable().First(r => r.Field<int>("ID") == sItemId);
		}
	}
}
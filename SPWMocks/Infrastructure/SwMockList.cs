using System;
using System.Collections.Generic;
using System.Data;
using SPW.Mocks.Utils;
using SPW.Utils;

namespace SPW.Mocks
{
    public class SwMockList<T> : SwMockListBase, ISwList<T> where T : SwListItem
    {
        internal SwMockList(ISwWeb swWeb, DataTable listData, string listName,
            SwListTemplate template = SwListTemplate.List)
            : base(listName, listData, template)
        {
        }

        public T Find(int sItemId)
        {
            var spItem = ListData.GetById(sItemId);

            return CreateItemFromDataRow(spItem);
        }

        /// <inheritdoc />
        public IEnumerable<T> GetItems(string query)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public T Create(T sItem)
        {
            var newRow = ListData.NewRow();
            var fields = PropertyUtils.GetProperties<T>();
            foreach (var field in fields)
            {
                newRow[field.Name] = field.GetValue(sItem);
            }

            newRow["ID"] = ++CurrentIndex;
            ListData.Rows.Add(newRow);

            return CreateItemFromDataRow(newRow);
        }

        /// <inheritdoc />
        public T Update(T sItem)
        {
            var row = ListData.GetById(sItem.ID);

            var fields = PropertyUtils.GetProperties<T>();
            foreach (var field in fields)
            {
                row[field.Name] = field.GetValue(sItem);
            }

            return CreateItemFromDataRow(row);
        }

        private static T CreateItemFromDataRow(DataRow spItem)
        {
            var sItem = Activator.CreateInstance<T>();
            var fields = PropertyUtils.GetProperties<T>();
            foreach (var field in fields)
            {
                var value = FieldTypeMapper.Instance.ConvertValue(spItem[field.Name], field.PropertyType);
                field.SetValue(sItem, value);
            }

            return sItem;
        }
    }
}
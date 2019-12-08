using System.Data;
using System.Linq;
using SPW;
using SPW.Mocks.Utils;

namespace SPW.Mocks
{
    public abstract class SwMockListBase
    {
        protected DataTable ListData;
        protected int CurrentIndex = 0;

        protected SwMockListBase(string listName, DataTable listData,
            SwListTemplate template = SwListTemplate.List)
        {
            ListName = listName;
            Template = template;
            ListData = listData;
            if (ListData.Columns["ID"] == null)
            {
                ListData.Columns.Add(new DataColumn("ID", typeof(int)));
            }

            if (ListData.Rows.Count > 0)
            {
                CurrentIndex = ListData.AsEnumerable().Max(r => r.Field<int>("ID"));
            }
        }

        public string ListName { get; }
        public SwListTemplate Template { get; }

        public void Delete(int sItemId)
        {
            var dataRow = ListData.GetById(sItemId);
            ListData.Rows.Remove(dataRow);
        }

        public void Recycle(int sItemId)
        {
            var dataRow = ListData.GetById(sItemId);
            ListData.Rows.Remove(dataRow);
        }
    }
}
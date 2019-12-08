using System.Collections.Generic;

namespace SPW
{
    public interface ISwDynamicList
    {
        SwItemData Find(int sItemId, params string[] fieldNames);
        IEnumerable<SwItemData> GetItems(string query, string viewFields);
        SwItemData Create(SwItemData sItem);
        SwItemData Update(SwItemData sItem);
        void Delete(int sItemId);
        void Recycle(int sItemId);
    }
}
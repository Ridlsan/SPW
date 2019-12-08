using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SPW
{
    public interface ISwList<T>
    {
        T Find(int sItemId);
        IEnumerable<T> GetItems(string query);
        T Create(T sItem);

        T EnsureItem(IQueryable<T> query, T sItem);

        T Update(T sItem);
        void Delete(int sItemId);
        void Recycle(int sItemId);
    }
}
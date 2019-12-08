using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.SharePoint;
using SPW.Utils;

namespace SPW.LINQ
{
    internal class SwObjectCreator<T> : IEnumerable<T>, IEnumerable where T : SwListItem, new()
    {
        private Enumerator _enumerator;

        internal SwObjectCreator(IEnumerable<SPListItem> spItems)
        {
            _enumerator = new Enumerator(spItems);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Enumerator e = _enumerator;
            if (e == null)
            {
                throw new InvalidOperationException("Cannot enumerate more than once");
            }

            _enumerator = null;
            return e;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly PropertyInfo[] _fields;
            private readonly IEnumerator<SPListItem> _spEnumerator;


            internal Enumerator(IEnumerable<SPListItem> spItems)
            {
                _spEnumerator = spItems.GetEnumerator();
                _fields = ReflectionUtils.GetProperties<T>();
            }

            public T Current { get; private set; }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                Current = null;
                if (_spEnumerator.MoveNext())
                {
                    var spItem = _spEnumerator.Current;
                    if (spItem != null)
                    {
                        Current = new T();
                        foreach (var field in _fields)
                        {
                            var value = FieldTypeMapper.Instance.ConvertValue(spItem[field.Name], field.PropertyType);
                            field.SetValue(Current, value);
                        }

                        Current.ID = spItem.ID;
                    }

                    return true;
                }

                return false;
            }


            public void Reset()
            {
            }

            /// <inheritdoc />
            public void Dispose()
            {
                _spEnumerator?.Dispose();
            }
        }
    }
}
using System;
using System.Collections.Generic;

namespace SPW
{
    public class SwItemData : Dictionary<string, object>, ISwItemData
    {
        public int Id { get; internal set; }
    }

    public interface ISwItemData : IDictionary<string, object>
    {
        int Id { get; }
    }
}
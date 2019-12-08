using System;

namespace SPW
{
    public interface ISwWeb : IDisposable
    {
        string ServerRelativeUrl { get; }

        ISwDynamicList GetList(string listName, SwListTemplate template = SwListTemplate.List);
        ISwList<T> GetList<T>(string listName, SwListTemplate template = SwListTemplate.List) where T : SwListItem;
    }
}
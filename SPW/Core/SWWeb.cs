using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using SPW.LINQ;
using SPW.Utils;

namespace SPW
{
    public class SwWeb : ISwWeb
    {
        private readonly Dictionary<Tuple<string, SwListTemplate>, SPList> _lists =
            new Dictionary<Tuple<string, SwListTemplate>, SPList>();

        internal readonly SPWeb SpWeb;

        internal SwWeb(SPWeb sWeb)
        {
            SpWeb = sWeb;
            ServerRelativeUrl = sWeb.ServerRelativeUrl;
        }

        public string ServerRelativeUrl { get; }

        /// <inheritdoc />
        public ISwDynamicList GetList(string listName, SwListTemplate template)
        {
            return new SwDynamicList(this, listName, template);
        }

        /// <inheritdoc />
        public ISwList<T> GetList<T>(string listName, SwListTemplate template = SwListTemplate.List)
            where T : SwListItem
        {
            return new SwList<T>(this, listName, template);
        }

        internal SPList _getList(string listName, SwListTemplate listType)
        {
            var key = new Tuple<string, SwListTemplate>(listName, listType);
            if (!_lists.ContainsKey(key))
            {
                _lists.Add(key, SpWeb.SwGetList(listName, listType));
            }

            return _lists[key];
        }


        #region Disposing

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        #endregion
    }
}
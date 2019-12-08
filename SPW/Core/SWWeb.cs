namespace SPW
{
	using System;
	using System.Collections.Generic;

	using Microsoft.SharePoint;

	using SPW.Extensions;

	/// <summary>
	///   Initiates SwWeb
	/// </summary>
	/// <seealso cref="SPW.ISwWeb" />
	internal class SwWeb : ISwWeb
	{
		/// <summary>
		///   SharePoint Web Instance
		/// </summary>
		internal readonly SPWeb SpWeb;

		/// <summary>
		///   Cached lists on web
		/// </summary>
		private readonly Dictionary<Tuple<string, SwListTemplate>, SPList> _lists =
			new Dictionary<Tuple<string, SwListTemplate>, SPList>();

		/// <summary>
		///   Initializes a new instance of the <see cref="SwWeb" /> class.
		/// </summary>
		/// <param name="sWeb">SharePoint Web</param>
		internal SwWeb(SPWeb sWeb)
		{
			this.SpWeb = sWeb;
			this.ServerRelativeUrl = sWeb.ServerRelativeUrl;
		}

		/// <summary>
		///   Server relative url
		/// </summary>
		public string ServerRelativeUrl { get; }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

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

		/// <summary>
		/// Gets SharePoint List using cache
		/// </summary>
		/// <param name="listName">Name of the list.</param>
		/// <param name="listType">Type of the list.</param>
		/// <returns></returns>
		internal SPList _getList(string listName, SwListTemplate listType)
		{
			var key = new Tuple<string, SwListTemplate>(listName, listType);
			if (!this._lists.ContainsKey(key))
			{
				this._lists.Add(key, this.SpWeb.SwGetList(listName, listType));
			}

			return this._lists[key];
		}

		/// <summary>
		///   Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing">
		///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
		///   unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
		}
	}
}
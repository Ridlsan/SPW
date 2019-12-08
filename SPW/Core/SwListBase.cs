namespace SPW
{
	using System;

	using Microsoft.SharePoint;

	/// <summary>
	///   List with base implementations
	/// </summary>
	public abstract class SwListBase
	{
		/// <summary>
		///   Initializes a new instance of the <see cref="SwListBase" /> class.
		/// </summary>
		/// <param name="swWeb">IswWeb</param>
		/// <param name="listName">Name of the list (url part after "/lists/" or webUrl for libs)</param>
		/// <param name="template">Template of the list</param>
		protected SwListBase(ISwWeb swWeb, string listName, SwListTemplate template = SwListTemplate.List)
		{
			this.ParentWeb = swWeb;
			this.ListName = listName;
			this.Template = template;
			this.SpList = new Lazy<SPList>(this.GetList);
		}

		/// <summary>
		///   Gets the name of the list (url part after "/lists/" or webUrl for libs)
		/// </summary>
		/// <value>
		///   The name of the list.
		/// </value>
		public string ListName { get; }

		/// <summary>
		/// Gets the parent web.
		/// </summary>
		public ISwWeb ParentWeb { get; }

		/// <summary>
		///   Gets the template.
		/// </summary>
		public SwListTemplate Template { get; }

		/// <summary>
		///   SharePoint list instance.
		/// </summary>
		internal Lazy<SPList> SpList { get; set; }

		/// <inheritdoc />
		public void Delete(int sItemId)
		{
			this.SpList.Value.GetItemById(sItemId).Delete();
		}

		/// <inheritdoc />
		public void Recycle(int sItemId)
		{
			this.SpList.Value.GetItemById(sItemId).Recycle();
		}

		/// <summary>
		/// Returns SharePoint List from web.
		/// </summary>
		/// <returns>Returns SharePoint List from web</returns>
		private SPList GetList()
		{
			return ((SwWeb)this.ParentWeb)._getList(this.ListName, this.Template);
		}
	}
}
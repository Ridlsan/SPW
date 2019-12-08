namespace SPW
{
	using System;

	using Microsoft.SharePoint;

	public abstract class SwListBase
	{
		internal Lazy<SPList> SpList;

		protected ISwWeb _swWeb;

		protected SwListBase(ISwWeb swWeb, string listName, SwListTemplate template = SwListTemplate.List)
		{
			this._swWeb = swWeb;
			this.ListName = listName;
			this.Template = template;
			this.SpList = new Lazy<SPList>(this.CreateList);
		}

		public string ListName { get; }

		public ISwWeb ParentWeb => this._swWeb;

		public SwListTemplate Template { get; }

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

		private SPList CreateList()
		{
			return ((SwWeb)this._swWeb)._getList(this.ListName, this.Template);
		}
	}
}
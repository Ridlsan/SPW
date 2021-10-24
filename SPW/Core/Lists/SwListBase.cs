namespace SPW
{
	using System;
	using Microsoft.SharePoint;

	/// <summary>
	///   List with base implementations.
	/// </summary>
	internal abstract class SwListBase
	{
		/// <summary>
		///   Initializes a new instance of the <see cref="SwListBase" /> class.
		/// </summary>
		/// <param name="swWeb">IswWeb.</param>
		/// <param name="listName">Name of the list (url part after "/lists/" or webUrl for libs).</param>
		/// <param name="template">Template of the list.</param>
		protected SwListBase(SwWeb swWeb, string listName, SwListTemplate template = SwListTemplate.List)
		{
			this.Web = swWeb;
			this.ListName = listName;
			this.Template = template;
			this.SpList = new Lazy<SPList>(this.GetList);
		}

		/// <summary>
		///   Gets the name of the list (url part after "/lists/" or webUrl for libs).
		/// </summary>
		/// <value>
		///   The name of the list.
		/// </value>
		public string ListName { get; }

		/// <summary>
		/// Gets the parent web.
		/// </summary>
		public SwWeb Web { get; }

		/// <summary>
		///   Gets the template.
		/// </summary>
		public SwListTemplate Template { get; }

		/// <summary>
		///  Gets SharePoint list instance.
		/// </summary>
		internal Lazy<SPList> SpList { get; }

		private SPList GetList()
		{
			return this.Web.GetSPList(this.ListName, this.Template);
		}
	}
}
using Microsoft.SharePoint;
using NecroNet.SharePoint.CodeCaml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPW.Examples
{
	public class DynamicService
	{
		private readonly ISwContext _swContext;

		public DynamicService(ISwContext swContext)
		{
			_swContext = swContext;
		}

		public void CreateAndUpdateItem()
		{
			var sItem = new SwItemData
			{
				["Produced"] = DateTime.Today
			};

			var sCarsList = _swContext.GetWeb("myweb").GetList("cars");
			sCarsList.InsertOnSubmit(sItem);
			_swContext.SubmitChanges();
			var newItem = sCarsList.Find(sItem.Id, "Produced", "Title");
			newItem["Produced"] = DateTime.Parse("01.01.2000");
			newItem["Title"] = "Mazda CX-5";
			sCarsList.UpdateOnSubmit(newItem);
			_swContext.SubmitChanges();
		}

		public void GetUserAndRights()
		{
			var sWeb = _swContext.GetWeb("myweb");
			var currentUser = sWeb.CurrentUser;
			var newItem = sWeb.GetList("cars").Items.First();



		}

		public IEnumerable<SwItemData> QueryItems()
		{
			var someList = _swContext.GetWeb("myweb").GetList("cars");
			return someList.GetItems(
					CQ.Where(CQ.Eq.FieldRef("Produced").Value(DateTime.Parse("01.01.2000"))),
					CQ.ViewFields(CQ.FieldRef("Produced"))
			);
		}
	}
}

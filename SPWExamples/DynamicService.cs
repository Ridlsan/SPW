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

			var sCarsList = _swContext.GetWeb("production").GetList("cars");
			sCarsList.Add(sItem);
			var newItem = sCarsList.Find(sItem.ID, new[] { "Produced", "Title" });
			newItem["Produced"] = DateTime.Parse("01.01.2000");
			newItem["Title"] = "Mazda CX-5";
			sCarsList.Update(newItem);
		}

		public void GetUserAndRights()
		{
			var sWeb = _swContext.GetWeb("production");
			var user = sWeb.CurrentUser;
			var sCarsList = sWeb.GetList("cars");
			var newItem = sCarsList.Find(2);
			var canEdit = sCarsList.DoesUserHasPermissions(newItem, ListItemRights.Edit, user);
		}

		public IEnumerable<SwItemData> QueryItems()
		{
			var someList = _swContext.GetWeb("production").GetList("cars");
			return someList.GetItems(
					CQ.Where(CQ.Eq.FieldRef("Produced").Value(DateTime.Parse("01.01.2000"))),
					CQ.ViewFields(CQ.FieldRef("Produced"))
			);
		}
	}
}

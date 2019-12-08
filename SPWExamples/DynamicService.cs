using System;
using System.Collections.Generic;
using NecroNet.SharePoint.CodeCaml;

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
            using (var someWeb = _swContext.Web("myweb"))
            {
                var someList = someWeb.GetList("cars");
                var sItem = someList.Create(
                    new SwItemData
                    {
                        ["Produced"] = DateTime.Today
                    }
                );

                var newItem = someList.Find(sItem.Id, "Produced", "Title");
                newItem["Produced"] = DateTime.Parse("01.01.2000");
                newItem["Title"] = "Mazda CX-5";
                someList.Update(newItem);
            }
        }

        public IEnumerable<SwItemData> QueryItems()
        {
            using (var someWeb = _swContext.Web("myweb"))
            {
                var someList = someWeb.GetList("cars");
                return someList.GetItems(
                    CQ.Where(CQ.Eq.FieldRef("Produced").Value(DateTime.Parse("01.01.2000"))),
                    CQ.ViewFields(CQ.FieldRef("Produced"))
                );
            }
        }
    }
}
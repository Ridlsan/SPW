using System;
using Microsoft.SharePoint;

namespace SPW.Utils
{
    public static class CommonUtils
    {
        public static SwItemData ConvertSpItemToSwItem(SPListItem spItem, params string[] fieldNames)
        {
            var swItem = new SwItemData();
            foreach (string fieldName in fieldNames)
            {
                swItem[fieldName] = spItem[fieldName];
            }

            swItem.Id = spItem.ID;
            return swItem;
        }


    }
}
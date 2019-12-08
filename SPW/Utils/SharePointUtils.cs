using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace SPW.Utils
{
    public static class SharePointUtils
    {
        public static Dictionary<SwListTemplate, SPListTemplateType> ListTemplateMatch =
            new Dictionary<SwListTemplate, SPListTemplateType>
            {
                [SwListTemplate.Library] = SPListTemplateType.DocumentLibrary,
                [SwListTemplate.List] = SPListTemplateType.GenericList,
                [SwListTemplate.TaskList] = SPListTemplateType.Tasks
            };

        public static SPList SwGetList(this SPWeb spWeb, string listName, SwListTemplate listType)
        {
            if (spWeb == null)
            {
                throw new ArgumentNullException(nameof(spWeb));
            }


            var template = ListTemplateMatch[listType];

            if (template == SPListTemplateType.DataConnectionLibrary ||
                template == SPListTemplateType.DocumentLibrary ||
                template == SPListTemplateType.HomePageLibrary ||
                template == SPListTemplateType.PictureLibrary ||
                template == SPListTemplateType.WebPageLibrary)
            {
                var spLibrary =
                    spWeb.GetList(
                        SPUrlUtility.CombineUrl(spWeb.Url, "/") + listName + "/"
                    );

                return spLibrary;
            }
            else
            {
                var spList = spWeb.GetList(
                    SPUrlUtility.CombineUrl(spWeb.Url, "/lists/") + listName + "/"
                );
                return spList;
            }
        }
    }
}
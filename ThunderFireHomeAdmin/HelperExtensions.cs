using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace System.Web.Mvc.Html
{
    public static class HtmlHelperExtensions

    {
        public static string ResolveUrl(this HtmlHelper helper, string relativeUrl)
        {
            if (VirtualPathUtility.IsAppRelative(relativeUrl))
            {
                return VirtualPathUtility.ToAbsolute(relativeUrl);
            }
            else
            {
                var curPath = WebPageContext.Current.Page.TemplateInfo.VirtualPath;
                var curDir = VirtualPathUtility.GetDirectory(curPath);
                return VirtualPathUtility.ToAbsolute(VirtualPathUtility.Combine(curDir, relativeUrl));

            }
        }


        public static string ScriptPath(this HtmlHelper helper, string relativeUrl)
        {
            string result = "";
            if (VirtualPathUtility.IsAppRelative(relativeUrl))
            {
                result = VirtualPathUtility.ToAbsolute(relativeUrl);
   
            }
            else
            {
                var curPath = WebPageContext.Current.Page.TemplateInfo.VirtualPath;
                var curDir = VirtualPathUtility.GetDirectory(curPath);
                result = VirtualPathUtility.ToAbsolute(VirtualPathUtility.Combine(curDir, relativeUrl));

            }
            return "http://localhost:51538" + result;
        }

    }
}




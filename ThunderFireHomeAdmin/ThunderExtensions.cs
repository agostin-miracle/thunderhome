using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThunderFireHomeAdmin
{
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="valueFunc"></param>
        /// <param name="textFunc"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> source, Func<T, string> valueFunc, Func<T, string> textFunc)
        {
            return source.Select(x => new SelectListItem
            {
                Value = valueFunc(x),
                Text = textFunc(x)
            });
        }
    }
}
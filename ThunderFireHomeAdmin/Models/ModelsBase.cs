using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderFireHomeAdmin.Models
{
    public class ModelsBase
    {

        public string ShowDate(DateTime d)
        {
            return d.ToString("dd/MM/yyyy HH:mm");
        }

        public string ShowDateFromJson(string d)
        {
            DateTime dt = JsonConvert.DeserializeObject<DateTime>(d);
            return ShowDate(dt);
        }

        //public DateTime GetDateFromJSON(long jsonDateTime, bool shorter = false)
        //{
        //    return JsonConvert.DeserializeObject<DateTime>($"\"\\/Date({ (shorter ? jsonDateTime * 1000 : jsonDateTime) })\\/\"", new JsonSerializerSettings
        //    {
        //        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        //    }).ToLocalTime();
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderFireHomeAdmin.Models;

namespace ThunderFireHomeAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("Logon", "Home");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        #region -- Logon --

        public ActionResult Logon()
        {
            ThunderFireHomeAdmin.Models.LogonModel model = GetLogonModel();


            return View(model);
        }

        private ThunderFireHomeAdmin.Models.LogonModel GetLogonModel()
        {
            byte _type = 0;
            try
            {
                _type = byte.Parse(HttpContext.Request.Form["plgntyp"].FirstOrDefault().ToString());
            }
            catch { }
            ThunderFireHomeAdmin.Models.LogonModel model = new Models.LogonModel();
            model.AccessType = _type;
            return model;
        }

        [HttpPost]
        public ActionResult Authenticate(LoginEntry entry)
        {
            return Json(SessionControl.Authenticate(entry), JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
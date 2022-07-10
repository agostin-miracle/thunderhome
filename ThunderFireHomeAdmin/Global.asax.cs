using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ThunderFire.Domain.DTO;

namespace ThunderFireHomeAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Session_Start()
        {
            Session["USRLOGGED"] = new AccessControl();
        }

        protected void Session_End()
        {
            try
            {
                var instance = (AccessControl)Session["USRLOGGED"];
                if (instance != null)
                {
                    if (instance.IsLogged)
                    {
                        int _LGNNUM = instance.LGNNUM;
                        ThunderFire.Business.LoginUserDao obj = new ThunderFire.Business.LoginUserDao();
                        obj.Logoff(_LGNNUM);
                        obj = null;
                    }
                    Session["USRLOGGED"] = null;
                }
            }
            catch { }
        }

    }
}

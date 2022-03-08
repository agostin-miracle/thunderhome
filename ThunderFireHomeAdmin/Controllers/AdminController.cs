using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;
using System.Linq.Dynamic.Core;

namespace ThunderFireHomeAdmin.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            return View();
        }


        #region -- Grupos --
        public ActionResult Grupos()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            ThunderFireHomeAdmin.Models.GroupsModel model = GetGroups();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.GroupsModel GetGroups()
        {
            ThunderFireHomeAdmin.Models.GroupsModel model = new Models.GroupsModel();
            ThunderFire.Business.GroupsDao obj = new ThunderFire.Business.GroupsDao();
            model.Lista = obj.List();
            return model;
        }


        [HttpPost]

        public JsonResult ChangeGroups(byte modo, Groups entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            try
            {
                ThunderFire.Business.GroupsDao obj = new ThunderFire.Business.GroupsDao();
                entry.UPDUSU = SessionControl.Current.User.CODUSU;
                if (modo == 1)
                    result = obj.Insert(entry);
                else
                    result = obj.Update(entry);

            }
            catch (Exception Error)
            {
                result.ReturnValue = 0;
                result.MessageToUser = Error.Message;
                result.ErrorObject = Error;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        #endregion -- Grupos --


        #region -- Funcoes do Sistema --

        public ActionResult FuncoesSistema()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            ThunderFireHomeAdmin.Models.SystemFeaturesModel model = GetFeatures();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.SystemFeaturesModel GetFeatures()
        {
            ThunderFireHomeAdmin.Models.SystemFeaturesModel model = new Models.SystemFeaturesModel();
            ThunderFire.Business.SystemFeaturesDao obj = new ThunderFire.Business.SystemFeaturesDao();
            model.KeyTableId = obj.KeyTableId;
            ThunderFire.Business.GeneralTableDao obj1 = new ThunderFire.Business.GeneralTableDao();
            model.ListaTabelas = obj1.List(ThunderFire.Domain.Constants.TABELA_INTERNAS);
            return model;
        }


        public JsonResult SelectSystemFeatures(int pSYSFUN)
        {
            SystemFeatures result = new SystemFeatures();
            ThunderFire.Business.SystemFeaturesDao obj = new ThunderFire.Business.SystemFeaturesDao();
            result = obj.Select(pSYSFUN);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ChangeSystemFeatures(byte modo, SystemFeatures entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            try
            {
                ThunderFire.Business.SystemFeaturesDao obj = new ThunderFire.Business.SystemFeaturesDao();
                entry.UPDUSU = SessionControl.Current.User.CODUSU;
                if (modo == 1)
                    result = obj.Insert(entry);
                else
                    result = obj.Update(entry);

            }
            catch (Exception Error)
            {
                result.ReturnValue = 0;
                result.MessageToUser = Error.Message;
                result.ErrorObject = Error;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuerySystemFeatures(short pSYSTBL)
        {
            string draw = "";
            string start = "";
            string length = "";
            int skip = 0;
            int pageSize = 0;
            try
            {
                draw = HttpContext.Request.Form["draw"].FirstOrDefault().ToString();
                start = Request.Form["start"].ToString();
                length = Request.Form["length"].ToString();
            }
            catch { }

            if (!String.IsNullOrWhiteSpace(start))
                skip = Convert.ToInt32(start);

            if (!String.IsNullOrWhiteSpace(length))
                pageSize = Convert.ToInt32(length);

            ThunderFire.Business.SystemFeaturesDao obj = new ThunderFire.Business.SystemFeaturesDao();
            var result = obj.List(pSYSTBL);
            int count = 0;
            if (result != null)
                count = result.Count;
            var data = result.Skip(skip).Take(pageSize).ToList();
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };
            if (result != null)
                resultout.data = data.ToArray();

            obj = null;
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        #endregion -- Funcoes do Sistema --




        #region -- Funcoes Grupo --

        public ActionResult FuncoesGrupo()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            ThunderFireHomeAdmin.Models.FeaturesGroupModel model = GetFeaturesGroup();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.FeaturesGroupModel GetFeaturesGroup()
        {
            ThunderFireHomeAdmin.Models.FeaturesGroupModel model = new Models.FeaturesGroupModel();
            ThunderFire.Business.FeaturesGroupDao obj = new ThunderFire.Business.FeaturesGroupDao();
            model.KeyTableId = obj.KeyTableId;
            ThunderFire.Business.GroupsDao obj1 = new ThunderFire.Business.GroupsDao();
            model.ListaGrupo = obj1.List();
            ThunderFire.Business.SystemFeaturesDao obj2 = new ThunderFire.Business.SystemFeaturesDao();
            model.ListaFeatures = obj2.List(null);
            return model;
        }


        [HttpPost]
        public JsonResult ChangeFeaturesGroup(byte modo, FeaturesGroup entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            try
            {
                ThunderFire.Business.FeaturesGroupDao obj = new ThunderFire.Business.FeaturesGroupDao();
                entry.UPDUSU = SessionControl.Current.User.CODUSU;
                if (modo == 1)
                    result = obj.Insert(entry);
                else
                    result = obj.Update(entry);

            }
            catch (Exception Error)
            {
                result.ReturnValue = 0;
                result.MessageToUser = Error.Message;
                result.ErrorObject = Error;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QueryFeaturesGroup(int pSYSGRP)
        {
            string draw = "";
            string start = "";
            string length = "";
            int skip = 0;
            int pageSize = 0;
            try
            {
                draw = HttpContext.Request.Form["draw"].FirstOrDefault().ToString();
                start = Request.Form["start"].ToString();
                length = Request.Form["length"].ToString();
            }
            catch { }

            if (!String.IsNullOrWhiteSpace(start))
                skip = Convert.ToInt32(start);

            if (!String.IsNullOrWhiteSpace(length))
                pageSize = Convert.ToInt32(length);

            ThunderFire.Business.FeaturesGroupDao obj = new ThunderFire.Business.FeaturesGroupDao();
            var result = obj.List(pSYSGRP);
            int count = 0;
            if (result != null)
                count = result.Count;
            var data = result.Skip(skip).Take(pageSize).ToList();
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };
            if (result != null)
                resultout.data = data.ToArray();

            obj = null;
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        #endregion -- Funcoes Grupo --

    }
}
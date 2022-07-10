using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Web;
using System.Web.Mvc;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Controllers
{
    public class TarifacaoController : Controller
    {
        // GET: Tarifacao
        public ActionResult Index()
        {
            return View();
        }



        #region  -- Tarifas --

        public ActionResult Tarifas()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            else
            {
                if (!(SessionControl.Current.IsAdministrator || SessionControl.Current.IsManagerProduct))
                    return RedirectToAction("Index", "Gestao");
            }
            byte _level = 0;
            int _id = 0;
            var rsp = AdminSite.GetRequestValue("lvl");
            if (!String.IsNullOrWhiteSpace(rsp))
            {
                _level = byte.Parse(rsp);
            }
            rsp = AdminSite.GetRequestValue("usrcode");
            if (!String.IsNullOrWhiteSpace(rsp))
            {
                _id = int.Parse(rsp);
            }

            ThunderFireHomeAdmin.Models.TariffsModel model = GetTariffs(_level, _id);
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.TariffsModel GetTariffs(byte level, int id)
        {
            ThunderFireHomeAdmin.Models.TariffsModel model = new Models.TariffsModel();
            if (level == 0)
                level = 2;
            if (level == 1)
            {
                model.ListaUsuarios = ThunderFire.Business.Lists.ListUsers(1);
            }
            if (level == 2)
            {
                model.ListaUsuarios = ThunderFire.Business.Lists.UserTarifacion();
            }

            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            model.ListaNivelConfiguracao = obj.List(ThunderFire.Domain.Constants.TABELA_NIVEL_CONFIGURACAO_USUARIO);
            model.ListaResponsavel = obj.List(ThunderFire.Domain.Constants.TABELA_RESPONSAVEL_TARIFA);
            model.ListaBandeiras = obj.List(ThunderFire.Domain.Constants.TABELA_BANDEIRAS);
            model.ListaLinhas= obj.List(ThunderFire.Domain.Constants.TABELA_OPERACOES_POS);
            model.ListaFormaCobranca = obj.List(ThunderFire.Domain.Constants.TABELA_FORMA_COBRANCA);
            ThunderFire.Business.TariffModalityDao obj1 = new ThunderFire.Business.TariffModalityDao();
            model.ListaModalidade = obj1.List();
            ThunderFire.Business.TariffTypeDao obj3 = new ThunderFire.Business.TariffTypeDao();
            model.ListaTarifa = obj3.List();
            ThunderFire.Business.ExpandedTypesDao obj4 = new ThunderFire.Business.ExpandedTypesDao();
            model.ListaTiposExpansao = obj4.List();
            model.FCODEXP = 0;
            model.FNIVCFG = level;
            model.FUSUCFG = id;
            model.CUSUCFG = id;
            model.CNIVCFG = level;
            return model;
        }
        public JsonResult PesquisarTarifa(int pUSUCFG, byte pNIVCFG)
        {
            string draw = "1";
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

            ThunderFire.Business.TariffDao obj = new ThunderFire.Business.TariffDao();
            var result = obj.List(pUSUCFG,pNIVCFG);
            int count = 0;
            List<QueryTariff> data = null;
            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };

            if (result != null)
            {
                data = result.Skip(skip).Take(pageSize).ToList();

                if (result != null)
                {
                    resultout.data = data.ToArray();
                }

            }

            obj = null;
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelecionarTarifa(int pNIDTAR)
        {
            ThunderFire.Business.TariffDao obj = new ThunderFire.Business.TariffDao();
            var result = obj.Select(pNIDTAR);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AtualizarTarifa(byte modo, Tariff entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.TariffDao obj = new ThunderFire.Business.TariffDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion 


        #region  -- Expansão de Tarifas --

        public ActionResult ExpansaoTarifa()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            else
            {
                if (!(SessionControl.Current.IsAdministrator || SessionControl.Current.IsManagerProduct))
                    return RedirectToAction("Index", "Gestao");
            }

            ThunderFireHomeAdmin.Models.ExpandedTariffModel model = GetExpandedTariff();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.ExpandedTariffModel GetExpandedTariff()
        {
            ThunderFireHomeAdmin.Models.ExpandedTariffModel model = new Models.ExpandedTariffModel();
            ThunderFire.Business.ExpandedTariffDao obj = new ThunderFire.Business.ExpandedTariffDao();
            model.ListaTiposExpansao = obj.ListTypes();
            model.FTIPEXP= 0;
            return model;
        }
        public JsonResult PesquisarExpansaoTarifa(short? pTIPEXP)
        {
            string draw = "1";
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

            ThunderFire.Business.ExpandedTariffDao obj = new ThunderFire.Business.ExpandedTariffDao();
            var result = obj.List(pTIPEXP);
            int count = 0;
            List<ExpandedTariff> data = null;
            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };

            if (result != null)
            {
                data = result.Skip(skip).Take(pageSize).ToList();

                if (result != null)
                {
                    resultout.data = data.ToArray();
                }
            }

            obj = null;
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelecionarExpansao(int pNIDEXP)
        {
            ThunderFire.Business.ExpandedTariffDao obj = new ThunderFire.Business.ExpandedTariffDao();
            var result = obj.Select(pNIDEXP);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AtualizarExpansao(byte modo, ExpandedTariff entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.ExpandedTariffDao obj = new ThunderFire.Business.ExpandedTariffDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion -- Expansão de Tarifas --


        #region -- Tipos de Expansão --


        public ActionResult TiposExpansao()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            else
            {
                if (!(SessionControl.Current.IsAdministrator || SessionControl.Current.IsManagerProduct))
                    return RedirectToAction("Index", "Gestao");
            }

            ThunderFireHomeAdmin.Models.ExpandedTypesModel model = GetExpandedTypes();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.ExpandedTypesModel GetExpandedTypes()
        {
            ThunderFireHomeAdmin.Models.ExpandedTypesModel model = new Models.ExpandedTypesModel();
            ThunderFire.Business.ExpandedTypesDao obj = new ThunderFire.Business.ExpandedTypesDao();
            model.ListaRegrasExpansao = obj.ListRules();
            return model;
        }
        public JsonResult PesquisarTipoExpansao()
        {
            string draw = "1";
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

            ThunderFire.Business.ExpandedTypesDao obj = new ThunderFire.Business.ExpandedTypesDao();
            var result = obj.List();
            int count = 0;
            List<ExpandedTypes> data = null;
            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };

            if (result != null)
            {
                data = result.Skip(skip).Take(pageSize).ToList();

                if (result != null)
                {
                    resultout.data = data.ToArray();
                }

            }

            obj = null;
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelecionarTipoExpansao(short pTIPEXP)
        {
            ThunderFire.Business.ExpandedTypesDao obj = new ThunderFire.Business.ExpandedTypesDao();
            var result = obj.Select(pTIPEXP);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AtualizarTipoExpansao(byte modo, ExpandedTypes entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.ExpandedTypesDao obj = new ThunderFire.Business.ExpandedTypesDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion -- Expansão de Tarifas --


        #region -- Tarifas x Operacoes --
        public ActionResult TarifasOperacoes()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            else
            {
                if (!(SessionControl.Current.IsAdministrator || SessionControl.Current.IsManagerProduct))
                    return RedirectToAction("Index", "Gestao");
            }

            ThunderFireHomeAdmin.Models.TariffOperationsModel model = GetTariffsOperations();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.TariffOperationsModel GetTariffsOperations()
        {
            ThunderFireHomeAdmin.Models.TariffOperationsModel model = new Models.TariffOperationsModel();
            ThunderFire.Business.TariffOperationsDao obj = new ThunderFire.Business.TariffOperationsDao();
            model.ListaOperacoes = obj.ListOperations();
            model.FCODMOV = 0;
            model.FCODTAR = -1;
            model.ListaTarifas= obj.ListTariffs();

            return model;
        }
        public JsonResult PesquisarTarifaOperacao()
        {
            string draw = "1";
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

            ThunderFire.Business.TariffOperationsDao obj = new ThunderFire.Business.TariffOperationsDao();
            var result = obj.List();
            int count = 0;
            List<TariffOperations> data = null;
            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };

            if (result != null)
            {
                data = result.Skip(skip).Take(pageSize).ToList();

                if (result != null)
                {
                    resultout.data = data.ToArray();
                }

            }

            obj = null;
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelecionarTarifaOperacao(int pNIDTXM)
        {
            ThunderFire.Business.TariffOperationsDao obj = new ThunderFire.Business.TariffOperationsDao();
            var result = obj.Select(pNIDTXM);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AtualizarTarifaOperacao(byte modo, TariffOperations entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.TariffOperationsDao obj = new ThunderFire.Business.TariffOperationsDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion -- Tarifas x Operacoes

        #region -- Modalidade de Tarifa --
        public ActionResult Modalidades()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            else
            {
                if (!(SessionControl.Current.IsAdministrator || SessionControl.Current.IsManagerProduct))
                    return RedirectToAction("Index", "Gestao");
            }

            ThunderFireHomeAdmin.Models.TariffModalityModel model = GetModality();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.TariffModalityModel GetModality()
        {
            ThunderFireHomeAdmin.Models.TariffModalityModel model = new Models.TariffModalityModel();
            return model;
        }
        public JsonResult PesquisarModalidades()
        {
            string draw = "1";
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

            ThunderFire.Business.TariffModalityDao obj = new ThunderFire.Business.TariffModalityDao();
            var result = obj.List();
            int count = 0;
            List<TariffModality> data = null;
            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };

            if (result != null)
            {
                data = result.Skip(skip).Take(pageSize).ToList();

                if (result != null)
                {
                    resultout.data = data.ToArray();
                }

            }

            obj = null;
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelecionarModalidade(int pMODCRT)
        {
            ThunderFire.Business.TariffModalityDao obj = new ThunderFire.Business.TariffModalityDao();
            var result = obj.Select(pMODCRT);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AtualizarModalidade(byte modo, TariffModality entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.TariffModalityDao obj = new ThunderFire.Business.TariffModalityDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion -- Modalidade de Tarifa --

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Controllers
{
    public class GestaoController : Controller
    {
        // GET: Gestao
        public ActionResult Index()
        {
            return View();
        }


        #region  -- Gestor Cartões --

        public ActionResult Cartoes()
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

            ThunderFireHomeAdmin.Models.ManagementModel model = GetManagement();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.ManagementModel GetManagement()
        {
            ThunderFireHomeAdmin.Models.ManagementModel model = new Models.ManagementModel();
            if (SessionControl.Current.IsAdministrator || SessionControl.Current.IsManagerProduct)
            {
                model.CurrentUser = SessionControl.Current.User.CODUSU;
                if (SessionControl.Current.IsManagerProduct)
                    model.ListaGestor = ThunderFire.Business.Lists.ManagersByLine(1);
                else
                    model.ListaGestor = ThunderFire.Business.Lists.ManagersByLine(1);
            }
            model.ListaStatus = ThunderFire.Business.Lists.Transactions((int)ThunderFire.Domain.MODULOS.CARTOES);

            return model;
        }
        public JsonResult QueryManangementCards(System.Int32 pUSUPRO, System.Int16? pSTACRT = null, string pNUMCRT = "", string pNOMPRT = null)
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
            var result = ThunderFire.Business.Lists.ListCards(pUSUPRO, pSTACRT, pNUMCRT, pNOMPRT);
            int count = 0;
            List<QueryActiveCards> data = null;
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

            
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelecionarUsuarioProduto(int pUSUPRO)
        {
            ThunderFire.Business.ProductManagerDao obj = new ThunderFire.Business.ProductManagerDao();
            var result = obj.Select(pUSUPRO);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pCODCRT"></param>
        /// <param name="pACTION"></param>
        /// <param name="pCODACT"></param>
        /// <returns></returns>

        public JsonResult ChangeDataOnCard(int pCODCRT, int pACTION, byte pCODACT = 0)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.ActiveCardsDao obj = new ThunderFire.Business.ActiveCardsDao();
            ActiveCards objeto = obj.Select(pCODCRT);
            int pUPDUSU = SessionControl.Current.User.CODUSU;
            if (obj.Found)
            {
                /*
                 * Permissão de Compra On-Line 
                 */
                if (pACTION == 1)
                {
                    result = obj.ChangeOnlineShop(pCODCRT, pUPDUSU, (byte)(objeto.APLCON == 1 ? 0 : 1));
                }
                /*
                 * Permissão de Saque 
                 */

                if (pACTION == 2)
                {
                    result = obj.ChangeWithdraw(pCODCRT, pUPDUSU, (byte)(objeto.APLSAQ == 1 ? 0 : 1));
                }

                /*
                 *  Cancelamento de Mensalides
                 */
                if (pACTION == 3)
                {
                    /*
                     *  1 - Cancelamento
                     *  3 - Reversão do Cancelamento
                     */
                    /* Cancelamento */
                    if (pCODACT == 1 || pCODACT == 3)
                    {
                        result = obj.CancelMonthlyFees(pCODCRT, pUPDUSU, pCODACT);
                    }

                }

                /*
                 *  Bloqueio/Desbloqueio de Cartão
                 */
                if (pACTION == 4)
                {
                    /*
                        1 - Bloqueia o Cartão
                        2 - Reverte o Bloqueio Anterior
                        3 - Fixa o cartão como aguardando desbloqueio
                     */

                    /* Cancelamento */
                    if (pCODACT == 1 || pCODACT == 2 || pCODACT == 3)
                    {
                        result = obj.LockCard(pCODCRT, pUPDUSU, pCODACT);
                    }

                }

                /* Cancela a associação do cartão */
                if (pACTION == 5)
                {
                    result = obj.CancelAssociation(pCODCRT, pUPDUSU);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion 



        #region -- Mensalidades --

        public ActionResult Mensalidades()
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

            ThunderFireHomeAdmin.Models.MonthlyPaymentModel model = GetMonthlyPayment();
            return View(model);
        }


        private ThunderFireHomeAdmin.Models.MonthlyPaymentModel GetMonthlyPayment()
        {
            ThunderFireHomeAdmin.Models.MonthlyPaymentModel model = new Models.MonthlyPaymentModel();
            if (SessionControl.Current.IsAdministrator || SessionControl.Current.IsManagerProduct)
            {
                model.CurrentUser = SessionControl.Current.User.CODUSU;
                if (SessionControl.Current.IsManagerProduct)
                    model.ListaGestor = ThunderFire.Business.Lists.ManagersByLine(1);
                else
                    model.ListaGestor = ThunderFire.Business.Lists.ManagersByLine(1);
            }

            model.ListaStatus = ThunderFire.Business.Lists.Transactions((short)ThunderFire.Domain.MODULOS.MENSALIDADES);
            model.ListaTipoMensalidade = ThunderFire.Business.Lists.MonthlyPaymentTypes();
            model.Listausuarios = ThunderFire.Business.Lists.UsersInMonthly();
            model.CTIPMEN = 1;
            model.CSTAMEN = 261;
            return model;
        }


        public JsonResult PesquisaMensalidades(System.Int32 pUSUPRO, System.Byte pTIPMEN, short? pSTAMEN = null, int? pCODUSU = null)
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

            var result = ThunderFire.Business.Lists.MonthlyPayments(pUSUPRO, pTIPMEN, pSTAMEN, pCODUSU);
            int count = 0;
            List<QueryMonthlyPayment> data = null;
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


            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ResumoMensalidades(System.Int32 pUSUPRO, System.Byte pTIPMEN, short? pSTAMEN = null, int? pCODUSU = null)
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

            
            var result = ThunderFire.Business.Lists.MonthlyPaymentSummary(pUSUPRO, pTIPMEN, pSTAMEN, pCODUSU);
            int count = 0;

            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };
            List<QueryMonthlyPayment> data = null;
            if (result != null)
            {
                data = result.Skip(skip).Take(pageSize).ToList();

                if (result != null)
                {
                    resultout.data = data.ToArray();
                }

            }
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region -- Configurr Boleto --
        public ActionResult ConfigurarBoleto()
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

            ThunderFireHomeAdmin.Models.BilletConfigurationModel model = GetBilletConfiguration();
            return View(model);
        }


        private ThunderFireHomeAdmin.Models.BilletConfigurationModel GetBilletConfiguration()
        {
            ThunderFireHomeAdmin.Models.BilletConfigurationModel model = new Models.BilletConfigurationModel();
            short _t = 0;
            model.CNIVCFG = 1;
            model.CUSUCFG = 0;
            model.PNIVCFG = 0;

            model.PNIVCFG = Convert.ToInt16(Utilities.GetRequest("qnivcfg", "0"));
            model.PUSUCFG= Convert.ToInt16(Utilities.GetRequest("qusucfg", "0"));
            if (model.PNIVCFG == 1)
            {
                if (SessionControl.Current.IsManagerProduct)
                    model.ListaUsuarios = ThunderFire.Business.Lists.Managers(SessionControl.Current.User.CODUSU);
                else
                {
                    if (SessionControl.Current.IsSupervisor || SessionControl.Current.IsAdministrator)
                        model.ListaUsuarios = ThunderFire.Business.Lists.Managers();
                }
            }
            else if (model.PNIVCFG == 2)
            {
                model.ListaUsuarios = ThunderFire.Business.Lists.Users(null, null);
            }
            else
            {
                model.ListaUsuarios = new List<MyUsers>();
                model.ListaUsuarios.Add(new MyUsers(0, "PADRAO"));
            }

            if (model.ListaUsuarios != null)
            {
                if (model.ListaUsuarios.Count > 1)
                    model.ListaUsuarios.Add(new MyUsers(-1, "-- SELECIONE --"));
            }

            model.ListaTipoBoletos = ThunderFire.Business.Lists.TicketTypes();
            model.ListaTarifas = ThunderFire.Business.Lists.TariffTypes();
            model.ListaTaxaDepositante = ThunderFire.Business.Lists.DepositorFee();
            model.CUSUCFG = model.PUSUCFG;
            return model;
        }


        public JsonResult PesquisarConfiguracaoBoleto(byte pNIVCFG,System.Int32 pUSUCFG)
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

            var result = ThunderFire.Business.Lists.TicketConfigurations(pNIVCFG, pUSUCFG);
            int count = 0;
            List<TicketConfiguration> data = null;
            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count            };

            if (result != null)
            {
                data = result.Skip(skip).Take(pageSize).ToList();

                if (result != null)
                {
                    resultout.data = data.ToArray();
                }
            }
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult SelecionarConfiguracaoBoleto(int pNIDCBL)
        {
            ThunderFire.Business.TicketConfigurationDao obj = new ThunderFire.Business.TicketConfigurationDao();
            var result = obj.Select(pNIDCBL);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AtualizarConfiguracaoBoleto(byte modo, TicketConfiguration entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.TicketConfigurationDao obj = new ThunderFire.Business.TicketConfigurationDao();

            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        #endregion
    }
}

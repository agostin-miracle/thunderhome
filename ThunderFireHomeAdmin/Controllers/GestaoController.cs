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
            ThunderFire.Business.ProductManagementDao obj = new ThunderFire.Business.ProductManagementDao();
            ThunderFire.Business.TransactionStatusDao obj1 = new ThunderFire.Business.TransactionStatusDao();

            List<GeneralRegistry> itens = null;
            if (SessionControl.Current.IsAdministrator || SessionControl.Current.IsManagerProduct)
            {
                model.CurrentUser = SessionControl.Current.User.CODUSU;
                if (SessionControl.Current.IsManagerProduct)
                    model.ListaGestor= obj.List(1);
                else
                    model.ListaGestor = obj.List(1);
            }

            model.ListaStatus = obj1.List(ThunderFire.Domain.Constants.MODULO_CARTOES);
            return model;
        }
        public JsonResult QueryManangementCards(System.Int32? pUSUPRO = null, System.Int16? pSTACRT = null, string pNUMCRT = "", string pNOMPRT = null)
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

            ThunderFire.Business.ActiveCardsDao obj = new ThunderFire.Business.ActiveCardsDao();
            var result = obj.List(pUSUPRO, pSTACRT, pNUMCRT, pNOMPRT);
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

            obj = null;
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelectProductManagement(int pUSUPRO)
        {
            ThunderFire.Business.ProductManagementDao obj = new ThunderFire.Business.ProductManagementDao();
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

        public JsonResult ChangeDataOnCard(int pCODCRT, int pACTION, byte pCODACT=0)
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
                    result = obj.ChangeOnlineShop(pCODCRT, pUPDUSU, (byte)(objeto.APLCON==1?0:1));
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
                    if (pCODACT == 1 || pCODACT==3)
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
                    if (pCODACT == 1 || pCODACT == 2 || pCODACT == 3 )
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

        #endregion -- Gestor Cartões --

    }
}

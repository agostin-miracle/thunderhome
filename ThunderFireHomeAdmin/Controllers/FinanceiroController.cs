using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Controllers
{
    public class FinanceiroController : Controller
    {

        public string DefaultCulture = "en-us";
        // GET: Financeiro
        public ActionResult Index()
        {
            return View();
        }



        #region -- Boletos --

        public ActionResult Boletos()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.TicketModel model = GetTicket();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.TicketModel GetTicket()
        {
            ThunderFireHomeAdmin.Models.TicketModel model = new Models.TicketModel();

            model.ListaGestores = ThunderFire.Business.Lists.UsersInTickets(3);
            model.PUSUPRO = null;
            int _CODUSU = 0;
            if (SessionControl.Current.IsManagerProduct)
            {
                _CODUSU = SessionControl.Current.User.CODUSU;
                ThunderFire.Business.ProductManagerDao obj = new ThunderFire.Business.ProductManagerDao();
                ProductManager objeto = obj.Select(_CODUSU, ThunderFire.Domain.Constants.PRODUTO_BOLETO);
                if (obj.Found)
                {
                    model.PUSUPRO = objeto.USUPRO;
                }
                objeto = null;
                obj = null;
            }
            if (model.PUSUPRO.HasValue)
                model.ListaGestores = model.ListaGestores.Where(p => p.CODUSU == model.PUSUPRO).ToList();


            if (model.ListaGestores.Count > 1)
            {
                model.ListaGestores.Add(AddItem());
            }


            model.ListaCedentes = ThunderFire.Business.Lists.UsersInTickets(1);

            if (model.ListaCedentes == null)
                model.ListaCedentes = new List<ThunderFire.Domain.DTO.MyUsers>();

            if (model.ListaCedentes.Count > 1)
                model.ListaCedentes.Add(AddItem());
            model.ListaCedentes = model.ListaCedentes.OrderBy(p => p.CODUSU).ToList();


            model.ListaSacados = ThunderFire.Business.Lists.UsersInTickets(2);
            if (model.ListaSacados == null)
                model.ListaSacados = new List<ThunderFire.Domain.DTO.MyUsers>();

            if (model.ListaCedentes.Count > 1)
                model.ListaSacados.Add(AddItem());
            model.ListaSacados = model.ListaSacados.OrderBy(p => p.CODUSU).ToList();


            model.ListaStatusBoleto = ThunderFire.Business.Lists.Transactions(1);
            model.ListaStatusBoleto.Add(new ThunderFire.Domain.Models.TransactionStatus { CODSTA = -1, DSCSTA = "-- SELECIONE --" });
            model.ListaStatusBoleto = model.ListaStatusBoleto.OrderBy(p => p.CODSTA).ToList();


            model.ListaTipoBoleto = ThunderFire.Business.Lists.TicketTypes();
            model.ListaTipoBoleto.Add(new ThunderFire.Domain.Models.GeneralTable { KEYCOD = -1, DSCTAB = "-- SELECIONE --" });
            model.ListaTipoBoleto = model.ListaTipoBoleto.OrderBy(p => p.KEYCOD).ToList();

            model.ListaAvalistas = ThunderFire.Business.Lists.Guarantor();
            model.ListaResponsavel = ThunderFire.Business.Lists.TariffResponsible();
            model.CSTABOL = 1;
            return model;
        }
        public ContentResult InformacoesGestor(int pUSUPRO)
        {
            ThunderFire.Business.ProductManagerDao obj = new ThunderFire.Business.ProductManagerDao();

            ProductManager objeto = obj.Select(pUSUPRO);
            return Content(CreateJson<ProductManager>(obj.Select(pUSUPRO)), "application/json");
        }
        public ContentResult ConfiguracaoBoleto(int pUSUPRO, int pUSUCED, byte pTIPBOL)
        {
            ThunderFire.Business.TicketConfigurationDao obj = new ThunderFire.Business.TicketConfigurationDao();
            TicketConfiguration objeto = new TicketConfiguration();
            int _NIDCBL = obj.Locate(pUSUPRO, pUSUCED, pTIPBOL);
            if (_NIDCBL > 0)
            {
                var result = obj.Select(_NIDCBL);
                if (obj.Found)
                    objeto = result;
            }
            obj = null;
            return Content(CreateJson<TicketConfiguration>(objeto), "application/json");
        }

        public ContentResult TarifasSobreBoleto(int pUSUPRO, int pUSUCED, byte pTIPBOL, short pCODTAR, double pVLRREF, byte pAPLTDP)
        {
            double VLRTAR = 0;
            double VLRTDP = 0;
            Tariffed calc = new Tariffed();
            if (pCODTAR > 0)
                calc = ThunderFire.Business.Actions.TariffValue(pUSUPRO, pUSUCED, pCODTAR, (decimal)pVLRREF, SessionControl.Current.User.CODUSU);
            VLRTAR = calc.EXTVLR;
            calc = new Tariffed();
            //if (pAPLTDP == 1)
            calc = ThunderFire.Business.Actions.TariffValue(pUSUPRO, pUSUCED, 22, (decimal)pVLRREF, SessionControl.Current.User.CODUSU);
            VLRTDP = calc.EXTVLR;
            var response = "{" + String.Format("\"VLRTAR\":{0},\"VLRTDP\":{1}", VLRTAR.ToString(new CultureInfo(DefaultCulture)), VLRTDP.ToString(new CultureInfo(DefaultCulture))) + "}";
            return Content(response, "application/json");
        }


        public ContentResult PesquisarBoletos(System.Int32? pUSUPRO, System.Int32? pUSUCED, System.Int32? pUSUSAC, System.Int32? pCODAVA, System.Int16? pSTABOL, System.Int16? pTIPBOL, System.Int32? pNIDBOL, string pDATEMI1, string pDATEMI2, string pDATVCT1, string pDATVCT2, string pDATPGT1, string pDATPGT2)
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

            var result = ThunderFire.Business.Lists.Tickets(pUSUPRO, pUSUCED, pUSUSAC, pCODAVA, pSTABOL, pTIPBOL, pNIDBOL, pDATEMI1, pDATEMI2, pDATVCT1, pDATVCT2, pDATPGT1, pDATPGT2);
            int count = 0;
            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };

            string response = "";
            if (result != null)
            {
                response = CreateJson<TicketQuery>(resultout, result.Skip(skip).Take(pageSize).ToList());
            }
            else
                response = CreateJson<TicketQuery>(resultout, null);
            return Content(response, "application/json");

        }

        [HttpPost]
        public JsonResult AtualizarBoleto(byte modo, Ticket entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.TicketDao obj = new ThunderFire.Business.TicketDao();

            if (String.IsNullOrWhiteSpace(entry.NUMCTR))
                entry.NUMCTR = " ";
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ContentResult SelecionarBoleto(int pNIDBOL)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.TicketDao obj = new ThunderFire.Business.TicketDao();
            return Content(CreateJson<Ticket>(obj.Select(pNIDBOL)), "application/json");
        }




        private ThunderFire.Domain.DTO.MyUsers AddItem()
        {
            return new ThunderFire.Domain.DTO.MyUsers() { CODUSU = -1, NOMUSU = "-- SELECIONE--" };
        }
        public JsonResult CarregarUsuariosGestor(int pUSUPRO)
        {
            return null;
        }
        #endregion

        
        #region -- Operacoes --

        public ActionResult RegistroOperacoes()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.OperationsRegisterModel model = GetRegistroOperacoes();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.OperationsRegisterModel GetRegistroOperacoes()
        {
            ThunderFireHomeAdmin.Models.OperationsRegisterModel model = new Models.OperationsRegisterModel();
            model.pSUBSYS = Convert.ToByte(Utilities.GetRequest("qsubsys", "0"));
            var _t1 = Utilities.GetRequest("qnidref", "0");
            model.pNIDREF = Convert.ToInt32(Utilities.GetRequest("qnidref", "0"));
            model.ListaOperacoes = ThunderFire.Business.Lists.Operations((byte)model.pSUBSYS);
            model.ListaSinal = ThunderFire.Business.Lists.Signals();
            model.Lista = ThunderFire.Business.Lists.OperationsRegisters(model.pSUBSYS, model.pNIDREF);
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarRegistroOperacoes(byte modo, OperationsRegister entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.OperationsRegisterDao obj = new ThunderFire.Business.OperationsRegisterDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ContentResult SelecionarRegistropOperacoes(int pNIDOPE)
        {
            ThunderFire.Business.OperationsRegisterDao obj = new ThunderFire.Business.OperationsRegisterDao();
            var result = obj.Select(pNIDOPE);
            return Content(CreateJson<OperationsRegister>(obj.Select(pNIDOPE)), "application/json");
        }


        #endregion


        #region -- Baixa Boleto Manual --

        public ActionResult BaixaBoletoManual()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            ThunderFireHomeAdmin.Models.TicketReceiptRecordModel model = GetBaixaBoletoManual();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.TicketReceiptRecordModel GetBaixaBoletoManual()
        {
            ThunderFireHomeAdmin.Models.TicketReceiptRecordModel model = new Models.TicketReceiptRecordModel();
            return model;
        }

        [HttpPost]
        public JsonResult RegistrarBaixa(TicketReceiptRecord entry)
        {
            bool go = true;
            ExecutionResponse result = new ExecutionResponse();


            if (entry.TIPBXA == 2)
            {

                if (entry.NIDBOL <= 0)
                {
                    result.MessageToUser = "NÚMERO DE BOLETO INVALIDO";
                    go = false;
                }

                if (go)
                {
                    if (entry.TOTPAG <= 0 || entry.TOTLIQ <= 0)
                    {
                        result.MessageToUser = "VALOR DO BOLETO INVÁLIDO";
                        go = false;
                    }
                }

                if (String.IsNullOrWhiteSpace(entry.NUMREX))
                    entry.NUMREX = "";

                if (go)
                {
                    ThunderFire.Business.TicketDao bol = new ThunderFire.Business.TicketDao();
                    ThunderFire.Business.TicketReceiptRecordDao obj = new ThunderFire.Business.TicketReceiptRecordDao();
                    entry.NIDLIM = 0;
                    entry.UPDUSU = SessionControl.Current.User.CODUSU;
                    result = obj.Insert(entry);
                    int _NIDRBB = Convert.ToInt32(result.ReturnValue);
                    if (_NIDRBB > 0)
                    {
                        ThunderFire.Business.TicketReceiptDetailDao obj2 = new ThunderFire.Business.TicketReceiptDetailDao();
                        TicketReceiptDetail objeto = new TicketReceiptDetail();
                        objeto.TIPBXA = entry.TIPBXA;
                        objeto.DATPGT = entry.DATPGT;
                        objeto.NIDRBB = _NIDRBB;
                        objeto.NIDBOL = entry.NIDBOL;
                        objeto.NUMPCL = 1;
                        objeto.STAREC = 1;
                        objeto.UPDUSU = entry.UPDUSU;
                        objeto.VLRPAG = entry.TOTPAG;
                        objeto.VLRLIQ = entry.TOTPAG;
                        result = obj2.Insert(objeto);
                        obj2 = null;
                    }
                    obj = null;
                    bol = null;
                }
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region -- Métodos Privados --
        private string CreateJson<T>(DataTableResponse resultout, List<T> dados)
        {
            string b = "\"draw\":{0},\"recordsTotal\":{1},\"recordsFiltered\":{2},\"error\":{3},\"data\":{4}";
            string _list = "";
            if (dados != null)
            {
                _list = ThunderFire.JsonUtil.Serialize<List<T>>(dados);
            }
            string json = String.Format(b, resultout.draw, resultout.recordsTotal, resultout.recordsFiltered, "null", _list);
            return "{" + json + "}";
        }
        private string CreateJson<T>(T dados)
        {
            return ThunderFire.JsonUtil.Serialize<T>(dados);
        }

        #endregion


        #region -- Transferencias --

        public ActionResult Transferencias()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            ThunderFireHomeAdmin.Models.TransferRegistrationModel model = GetTransfers();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.TransferRegistrationModel GetTransfers()
        {
            ThunderFireHomeAdmin.Models.TransferRegistrationModel model = new Models.TransferRegistrationModel();
            model.ListaTipoLancamento = ThunderFire.Business.Lists.AccountEntryTypes(1);
            model.ListaIndicadorCredito = ThunderFire.Business.Lists.AccountEntryIndicators();
            model.ListaIndicadorDebito = ThunderFire.Business.Lists.AccountEntryIndicators();
            return model;
        }

        public ContentResult SelecionarTransferencia(int pNIDTRF)
        {
            ThunderFire.Business.TransferRegistrationDao obj = new ThunderFire.Business.TransferRegistrationDao();
            return Content(CreateJson<TransferRegistration>(obj.Select(pNIDTRF)), "application/json");
        }

        [HttpPost]
        public ContentResult PesquisarTransferencias(short pTIPLCT,int?pCODUSU, string pNIDTRA, byte?pSTAREC)
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

            var result = ThunderFire.Business.Lists.TransferRegistrations(pTIPLCT,pCODUSU,pNIDTRA,pSTAREC);
            int count = 0;
            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };

            string response = "";
            if (result != null)
            {
                response = CreateJson<TransferRegistration>(resultout, result.Skip(skip).Take(pageSize).ToList());
            }
            else
                response = CreateJson<TransferRegistration>(resultout, null);
            return Content(response, "application/json");
        }

        [HttpPost]
        public JsonResult AtualizarTransferencia(byte modo, TransferRegistration entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.TransferRegistrationDao obj = new ThunderFire.Business.TransferRegistrationDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region -- Tipo de Lancamento --

        public ActionResult TipoLancamento()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.AccountEntryTypeModel model = GetAccountEntryType();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.AccountEntryTypeModel GetAccountEntryType()
        {
            ThunderFireHomeAdmin.Models.AccountEntryTypeModel model = new Models.AccountEntryTypeModel();
            model.ListaIndicadorLancamento = ThunderFire.Business.Lists.AccountEntryIndicators();
            model.ListaIndicadorCredito = ThunderFire.Business.Lists.AccountEntryIndicators();
            model.ListaIndicadorDebito = ThunderFire.Business.Lists.AccountEntryIndicators();
            model.ListaTipoTarifa = ThunderFire.Business.Lists.TariffTypes();
            model.ListaAssociacaoOrigem = ThunderFire.Business.Lists.OriginAssociation();

            return model;
        }

        public ContentResult SelecionarTipoLancamento(short pTIPLCT)
        {
            ThunderFire.Business.AccountEntryTypeDao obj = new ThunderFire.Business.AccountEntryTypeDao();
            return Content(CreateJson<AccountEntryType>(obj.Select(pTIPLCT)), "application/json");
        }

        public ContentResult PesquisarTipoLancamento()
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

            var result = ThunderFire.Business.Lists.AccountEntryTypes(null);
            int count = 0;
            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };

            string response = "";
            if (result != null)
            {
                response = CreateJson<AccountEntryType>(resultout, result.Skip(skip).Take(pageSize).ToList());
            }
            else
                response = CreateJson<AccountEntryType>(resultout, null);
            return Content(response, "application/json");
        }

        [HttpPost]
        public JsonResult AtualizarTipoLancamento(byte modo, AccountEntryType entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.AccountEntryTypeDao obj = new ThunderFire.Business.AccountEntryTypeDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region -- Cadastro de Lançamento --

        public ActionResult CadastroLancamento()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.AccountEntryPostingModel model = GetAccountEntryPosting();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.AccountEntryPostingModel GetAccountEntryPosting()
        {
            ThunderFireHomeAdmin.Models.AccountEntryPostingModel model = new Models.AccountEntryPostingModel();

            model.CTIPLCT = Convert.ToInt16(Utilities.GetRequest("qtiplct", "0"));
            model.ListaTipoLancamento = ThunderFire.Business.Lists.AccountEntryTypes(null);
            model.ListaTransacaoMovimento = ThunderFire.Business.Lists.TransactionMovement();
            model.ListaIndicador = ThunderFire.Business.Lists.AccountEntryIndicators();
            model.ListaOperacao= ThunderFire.Business.Lists.Operations(1);
            model.ListaStatusTransacao = ThunderFire.Business.Lists.Transactions(2);
            return model;
        }

        public ContentResult SelecionarCadastroLancamento(int pNIDLCT)
        {
            ThunderFire.Business.AccountEntryPostingDao obj = new ThunderFire.Business.AccountEntryPostingDao();
            return Content(CreateJson<AccountEntryPosting>(obj.Select(pNIDLCT)), "application/json");
        }
        [HttpPost]
        public JsonResult CopiarCadastroLancamento(short pTIPLCT1, short pTIPLCT2)
        {
            ThunderFire.Business.AccountEntryPostingDao obj = new ThunderFire.Business.AccountEntryPostingDao();
            ExecutionResponse result = new ExecutionResponse();
            if ((pTIPLCT1 - pTIPLCT2) != 0)
            {
                int retorno = obj.Copy(pTIPLCT1, pTIPLCT2, SessionControl.Current.User.CODUSU);
                if (retorno > 0)
                {
                    result.ReturnValue = retorno;
                    result.MessageToUser = String.Format("Foram copiados {0} registros", retorno);
                }
                else
                {
                    if (obj.MessageToUser != String.Empty)
                        result.MessageToUser = obj.MessageToUser;
                    else
                        result.MessageToUser = "FALHA NA EXECUÇÃO DA CÓPIA DO TIPO DE LANCAMENTO";
                    result.ReturnValue = 0;
                }
            }
            else
            {
                result.MessageToUser = "NAO É POSSIVEL COPIAR O MESMO TIPO DE LANCAMENTO";
                result.ReturnValue = 0;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteCadastroLancamento(short pTIPLCT)
        {
            ThunderFire.Business.AccountEntryPostingDao obj = new ThunderFire.Business.AccountEntryPostingDao();
            ExecutionResponse result = new ExecutionResponse();
            if (pTIPLCT>0)
            {
                int retorno = obj.Delete(pTIPLCT, SessionControl.Current.User.CODUSU);
                if (retorno > 0)
                {
                    result.ReturnValue = retorno;
                    result.MessageToUser = String.Format("Foram removidos {0} registros", retorno);
                }
                else
                {
                    if (obj.MessageToUser != String.Empty)
                        result.MessageToUser = obj.MessageToUser;
                    else
                        result.MessageToUser = "FALHA NA REMOÇÃO DO TIPO DE LANCAMENTO";
                    result.ReturnValue = 0;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ContentResult PesquisarCadastroLancamento(short pTIPLCT)
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

            var result = ThunderFire.Business.Lists.AccountEntryPostings(pTIPLCT);
            int count = 0;
            if (result != null)
                count = result.Count;
            var resultout = new DataTableResponse
            {
                draw = int.Parse(draw),
                recordsTotal = count,
                recordsFiltered = count
            };

            string response = "";
            if (result != null)
            {
                response = CreateJson<AccountEntryPosting>(resultout, result.Skip(skip).Take(pageSize).ToList());
            }
            else
                response = CreateJson<AccountEntryPosting>(resultout, null);
            return Content(response, "application/json");
        }

        [HttpPost]
        public JsonResult AtualizarCadastroLancamento(byte modo, AccountEntryPosting entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.AccountEntryPostingDao obj = new ThunderFire.Business.AccountEntryPostingDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            else if (modo == 2)
                result = obj.Update(entry);
            else
            {
                result.ReturnValue = 0;
                result.MessageToUser = "TIPO DE MANUTENCAO NAO FORNECIDO";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
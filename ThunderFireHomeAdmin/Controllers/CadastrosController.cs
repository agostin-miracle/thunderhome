using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;


namespace ThunderFireHomeAdmin.Controllers
{
    public class CadastrosController : Controller
    {
        // GET: Cadastros
        public ActionResult Index()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            return View();
        }



        #region -- Tipo de Usuario --
        public ActionResult TipoUsuario()
        {
            ThunderFireHomeAdmin.Models.UserTypeModel model = GetUserType();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.UserTypeModel GetUserType()
        {
            ThunderFireHomeAdmin.Models.UserTypeModel model = new Models.UserTypeModel();
            model.Lista = ThunderFire.Business.Lists.UserTypes();
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarTipoUsuario(byte modo, UserType entry)
        {
            ThunderFire.Business.UserTypeDao obj = new ThunderFire.Business.UserTypeDao();
            ExecutionResponse result = new ExecutionResponse();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion



        #region -- Tipo de Atributo --
        public ActionResult TipoAtributo()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            ThunderFireHomeAdmin.Models.AttributeTypeModel model = GetAttributeType();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.AttributeTypeModel GetAttributeType()
        {
            ThunderFireHomeAdmin.Models.AttributeTypeModel model = new Models.AttributeTypeModel();
            model.Lista = ThunderFire.Business.Lists.AttributeTypes();
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarTipoAtributo(byte modo, AttributeType entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.AttributeTypeDao obj = new ThunderFire.Business.AttributeTypeDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            else
                result = obj.Update(entry);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion -- Tipo de Usuário --


        #region -- Tipo de Conta --
        public ActionResult TipoConta()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            ThunderFireHomeAdmin.Models.AccountTypeModel model = GetAccountType();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.AccountTypeModel GetAccountType()
        {
            ThunderFireHomeAdmin.Models.AccountTypeModel model = new Models.AccountTypeModel();
            model.Lista = ThunderFire.Business.Lists.AccountTypes();
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarTipoConta(byte modo, AccountType entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            try
            {
                ThunderFire.Business.AccountTypeDao obj = new ThunderFire.Business.AccountTypeDao();
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

        #endregion


        #region -- Tipo de Endereço --

        public ActionResult TipoEndereco()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }
            ThunderFireHomeAdmin.Models.AddressTypeModel model = GetAddressType();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.AddressTypeModel GetAddressType()
        {
            ThunderFireHomeAdmin.Models.AddressTypeModel model = new Models.AddressTypeModel();
            model.Lista = ThunderFire.Business.Lists.AddressTypes();

            return model;
        }


        [HttpPost]
        public JsonResult AtualizarTipoEndereco(byte modo, AddressType entry)
        {
            ThunderFire.Business.AddressTypeDao obj = new ThunderFire.Business.AddressTypeDao();
            ExecutionResponse result = new ExecutionResponse();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            else
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region -- Tipo de Contato --

        public ActionResult TipoContato()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.ContactTypeModel model = GetContactType();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.ContactTypeModel GetContactType()
        {
            ThunderFireHomeAdmin.Models.ContactTypeModel model = new Models.ContactTypeModel();
            model.Lista = ThunderFire.Business.Lists.ContactTypes();
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarTipoContato(byte modo, ContactType entry)
        {
            ThunderFire.Business.ContactTypeDao obj = new ThunderFire.Business.ContactTypeDao();
            ExecutionResponse result = new ExecutionResponse();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region -- Tabela Geral --

        public ActionResult TabelaGeral()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.GeneralTableModel model = GetGeneralTable();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.GeneralTableModel GetGeneralTable()
        {
            ThunderFireHomeAdmin.Models.GeneralTableModel model = new Models.GeneralTableModel();
            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            model.ListaTabela = obj.List(0);
            model.CNUMTAB = 0;
            model.FNUMTAB = 0;
            return model;
        }


        [HttpGet]
        public JsonResult SelecionarTabelaGeral(int pKEYTAB)
        {
            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            var result = obj.Select(pKEYTAB);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Listar(int pNUMTAB)
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

            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            var result = obj.List(pNUMTAB);
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
        [HttpPost]
        public JsonResult AtualizarTabelaGeral(byte modo, GeneralTable entry)
        {
            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            ExecutionResponse result = new ExecutionResponse();
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region -- Linha de Produto --
        public ActionResult LinhaProduto()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.ProductLineModel model = GetProductLine();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.ProductLineModel GetProductLine()
        {
            ThunderFireHomeAdmin.Models.ProductLineModel model = new Models.ProductLineModel();
            model.Lista = ThunderFire.Business.Lists.ProductLines();
            return model;
        }
        [HttpPost]
        public JsonResult AtualizarLinhaProduto(byte modo, ProductLine entry)
        {
            ThunderFire.Business.ProductLineDao obj = new ThunderFire.Business.ProductLineDao();
            ExecutionResponse result = new ExecutionResponse();
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region -- Usuarios --
        public ActionResult Usuarios()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.UsersModel model = GetGeneralRegistry();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.UsersModel GetGeneralRegistry()
        {
            ThunderFireHomeAdmin.Models.UsersModel model = new Models.UsersModel();
            model.ListaTipoUsuario = ThunderFire.Business.Lists.UserTypes();
            model.ListaNivelConfianca = ThunderFire.Business.Lists.TrustLevel();
            model.ListaGenero = ThunderFire.Business.Lists.Genders();
            model.ListaNacionalidade = ThunderFire.Business.Lists.Nationalities();
            model.ListaStatusRegistro = ThunderFire.Business.Lists.RegistrationStatus();
            model.ListaGestor = ThunderFire.Business.Lists.Managers();
            model.ListaGestor.Add(new MyUsers(0, "GESTOR NAO ASSOCIADO"));
            model.ListaGestor = model.ListaGestor.OrderBy(P => P.CODUSU).ToList();
            model.ListaStatusUsuario = ThunderFire.Business.Lists.Transactions((int)ThunderFire.Domain.MODULOS.CADASTRO_GERAL);
            model.ListaAtributo = ThunderFire.Business.Lists.AttributeTypes();


            model.CListaAtributo = ThunderFire.Domain.Constants.AddSeletorBase(model.ListaAtributo);
            model.CCODATR = -1;
            model.CListaStatusUsuario = ThunderFire.Domain.Constants.AddSeletorBase(model.ListaStatusUsuario);
            model.CSTAUSU = -1;
            model.ListaStatusRegistro = ThunderFire.Domain.Constants.AddSeletorBase(model.ListaStatusRegistro);
            model.CSTAREC = -1;
            model.CListaGestor = model.ListaGestor;
            model.CListaGestor.Add(new MyUsers(-1, "-- SELECIONE --"));
            model.CListaGestor = model.CListaGestor.OrderBy(P => P.CODUSU).ToList();
            model.CSRCUSU = -1;
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarUsuarios(byte modo, Users entry)
        {
            ThunderFire.Business.UsersDao obj = new ThunderFire.Business.UsersDao();
            ExecutionResponse result = new ExecutionResponse();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ContentResult SelecionarUsuario(int id)
        {
            ThunderFire.Business.UsersDao obj = new ThunderFire.Business.UsersDao();
            return Content(CreateJson<Users>(obj.Select(id)), "application/json");
        }

        public ContentResult LoginPadrao(int pCODUSU)
        {
            ThunderFire.Business.UsersDao obj = new ThunderFire.Business.UsersDao();
            Users retorno = obj.Select(pCODUSU);
            if (obj.Found)
            {
                string _NOMUSU = retorno.NOMUSU;
                retorno.NOMUSU = obj.GetDefaultLoginName(_NOMUSU);
            }
            return Content(CreateJson<Users>(retorno), "application/json");
        }

        [HttpPost]
        public ContentResult PesquisarUsuarios(System.Int16? pCODATR, System.Int16? pSTAUSU, System.Int32? pSRCUSU, string pNOMUSU, System.Byte? pSTAREC)
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

            List<QueryUsers> result = ThunderFire.Business.Lists.Users(pCODATR, pSTAUSU, pSRCUSU, pNOMUSU, pSTAREC);
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
                response = CreateJson<QueryUsers>(resultout, result.Skip(skip).Take(pageSize).ToList());
            }
            else
                response = CreateJson<Operations>(resultout, null);
            return Content(response, "application/json");

        }

        #endregion

        #region -- Usuario Gestor --
        public ActionResult UsuarioGestor()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.ProductManagerModel model = GetProductManager();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.ProductManagerModel GetProductManager()
        {
            ThunderFireHomeAdmin.Models.ProductManagerModel model = new Models.ProductManagerModel();
            model.ListaProduto = ThunderFire.Business.Lists.Products(null);
            model.ListaGestor = ThunderFire.Business.Lists.UsersManagers();
            return model;
        }

        public JsonResult AtualizarGestorProduto(byte modo, ProductManager entry)
        {
            ThunderFire.Business.ProductManagerDao obj = new ThunderFire.Business.ProductManagerDao();
            ExecutionResponse result = new ExecutionResponse();
            if (String.IsNullOrEmpty(entry.INSBC1))
                entry.INSBC1 = "";
            if (String.IsNullOrEmpty(entry.INSBC2))
                entry.INSBC3 = "";
            if (String.IsNullOrEmpty(entry.INSBC3))
                entry.INSBC3 = "";
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PesquisarGestorProduto(System.Int32? pCODUSU, System.Int16? pCODPRO)
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


            var result = ThunderFire.Business.Lists.ProductManagers(pCODUSU, pCODPRO);
            int count = 0;
            List<ProductManager> data = null;
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
        public JsonResult SelecionarGestorProduto(int pUSUPRO)
        {
            ThunderFire.Business.ProductManagerDao obj = new ThunderFire.Business.ProductManagerDao();
            var result = obj.Select(pUSUPRO);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region -- Produtos --
        public ActionResult Produtos()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.ProductModel model = GetProducts();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.ProductModel GetProducts()
        {
            ThunderFireHomeAdmin.Models.ProductModel model = new Models.ProductModel();
            model.CListaLinhaProduto = ThunderFire.Business.Lists.ProductLines();
            model.CListaLinhaProduto.Add(new ProductLine { LINPRO = -1, DSCLIN = "-- SELECIONE --" });
            model.CListaLinhaProduto = model.CListaLinhaProduto.OrderBy(P => P.LINPRO).ToList();
            model.ListaLinhaProduto = ThunderFire.Business.Lists.ProductLines();
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarProduto(byte modo, Product entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.ProductDao obj = new ThunderFire.Business.ProductDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult PesquisarProduto(System.Int16 pLINPRO)
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




            var result = ThunderFire.Business.Lists.Products(pLINPRO);
            int count = 0;
            List<Product> data = null;
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
        public JsonResult SelecionarProduto(short pCODPRO)
        {
            ThunderFire.Business.ProductDao obj = new ThunderFire.Business.ProductDao();
            var result = obj.Select(pCODPRO);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion 



        #region -- Login de Usuário --
        public ActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ChangeLoginUser(LoginUser entry)
        {
            ThunderFire.Business.LoginUserDao obj = new ThunderFire.Business.LoginUserDao();
            ExecutionResponse result;
            result = obj.Insert(entry);
            obj = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetLoginUser(int id)
        {
            ThunderFire.Business.LoginUserDao obj = new ThunderFire.Business.LoginUserDao();
            return Json(obj.Get(id), JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region  -- Enderecos --

        public ActionResult Enderecos()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.AddressBookModel model = GetAddressBook();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.AddressBookModel GetAddressBook()
        {
            ThunderFireHomeAdmin.Models.AddressBookModel model = new Models.AddressBookModel();
            int _usersel = 0;
            int _usercode = 0;
            try
            {
                _usercode = int.Parse(HttpContext.Request["uc"].FirstOrDefault().ToString());
            }
            catch { }

            model.CCODUSU = _usercode;
            model.UsuarioSelecionado = _usersel;
            model.ListaUsuario = ThunderFire.Business.Lists.Users(_usercode, null);
            model.ListaUF = ThunderFire.Business.Lists.UF();
            model.ListaTipoLogradouro = ThunderFire.Business.Lists.PublicPlaces();
            model.ListaTipoEndereco = ThunderFire.Business.Lists.AddressTypes();
            model.ListaPais = ThunderFire.Business.Lists.Countries();
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarEnderecos(byte modo, AddressBook entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.AddressBookDao obj = new ThunderFire.Business.AddressBookDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PesquisarEnderecos(System.Int32 pCODUSU, short pTIPEND, short pREGATV, short pSTAREC)
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


            var result = ThunderFire.Business.Lists.Addresses(pCODUSU, pTIPEND, pREGATV, pSTAREC);
            int count = 0;
            List<QueryAddressBook> data = null;
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
        public JsonResult SelecionarEndereco(int pCODEND)
        {
            ThunderFire.Business.AddressBookDao obj = new ThunderFire.Business.AddressBookDao();
            var result = obj.Select(pCODEND);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region  -- Contatos --

        public ActionResult Contatos()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.ContactBookModel model = GetContactBook();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.ContactBookModel GetContactBook()
        {
            ThunderFireHomeAdmin.Models.ContactBookModel model = new Models.ContactBookModel();
            try
            {
                model.PCODUSU = int.Parse(HttpContext.Request["uc"].FirstOrDefault().ToString());
            }
            catch { }

            try
            {
                model.CCODEND = int.Parse(HttpContext.Request["ce"].FirstOrDefault().ToString());
            }
            catch { }

            model.ListaUsuario = ThunderFire.Business.Lists.Users(model.PCODUSU, null);
            model.ListaOperadora = ThunderFire.Business.Lists.TelephoneOperators();
            model.ListaPais = ThunderFire.Business.Lists.Countries();
            model.ListaTipoContato = ThunderFire.Business.Lists.ContactTypes();
            model.CCODUSU = model.PCODUSU;
            return model;
        }


        [HttpPost]
        public JsonResult AtualizarContato(byte modo, ContactBook entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.ContactBookDao obj = new ThunderFire.Business.ContactBookDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PesquisarContatos(System.Int32 pCODUSU)
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



            var result = ThunderFire.Business.Lists.Contacts(pCODUSU);
            int count = 0;
            List<QueryContactBook> data = null;
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
        public JsonResult SelecionarContato(int pCODCTO)
        {
            ThunderFire.Business.ContactBookDao obj = new ThunderFire.Business.ContactBookDao();
            var result = obj.Select(pCODCTO);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion 


        #region -- Status Transacao --
        public ActionResult StatusTransacao()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.TransactionStatusModel model = GetTransactionStatus();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.TransactionStatusModel GetTransactionStatus()
        {
            ThunderFireHomeAdmin.Models.TransactionStatusModel model = new Models.TransactionStatusModel();
            model.ListaModulos = ThunderFire.Business.Lists.Modules();
            model.CCODMOD = 1;
            model.FCODMOD = 1;
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarStatusTransacao(byte modo, TransactionStatus entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.TransactionStatusDao obj = new ThunderFire.Business.TransactionStatusDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);


            if (obj.HasError)
            {
                result = GetNewResponse(result);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ExecutionResponse GetNewResponse(ExecutionResponse result)
        {
            ExecutionResponse r = new ExecutionResponse();
            r.ReturnValue = result.ReturnValue;
            r.StatusCode = result.StatusCode;
            r.ErrorMessage = result.ErrorMessage;
            r.SourceError = result.SourceError;
            r.ErrorCode = result.ErrorCode;
            r.MessageToUser = result.MessageToUser;
            return r;
        }

        public JsonResult PesquisarStatusTransacao(System.Int16 pCODMOD)
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


            var result = ThunderFire.Business.Lists.Transactions(pCODMOD);

            int count = 0;
            List<TransactionStatus> data = null;
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
        public JsonResult SelecionarStatusTransacao(short pCODSTA)
        {
            ThunderFire.Business.TransactionStatusDao obj = new ThunderFire.Business.TransactionStatusDao();
            var result = obj.Select(pCODSTA);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region -- Operacoes --
        public ActionResult Operacoes()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.OperationsModel model = GetOperations();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.OperationsModel GetOperations()
        {
            ThunderFireHomeAdmin.Models.OperationsModel model = new Models.OperationsModel();
            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            model.ListaSinais = ThunderFire.Business.Lists.Signals();
            model.ListaSubSistema = ThunderFire.Business.Lists.Subsystems();
            model.ListaCondicaoBloqueio = ThunderFire.Business.Lists.BlockingCondition();
            model.FSIGOPE = 0;
            model.CSUBSYS = 1;
            model.FCNDBLO = 1;
            model.FSIGOPE = 1;
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarOperacao(byte modo, Operations entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.OperationsDao obj = new ThunderFire.Business.OperationsDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelecionarOperacao(short pCODMOV)
        {
            ThunderFire.Business.OperationsDao obj = new ThunderFire.Business.OperationsDao();
            var result = obj.Select(pCODMOV);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ContentResult ListarOperacoes(byte? pSUBSYS)
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


            var result = ThunderFire.Business.Lists.Operations(pSUBSYS);
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
                response = CreateJson<Operations>(resultout, result.Skip(skip).Take(pageSize).ToList());
            }
            else
                response = CreateJson<Operations>(resultout, null);
            return Content(response, "application/json");
        }
        #endregion

        #region -- Conta Virtual --
        public ActionResult ContaVirtual()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.VirtualAccountModel model = GetVirtualAccount();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.VirtualAccountModel GetVirtualAccount()
        {
            ThunderFireHomeAdmin.Models.VirtualAccountModel model = new Models.VirtualAccountModel();

            int _CODUSU = SessionControl.Current.User.CODUSU;

            try
            {
                model.PCODUSU = int.Parse(HttpContext.Request["uc"].FirstOrDefault().ToString());
            }
            catch { }


            model.ListaBancos = ThunderFire.Business.Lists.Banks();
            model.ListaOrigemConta = ThunderFire.Business.Lists.AccountOrigin();
            model.ListaStatusConta = ThunderFire.Business.Lists.Transactions((int)ThunderFire.Domain.MODULOS.CONTAS);
            model.CListaStatusConta = ThunderFire.Business.Lists.Transactions((int)ThunderFire.Domain.MODULOS.CONTAS);
            model.CListaStatusConta.Add(new TransactionStatus { CODSTA = -1, DSCSTA = "-- SELECIONE --" });
            model.ListaTipoBeneficiario = ThunderFire.Business.Lists.BeneficiaryTypes();
            model.ListaTipoConta = ThunderFire.Business.Lists.AccountTypes();
            model.ListaUsuario = ThunderFire.Business.Lists.UsersAccounts(model.PCODUSU);
            model.CListaUsuario = ThunderFire.Business.Lists.UsersAccounts(model.PCODUSU);
            model.CListaUsuario.Add(new MyUsers { CODUSU = -1, NOMUSU = "-- SELECIONE --" });
            model.APPROVAL_ACCOUNT = ThunderFire.Business.Actions.GetUserAccess(model.PROCESS_ID, 205, _CODUSU) == 1;
            model.CListaStatusConta = model.CListaStatusConta.OrderBy(p => p.CODSTA).ToList();
            model.CListaUsuario = model.CListaUsuario.OrderBy(p => p.CODUSU).ToList();
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarContaVirtual(byte modo, VirtualAccount entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.VirtualAccountDao obj = new ThunderFire.Business.VirtualAccountDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ContentResult SelecionarContaVirtual(int pNIDCTA)
        {
            ThunderFire.Business.VirtualAccountDao obj = new ThunderFire.Business.VirtualAccountDao();
            return Content(CreateJson<VirtualAccount>(obj.Select(pNIDCTA)), "application/json");

        }

        public ContentResult PesquisarContasVirtuais(short? pSTACTA, string pNOMUSU="")
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


            var result = ThunderFire.Business.Lists.Accounts (pSTACTA,pNOMUSU);
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
                response = CreateJson<Accounts>(resultout, result.Skip(skip).Take(pageSize).ToList());
            }
            else
                response = CreateJson<Accounts>(resultout, null);
            return Content(response, "application/json");
        }

        #endregion


        #region -- Contas Vinculadas --
        public ActionResult ContaVinculada()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.LinkedAccountModel model = GetLinkedAccount();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.LinkedAccountModel GetLinkedAccount()
        {
            ThunderFireHomeAdmin.Models.LinkedAccountModel model = new Models.LinkedAccountModel();
            int _CODUSU = SessionControl.Current.User.CODUSU;
            try
            {
                model.PCODUSU = int.Parse(HttpContext.Request["uc"].FirstOrDefault().ToString());
            }
            catch { }

            model.CanRemoveLinkId = 1;
            if (ThunderFire.Business.Support.HasAccess(35, 200, SessionControl.Current.User.CODUSU))
                model.CanRemoveLinkId = 0;

            model.ListaUsuario = ThunderFire.Business.Lists.Users(null,null);
            return model;
        }

        [HttpPost]
        public JsonResult AtualizarContaVinculada(byte modo, LinkedAccount entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.LinkedAccountDao obj = new ThunderFire.Business.LinkedAccountDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoverContaVinculada(int pNIDCVL)
        {
            ExecutionResponse result = new ExecutionResponse();
            if (pNIDCVL > 0)
            {
                ThunderFire.Business.LinkedAccountDao obj = new ThunderFire.Business.LinkedAccountDao();
                LinkedAccount data = obj.Select(pNIDCVL);
                if (obj.Found)
                {
                    int return_value = obj.RemoveLinkedID(data.CODUSU, data.NIDCTA, SessionControl.Current.User.CODUSU);
                    result.ReturnValue = return_value;
                    if (return_value > 0)
                        result.MessageToUser = "CONTA REMOVIDA COM SUCESSO";
                    else
                    {

                        result.MessageToUser =obj.MessageToUser;
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CarregarUsuariosLinkados(int pCODUSU)
        {
            var result = ThunderFire.Business.Lists.UsersLinkeds(pCODUSU);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ContentResult PesquisarContasVinculadas(string pNOMUSU = "")
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


            var result = ThunderFire.Business.Lists.LinkedAccounts(pNOMUSU);
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
                response = CreateJson<LinkedAccount>(resultout, result.Skip(skip).Take(pageSize).ToList());
            }
            else
                response = CreateJson<Accounts>(resultout, null);
            return Content(response, "application/json");
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


    }
}
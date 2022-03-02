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
            ThunderFire.Business.UserTypeDao obj = new ThunderFire.Business.UserTypeDao();
            model.Lista = obj.List();
            return model;
        }

        [HttpPost]
        public JsonResult InsertUserType(UserType entry)
        {
            ThunderFire.Business.UserTypeDao obj = new ThunderFire.Business.UserTypeDao();
            var result = obj.Insert(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateUserType(UserType entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            try
            {
                if (entry.DSCTUS != "")
                {
                    ThunderFire.Business.UserTypeDao obj = new ThunderFire.Business.UserTypeDao();
                    result = obj.Update(entry);
                }
                else
                {
                    result.MessageToUser = "A DESCRICAO DO TIPO DE USUARIO DEVERA SER FORNECIDA";
                    result.ReturnValue = 0;
                }
            }
            catch (Exception Error)
            {
                result.ReturnValue = 0;
                result.MessageToUser = Error.Message;
                result.ErrorObject = Error;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion -- Tipo de Usuário --



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
            ThunderFire.Business.AttributeTypeDao obj = new ThunderFire.Business.AttributeTypeDao();
            model.Lista = obj.List();
            return model;
        }

        [HttpPost]
        public JsonResult InsertAttributeType(AttributeType entry)
        {
            return Json(ChangeAttributeType(1, entry), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateAttributeType(AttributeType entry)
        {
            return Json(ChangeAttributeType(2, entry), JsonRequestBehavior.AllowGet);
        }

        private ExecutionResponse ChangeAttributeType(byte modo, AttributeType entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            try
            {
                if (!String.IsNullOrWhiteSpace(entry.DSCATR))
                {
                    ThunderFire.Business.AttributeTypeDao obj = new ThunderFire.Business.AttributeTypeDao();
                    entry.UPDUSU = SessionControl.Current.User.CODUSU;
                    if (modo == 1)
                        result = obj.Insert(entry);
                    else
                        result = obj.Update(entry);
                }
                else
                {
                    result.MessageToUser = "A DESCRICAO DO TIPO DE ATRIBUTO DEVERÁ SER INFORMADA";
                    result.ReturnValue = 0;
                }
            }
            catch (Exception Error)
            {
                result.ReturnValue = 0;
                result.MessageToUser = Error.Message;
                result.ErrorObject = Error;
            }
            return result;

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
            ThunderFire.Business.AccountTypeDao obj = new ThunderFire.Business.AccountTypeDao();
            model.Lista = obj.List();
            return model;
        }

        [HttpPost]
        public JsonResult ChangeAccountType(byte modo, AccountType entry)
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

        #endregion -- Tipo de Usuário --




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
            ThunderFire.Business.AddressTypeDao obj = new ThunderFire.Business.AddressTypeDao();
            model.Lista = obj.List();
            return model;
        }
        [HttpPost]
        public JsonResult InsertAddressType(AddressType entry)
        {
            ThunderFire.Business.AddressTypeDao obj = new ThunderFire.Business.AddressTypeDao();
            var result = obj.Insert(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateAddressType(AddressType entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            try
            {
                if (entry.DSCTEN != "")
                {
                    ThunderFire.Business.AddressTypeDao obj = new ThunderFire.Business.AddressTypeDao();
                    result = obj.Update(entry);
                }
                else
                {
                    result.MessageToUser = "A DESCRICAO DO TIPO DE ENDEREÇO SER FORNECIDA";
                    result.ReturnValue = 0;
                }
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
            ThunderFire.Business.ContactTypeDao obj = new ThunderFire.Business.ContactTypeDao();
            model.Lista = obj.List();
            return model;
        }
        [HttpPost]
        public JsonResult InsertContactType(AddressType entry)
        {
            ThunderFire.Business.AddressTypeDao obj = new ThunderFire.Business.AddressTypeDao();
            var result = obj.Insert(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateAContactType(ContactType entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            try
            {
                if (entry.DSCCTO != "")
                {
                    ThunderFire.Business.ContactTypeDao obj = new ThunderFire.Business.ContactTypeDao();
                    result = obj.Update(entry);
                }
                else
                {
                    result.MessageToUser = "A DESCRICAO DO TIPO DE ENDEREÇO SER FORNECIDA";
                    result.ReturnValue = 0;
                }
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
            model.Lista = obj.List(0);
            model.ListaTabela = obj.List(0);
            return model;
        }

        [HttpGet]
        public JsonResult GetTable(int pNUMTAB)
        {
            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            var result = obj.List(pNUMTAB);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GeneralTableQuery(int pNUMTAB)
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
        public JsonResult InsertGeneralTable(GeneralTable entry)
        {
            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            var result = obj.Insert(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateGeneralTable(GeneralTable entry)
        {
            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            var result = obj.Update(entry);
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
            ThunderFire.Business.ProductLineDao obj = new ThunderFire.Business.ProductLineDao();
            model.Lista = obj.List();
            return model;
        }
        [HttpPost]
        public JsonResult ChangeProductLine(byte modo,ProductLine entry)
        {
            ThunderFire.Business.ProductLineDao obj = new ThunderFire.Business.ProductLineDao();
            ExecutionResponse result = new ExecutionResponse();
            if (modo == 1)
            {
                result = obj.Insert(entry);
            }
            if (modo == 2)
            {
                result = obj.Update(entry);
            }
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

            ThunderFireHomeAdmin.Models.GeneralRegistryModel model = GetGeneralRegistry();
            return View(model);
        }
        private ThunderFireHomeAdmin.Models.GeneralRegistryModel GetGeneralRegistry()
        {
            ThunderFireHomeAdmin.Models.GeneralRegistryModel model = new Models.GeneralRegistryModel();
            ThunderFire.Business.UserTypeDao obj = new ThunderFire.Business.UserTypeDao();
            model.ListaTipoUsuario = obj.List();
            ThunderFire.Business.GeneralTableDao obj1 = new ThunderFire.Business.GeneralTableDao();
            model.ListaNivelConfianca = obj1.List(ThunderFire.Domain.Constants.TABELA_NIVEL_CONFIANCA);
            model.ListaGenero = obj1.List(ThunderFire.Domain.Constants.TABELA_GENERO);
            model.ListaNacionalidade = obj1.List(ThunderFire.Domain.Constants.TABELA_NACIONALIDADES);
            model.ListaStatusRegistro = obj1.List(ThunderFire.Domain.Constants.TABELA_STATUS_REGISTRO);
            ThunderFire.Business.GeneralRegistryDao obj3 = new ThunderFire.Business.GeneralRegistryDao();
            model.ListaGestor = obj3.List(ThunderFire.Domain.Constants.ATRIBUTO_GESTORES, null, null, null, null);
            ThunderFire.Business.TransactionStatusDao obj4 = new ThunderFire.Business.TransactionStatusDao();
            model.ListaStatusUsuario = obj4.List(ThunderFire.Domain.Constants.MODULO_CADASTRO_GERAL);
            ThunderFire.Business.AttributeTypeDao obj5 = new ThunderFire.Business.AttributeTypeDao();
            model.ListaAtributo = obj5.List();
            return model;
        }
        [HttpPost]
        public JsonResult InsertGeneralRegistry(GeneralRegistry entry)
        {
            ThunderFire.Business.GeneralRegistryDao obj = new ThunderFire.Business.GeneralRegistryDao();
            var result = obj.Insert(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateGeneralRegistry(GeneralRegistry entry)
        {
            ThunderFire.Business.GeneralRegistryDao obj = new ThunderFire.Business.GeneralRegistryDao();
            var result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectGeneralRegistry(int id)
        {
            ThunderFire.Business.GeneralRegistryDao obj = new ThunderFire.Business.GeneralRegistryDao();
            ////ExecutionResponse respond = new ExecutionResponse();
            //if (id > 0)
            //{
            //    var result = obj.Select(id);
            //    if (obj.Found)
            //    {
            //        respond.ReturnValue = result;
            //    }
            //    else
            //    {
            //        respond.ReturnValue = null;
            //        respond.MessageToUser = "NAO ENCONTRADO";
            //    }
            //}
            return Json(obj.Select(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GeneralRegistryQuery(System.Int16? pCODATR, System.Int16? pSTAUSU, System.Int32? pSRCUSU, string pNOMUSU, System.Byte? pSTAREC)
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

            ThunderFire.Business.GeneralRegistryDao obj = new ThunderFire.Business.GeneralRegistryDao();
            var result = obj.List(pCODATR, pSTAUSU, pSRCUSU, pNOMUSU, pSTAREC);
            int count = 0;
            List<QueryGeneralRegistry> data = null;
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

        #endregion

        #region -- Usuario Gestor --
        public ActionResult UsuarioGestor()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.ProductManagementModel model = GetProductManagement();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.ProductManagementModel GetProductManagement()
        {
            ThunderFireHomeAdmin.Models.ProductManagementModel model = new Models.ProductManagementModel();
            ThunderFire.Business.ProductsDao obj = new ThunderFire.Business.ProductsDao();
            ThunderFire.Business.GeneralRegistryDao obj1 = new ThunderFire.Business.GeneralRegistryDao();
            model.ListaGestor = obj1.List(ThunderFire.Domain.Constants.ATRIBUTO_GESTORES, null, null, null, null);
            return model;
        }

        public JsonResult InsertProductManagement(ProductManagement entry)
        {
            ThunderFire.Business.ProductManagementDao obj = new ThunderFire.Business.ProductManagementDao();
            var result = obj.Insert(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateProductManagement(ProductManagement entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            if (entry.USUPRO > 0)
            {
                ThunderFire.Business.ProductManagementDao obj = new ThunderFire.Business.ProductManagementDao();
                result = obj.Update(entry);
            }
            else
            {
                result.MessageToUser = "O IDENTIFICADOR DO GESTOR NAO FOI FORNECIDO";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult QueryProductManangement(System.Int32? pCODUSU, System.Int16? pCODPRO)
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

            ThunderFire.Business.ProductManagementDao obj = new ThunderFire.Business.ProductManagementDao();
            var result = obj.List(pCODUSU, pCODPRO);
            int count = 0;
            List<QueryProductManagement> data = null;
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
        #endregion -- Usuario Gestor --


        #region -- Produtos --
        public ActionResult Produtos()
        {
            if (!SessionControl.IsLogged())
            {
                return RedirectToAction("logon", "home");
            }

            ThunderFireHomeAdmin.Models.ProductsModel model = GetProducts();
            return View(model);
        }

        private ThunderFireHomeAdmin.Models.ProductsModel GetProducts()
        {
            ThunderFireHomeAdmin.Models.ProductsModel model = new Models.ProductsModel();
            ThunderFire.Business.ProductsDao obj = new ThunderFire.Business.ProductsDao();
            ThunderFire.Business.ProductLineDao obj1 = new ThunderFire.Business.ProductLineDao();
            model.ListaLinhaProduto = obj1.List();
            return model;
        }

        [HttpPost]
        public JsonResult ChangeProducts(byte modo, Products entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.ProductsDao obj = new ThunderFire.Business.ProductsDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult QueryProducts(System.Int16 pLINPRO)
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



            ThunderFire.Business.ProductsDao obj = new ThunderFire.Business.ProductsDao();
            var result = obj.List(pLINPRO);
            int count = 0;
            List<QueryProducts> data = null;
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
        public JsonResult SelectProducts(short pCODPRO)
        {
            ThunderFire.Business.ProductsDao obj = new ThunderFire.Business.ProductsDao();
            var result = obj.Select(pCODPRO);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion -- Produtos --



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
            int _usercode = 0;
            int _usersel = 0;
            try
            {
                _usercode = int.Parse(HttpContext.Request["usercode"].FirstOrDefault().ToString());
            }
            catch { }

            try
            {
                _usersel = int.Parse(HttpContext.Request["USRSEL"].FirstOrDefault().ToString());
            }
            catch { }

            model.CodigoUsuario = _usercode;
            model.UsuarioSelecionado = _usersel;
            ThunderFire.Business.GeneralRegistryDao obj1 = new ThunderFire.Business.GeneralRegistryDao();
            model.ListaUsuario = obj1.List(null, null, null, null, null);
            if (_usercode > 0)
                model.ListaUsuario = model.ListaUsuario.Where(p => p.CODUSU == _usercode).ToList();

            ThunderFire.Business.GeneralTableDao obj2 = new ThunderFire.Business.GeneralTableDao();
            model.ListaUF = obj2.List(ThunderFire.Domain.Constants.TABELA_UNIDADES_FEDERACAO);

            model.ListaTipoLogradouro = obj2.List(ThunderFire.Domain.Constants.TABELA_LOGRADOUROS);
            ThunderFire.Business.AddressTypeDao obj3 = new ThunderFire.Business.AddressTypeDao();
            model.ListaTipoEndereco = obj3.List();
            model.ListaPais = obj2.List(ThunderFire.Domain.Constants.TABELA_PAISES);
            return model;
        }

        [HttpPost]
        public JsonResult ChangeAddressBook(byte modo, AddressBook entry)
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
        public JsonResult QueryAddressBook(System.Int32 pCODUSU, short pTIPEND, short pREGATV, short pSTAREC)
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

        

            ThunderFire.Business.AddressBookDao obj = new ThunderFire.Business.AddressBookDao();
            var result = obj.List(pCODUSU, pTIPEND, pREGATV,pSTAREC);
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

            obj = null;
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelectAddressBook(int pCODEND)
        {
            ThunderFire.Business.AddressBookDao obj = new ThunderFire.Business.AddressBookDao();
            var result = obj.Select(pCODEND);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion -- Enderecos --

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
            int _usercode = 0;
            int _addresscode = 0;
            try
            {
                _usercode = int.Parse(HttpContext.Request["usercode"].FirstOrDefault().ToString());
                _addresscode = int.Parse(HttpContext.Request["addresscode"].FirstOrDefault().ToString());
            }
            catch { }

            try
            {
                _addresscode = int.Parse(HttpContext.Request["USRSEL"].FirstOrDefault().ToString());
            }
            catch { }

            model.CodigoUsuario = _usercode;
            model.CodigoEndereco = _addresscode;

            ThunderFire.Business.GeneralRegistryDao obj1 = new ThunderFire.Business.GeneralRegistryDao();
            model.ListaUsuario = obj1.List(null, null, null, null, null);
            if (_usercode > 0)
                model.ListaUsuario = model.ListaUsuario.Where(p => p.CODUSU == _usercode).ToList();

            ThunderFire.Business.GeneralTableDao obj2 = new ThunderFire.Business.GeneralTableDao();
            model.ListaOperadora = obj2.List(ThunderFire.Domain.Constants.TABELA_OPERADORAS_TELEFONIA);
            model.ListaPais = obj2.List(ThunderFire.Domain.Constants.TABELA_PAISES);
            ThunderFire.Business.ContactTypeDao obj3 = new ThunderFire.Business.ContactTypeDao();
            model.ListaTipoContato = obj3.List();
            return model;
        }


        [HttpPost]
        public JsonResult ChangeContactBook(byte modo, ContactBook entry)
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
        public JsonResult QueryContactBook(System.Int32 pCODUSU)
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



            ThunderFire.Business.ContactBookDao obj = new ThunderFire.Business.ContactBookDao();
            var result = obj.List(pCODUSU);
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

            obj = null;
            return Json(resultout, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelectContactBook(int pCODCTO)
        {
            ThunderFire.Business.ContactBookDao obj = new ThunderFire.Business.ContactBookDao();
            var result = obj.Select(pCODCTO);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion -- Endereços --


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
            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            model.ListaModulos = obj.List(ThunderFire.Domain.Constants.TABELA_MODULOS);
            return model;
        }

        [HttpPost]
        public JsonResult ChangeTransactionStatus(byte modo, TransactionStatus entry)
        {
            ExecutionResponse result = new ExecutionResponse();
            ThunderFire.Business.TransactionStatusDao obj = new ThunderFire.Business.TransactionStatusDao();
            entry.UPDUSU = SessionControl.Current.User.CODUSU;
            if (modo == 1)
                result = obj.Insert(entry);
            if (modo == 2)
                result = obj.Update(entry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult QueryTransactionStatus(System.Int16 pCODMOD)
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



            ThunderFire.Business.TransactionStatusDao obj = new ThunderFire.Business.TransactionStatusDao();
            var result = obj.List(pCODMOD);
            int count = 0;
            List<QueryTransactionStatus> data = null;
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
        public JsonResult SelectTransactionStatus(short pCODSTA)
        {
            ThunderFire.Business.TransactionStatusDao obj = new ThunderFire.Business.TransactionStatusDao();
            var result = obj.Select(pCODSTA);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion -- StatusTransacao --

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
            ThunderFire.Business.GeneralTableDao obj = new ThunderFire.Business.GeneralTableDao();
            model.ListaBancos = obj.List(ThunderFire.Domain.Constants.TABELA_BANCOS);
            model.ListaOrigemConta = obj.List(ThunderFire.Domain.Constants.TABELA_ORIGEM_CONTA);
            model.ListaTipoBeneficiario = obj.List(ThunderFire.Domain.Constants.TABELA_TIPO_BENEFICIARIO);
            ThunderFire.Business.TransactionStatusDao obj1 = new ThunderFire.Business.TransactionStatusDao();
            model.ListaStatusConta = obj1.List(ThunderFire.Domain.Constants.MODULO_CONTAS);
            ThunderFire.Business.GeneralRegistryDao obj2 = new ThunderFire.Business.GeneralRegistryDao();
            model.ListaUsuario = obj2.ListUserAccounts();
            ThunderFire.Business.AccountTypeDao obj3 = new ThunderFire.Business.AccountTypeDao();
            model.ListaTipoConta = obj3.List();
            return model;
        }

        [HttpPost]
        public JsonResult ChangeVirtualAccount(byte modo, VirtualAccount entry)
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
        public JsonResult SelectVirtualAccount(short pNIDCTA)
        {
            ThunderFire.Business.VirtualAccountDao obj = new ThunderFire.Business.VirtualAccountDao();
            var result = obj.Select(pNIDCTA);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult QueryVirtualAccount()
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



            ThunderFire.Business.VirtualAccountDao obj = new ThunderFire.Business.VirtualAccountDao();
            var result = obj.List();
            int count = 0;
            List<QueryVirtualAccount> data = null;
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


        #endregion -- Conta Virtual --
    }
}
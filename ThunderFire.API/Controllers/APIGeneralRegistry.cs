using ThunderFire;
using System;
using System.Net;
using System.Reflection;
using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Cors;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;
using ThunderFire.Business;
namespace ThunderFire.API.Services.Controllers
{
/// <summary>
/// ThunderFire.API.Services
/// </summary>
[EnableCors(origins: "*", headers: "*", methods: "*")]
[RoutePrefix("generalregistry")]
    public class GeneralRegistryController : ApiController
  {
private GeneralRegistryDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new GeneralRegistryDao();
  if (WRKOBJ != null)
  {
      if (WRKOBJ.Connected)
          return true;
  }
  return false;
}
/// <summary>
/// Construtor Base
/// </summary>
public GeneralRegistryController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBCADGER (Cadastro Geral)
/// </summary>
///<param name="model">GeneralRegistry</param>
/// <returns>ExecutionResponse</returns>
[HttpPost]
public IHttpActionResult Insert(GeneralRegistry model)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.Insert(model);
}
else
{
RETURN_VALUE.MessageToUser="Servico não disponível";
go = HttpStatusCode.ServiceUnavailable;
}
RETURN_VALUE.StatusCode=(int)go;
return Content(go, RETURN_VALUE);
}
/// <summary>
/// Insere um registro na tabela TBCADGER (Cadastro Geral)
/// </summary>
///<param name="model">GeneralRegistry</param>
/// <returns>ExecutionResponse</returns>
[HttpPost]
public IHttpActionResult Update(GeneralRegistry model)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.Update(model);
}
else
{
RETURN_VALUE.MessageToUser="Servico não disponível";
go = HttpStatusCode.ServiceUnavailable;
}
RETURN_VALUE.StatusCode=(int)go;
return Content(go, RETURN_VALUE);
}
    /// <summary>
    /// Seleciona o registro de acordo com o Código do Usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuario</param>

    /// <returns>GeneralRegistry</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pCODUSU)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pCODUSU);
if(WRKOBJ.Found)
{
go = HttpStatusCode.OK;
}
else
{
if(WRKOBJ.HasError)
{
    go=HttpStatusCode.BadRequest;
}
else
go=HttpStatusCode.NotFound;
}
}
return Content(go, RETURN_VALUE);
}
    /// <summary>
    /// Obtêm uma lista de registros do cadastro geral conforme parâmetros informados
    /// </summary>
        /// <param name="pCODATR">Atributo</param>
    /// <param name="pSTAUSU">Status do Usuário</param>
    /// <param name="pSRCUSU">ID do Responsável</param>
    /// <param name="pNOMUSU">Nome</param>
    /// <param name="pSTAREC">Status do Registro</param>

    /// <returns>List of QueryGeneralRegistry</returns>
[HttpGet]
    public IHttpActionResult List(System.Int16? pCODATR, System.Int16? pSTAUSU, System.Int32? pSRCUSU, System.String pNOMUSU, System.Byte? pSTAREC)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.List(pCODATR.Value, pSTAUSU.Value, pSRCUSU.Value, pNOMUSU, pSTAREC.Value);
if(WRKOBJ.Found)
{
go = HttpStatusCode.OK;
}
else
{
if(WRKOBJ.HasError)
{
    go=HttpStatusCode.BadRequest;
}
else
go=HttpStatusCode.NotFound;
}
}
return Content(go, RETURN_VALUE);
}
    /// <summary>
    /// Obtêm uma lista de usuários com contas virtuais ativas
    /// </summary>
    
    /// <returns>List of GeneralRegistry</returns>
[HttpGet]
    public IHttpActionResult ListUserAccounts()
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.ListUserAccounts();
if(WRKOBJ.Found)
{
go = HttpStatusCode.OK;
}
else
{
if(WRKOBJ.HasError)
{
    go=HttpStatusCode.BadRequest;
}
else
go=HttpStatusCode.NotFound;
}
}
return Content(go, RETURN_VALUE);
}
    /// <summary>
    /// Obtêm uma lista de usuários vinculados ao gerenciamento de Produto
    /// </summary>
        /// <param name="pCODPRO">Código do Produto</param>

    /// <returns>List of MyUsers</returns>
[HttpGet]
    public IHttpActionResult ListUserByProduct(System.Int16? pCODPRO)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.ListUserByProduct(pCODPRO.Value);
if(WRKOBJ.Found)
{
go = HttpStatusCode.OK;
}
else
{
if(WRKOBJ.HasError)
{
    go=HttpStatusCode.BadRequest;
}
else
go=HttpStatusCode.NotFound;
}
}
return Content(go, RETURN_VALUE);
}
    /// <summary>
    /// Obtêm uma lista de usuários por tipo de usuário
    /// </summary>
        /// <param name="pTIPUSU">Tipo de Usuário</param>
    /// <param name="pCODUSU">Código do Usuário</param>

    /// <returns>List of MyUsers</returns>
[HttpGet]
    public IHttpActionResult ListUsers(System.Byte? pTIPUSU, System.Int32? pCODUSU)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.ListUsers(pTIPUSU.Value, pCODUSU.Value);
if(WRKOBJ.Found)
{
go = HttpStatusCode.OK;
}
else
{
if(WRKOBJ.HasError)
{
    go=HttpStatusCode.BadRequest;
}
else
go=HttpStatusCode.NotFound;
}
}
return Content(go, RETURN_VALUE);
}
    /// <summary>
    /// Obtêm uma lista de usuários com permissão de uso da tarifação
    /// </summary>
    
    /// <returns>List of MyUsers</returns>
[HttpGet]
    public IHttpActionResult ListUserTarifacion()
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.ListUserTarifacion();
if(WRKOBJ.Found)
{
go = HttpStatusCode.OK;
}
else
{
if(WRKOBJ.HasError)
{
    go=HttpStatusCode.BadRequest;
}
else
go=HttpStatusCode.NotFound;
}
}
return Content(go, RETURN_VALUE);
}

    /// <summary>
    /// Valida um CPF/CNPJ já existente para o mesmo atributo na base de cadastro geral
    /// </summary>
    /// <param name="pCODATR">Código do Atributo</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODCMF">CPF/CNPJ</param>
    /// <param name="pSRCUSU">Usuário Gestor</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult ValidateEntryCMF(short pCODATR,System.Int32 pCODUSU,System.String pCODCMF,System.Int32 pSRCUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.ValidateEntryCMF(pCODATR,pCODUSU,pCODCMF,pSRCUSU);
}
else
{
RETURN_VALUE.MessageToUser="Servico não disponível";
go = HttpStatusCode.ServiceUnavailable;
}
RETURN_VALUE.StatusCode=(int)go;
return Content(go, RETURN_VALUE);
}

    /// <summary>
    /// Verifica se já existe um cadastro com o CPF/CNPJ cadastrado, e retorna o id localizado
    /// </summary>
    /// <param name="pCODATR">Código do Atributo</param>
    /// <param name="pCODCMF">CPF/CNPJ</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult PesquisarCMF(System.Int16 pCODATR,System.String pCODCMF)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.PesquisarCMF(pCODATR,pCODCMF);
}
else
{
RETURN_VALUE.MessageToUser="Servico não disponível";
go = HttpStatusCode.ServiceUnavailable;
}
RETURN_VALUE.StatusCode=(int)go;
return Content(go, RETURN_VALUE);
}


}
}

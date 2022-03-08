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
[RoutePrefix("virtualaccount")]
    public class VirtualAccountController : ApiController
  {
private VirtualAccountDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new VirtualAccountDao();
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
public VirtualAccountController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBCADCTA (Virtual Account)
/// </summary>
///<param name="model">VirtualAccount</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(VirtualAccount model)
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
/// Insere um registro na tabela TBCADCTA (Virtual Account)
/// </summary>
///<param name="model">VirtualAccount</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(VirtualAccount model)
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
    /// Seleciona o registro de conta virtual de acordo com o id da conta
    /// </summary>
        /// <param name="pNIDCTA">ID da Conta Virtual</param>

    /// <returns>VirtualAccount</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pNIDCTA)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNIDCTA);
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
    /// Seleciona o registro de conta virtual de acordo com os parâmetros fornecidos
    /// </summary>
        /// <param name="pNUMBCO">Banco</param>
    /// <param name="pNUMAGE">Agência</param>
    /// <param name="pNUMCTA">Conta</param>
    /// <param name="pNUMDVE">DV</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pORGCTA">Origem da Conta</param>
    /// <param name="pTIPCTA">Tipo de Conta</param>

    /// <returns>VirtualAccount</returns>
[HttpGet]
    public IHttpActionResult Select(System.String pNUMBCO, System.String pNUMAGE, System.String pNUMCTA, System.String pNUMDVE, System.Int32 pCODUSU, System.Byte pORGCTA= 2, System.Int32 pTIPCTA= 2)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNUMBCO, pNUMAGE, pNUMCTA, pNUMDVE, pCODUSU, pORGCTA, pTIPCTA);
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
    /// Obtêm o registro de conta virtual de acordo com o cpf/cnpj informado
    /// </summary>
        /// <param name="pCODCMF">Código do CPF/CNPJ</param>
    /// <param name="pORGCTA">Origem da Conta</param>

    /// <returns>VirtualAccount</returns>
[HttpGet]
    public IHttpActionResult Select(System.String pCODCMF, System.Byte? pORGCTA)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pCODCMF, pORGCTA.Value);
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
    /// Obtêm todos os registros de contas virtuais registradas conforme parametro fornecido
    /// </summary>
        /// <param name="pCODUSU">Usuário</param>

    /// <returns>QueryVirtualAccount</returns>
[HttpGet]
    public IHttpActionResult List(System.Int32 pCODUSU= null)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.List(pCODUSU);
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
    /// Exclui logicamente um registro de conta
    /// </summary>
    /// <param name="pNIDCTA">Id da Conta</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult DeleteAccountID(System.Int32 pNIDCTA,System.Int32 pUPDUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.DeleteAccountID(pNIDCTA,pUPDUSU);
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
    /// Executa a aprovação ou rejeição de uma conta digital
    /// </summary>
    /// <param name="pNIDCTA">ID da Conta</param>
    /// <param name="pCODOPE">Código da Operação</param>
    /// <param name="pUPDUSU">Usuário Aprovador</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult ChangeStatusAccount(System.Int32 pNIDCTA,System.Byte pCODOPE,System.Int32 pUPDUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.ChangeStatusAccount(pNIDCTA,pCODOPE,pUPDUSU);
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
    /// Localiza uma conta virtual a partir do código de usuário
    /// </summary>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pORGCTA">Origem da Conta</param>
    /// <param name="pTIPCTA">Tipo de Conta</param>
/// <returns>ExecutionResponse</returns>[HttpGet]
public IHttpActionResult Locate(System.Int32 pCODUSU,System.Byte pORGCTA = 1,System.Int32 pTIPCTA = 1)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.Locate(pCODUSU,pORGCTA,pTIPCTA);
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

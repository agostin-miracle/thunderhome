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
[RoutePrefix("accountentryposting")]
    public class AccountEntryPostingController : ApiController
  {
private AccountEntryPostingDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new AccountEntryPostingDao();
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
public AccountEntryPostingController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBCADLCT (Tabela de Cadastro de Lançamentos)
/// </summary>
///<param name="model">AccountEntryPosting</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(AccountEntryPosting model)
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
/// Insere um registro na tabela TBCADLCT (Tabela de Cadastro de Lançamentos)
/// </summary>
///<param name="model">AccountEntryPosting</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(AccountEntryPosting model)
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
    /// Obtêm o registro de Cadastro de Lançamento com base no id informado
    /// </summary>
        /// <param name="pNIDLCT">ID do Registro de Cadastro de Lançamento</param>

    /// <returns>AccountEntryPosting</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pNIDLCT)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNIDLCT);
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
    /// Copia um conjunto de cadastro de lançamentos para outros
    /// </summary>
    /// <param name="pTIPLCT">Tipo de Lançamento de Origem</param>
    /// <param name="pNEWTIP">Tipo de Lançamento de Destino</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
/// <returns>int</returns>[HttpPost]
public IHttpActionResult Copy(System.Int16 pTIPLCT,System.Int16 pNEWTIP,System.Int32 pUPDUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
int RETURN_VALUE = new int();
if(Init())
{
RETURN_VALUE = WRKOBJ.Copy(pTIPLCT,pNEWTIP,pUPDUSU);
}
else
{
go = HttpStatusCode.ServiceUnavailable;
}
RETURN_VALUE.StatusCode=(int)go;
return Content(go, RETURN_VALUE);
}

    /// <summary>
    /// Exclui um conjunto de cadastro de lançamentos
    /// </summary>
    /// <param name="pTIPLCT">Tipo de Lançamento de Origem</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
/// <returns>int</returns>[HttpPost]
public IHttpActionResult Delete(System.Int16 pTIPLCT,System.Int32 pUPDUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
int RETURN_VALUE = new int();
if(Init())
{
RETURN_VALUE = WRKOBJ.Delete(pTIPLCT,pUPDUSU);
}
else
{
go = HttpStatusCode.ServiceUnavailable;
}
RETURN_VALUE.StatusCode=(int)go;
return Content(go, RETURN_VALUE);
}


}
}

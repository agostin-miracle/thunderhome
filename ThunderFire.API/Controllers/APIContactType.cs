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
[RoutePrefix("contacttype")]
    public class ContactTypeController : ApiController
  {
private ContactTypeDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new ContactTypeDao();
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
public ContactTypeController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBTIPCTO (Tipos de Contato)
/// </summary>
///<param name="model">ContactType</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(ContactType model)
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
/// Insere um registro na tabela TBTIPCTO (Tipos de Contato)
/// </summary>
///<param name="model">ContactType</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(ContactType model)
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
    /// Obtêm o registro do Tipo de contato informado
    /// </summary>
        /// <param name="pTIPCTO">Tipo de Contato</param>

    /// <returns>ContactType</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pTIPCTO)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pTIPCTO);
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


}
}

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
[RoutePrefix("addresstype")]
    public class AddressTypeController : ApiController
  {
private AddressTypeDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new AddressTypeDao();
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
public AddressTypeController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBTIPEND (Address Type)
/// </summary>
///<param name="model">AddressType</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(AddressType model)
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
/// Insere um registro na tabela TBTIPEND (Address Type)
/// </summary>
///<param name="model">AddressType</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(AddressType model)
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
    /// Obtêm o registro de Tipo de Endereço com base no id informado
    /// </summary>
        /// <param name="pTIPEND">Tipo de Endereço</param>

    /// <returns>AddressType</returns>
[HttpGet]
    public IHttpActionResult Select(System.Byte pTIPEND)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pTIPEND);
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

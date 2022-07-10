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
[RoutePrefix("addressbook")]
    public class AddressBookController : ApiController
  {
private AddressBookDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new AddressBookDao();
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
public AddressBookController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBCADEND (Cadasto de Endereços)
/// </summary>
///<param name="model">AddressBook</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(AddressBook model)
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
/// Insere um registro na tabela TBCADEND (Cadasto de Endereços)
/// </summary>
///<param name="model">AddressBook</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(AddressBook model)
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
    /// Obtêm o registro de Endereço
    /// </summary>
        /// <param name="pCODEND">Código do Endereço</param>

    /// <returns>AddressBook</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pCODEND)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pCODEND);
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
    /// Localiza o ID de um endereço com base nos parametros fornecidos
    /// </summary>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pDSCEND">Descrição do Endereço</param>
    /// <param name="pTIPEND">Tipo de Endereço</param>
    /// <param name="pREGATV">Status de Registro</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult Find(System.Int32 pCODUSU,System.String pDSCEND,System.Byte pTIPEND,System.Byte? pREGATV)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.Find(pCODUSU,pDSCEND,pTIPEND,pREGATV);
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

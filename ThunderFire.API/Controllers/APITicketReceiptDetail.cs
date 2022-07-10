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
[RoutePrefix("ticketreceiptdetail")]
    public class TicketReceiptDetailController : ApiController
  {
private TicketReceiptDetailDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new TicketReceiptDetailDao();
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
public TicketReceiptDetailController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBBXABOL (Registro de Detalhe do Recebimento de Boleto)
/// </summary>
///<param name="model">TicketReceiptDetail</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(TicketReceiptDetail model)
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
    /// Obtêm o Registro de Baixa de acordo com o id
    /// </summary>
        /// <param name="pNIDRBD">ID do Registro de Detalhe de Recebimento de Boleto</param>

    /// <returns>TicketReceiptDetail</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pNIDRBD)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNIDRBD);
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

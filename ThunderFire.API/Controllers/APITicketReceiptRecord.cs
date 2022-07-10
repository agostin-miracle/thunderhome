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
[RoutePrefix("ticketreceiptrecord")]
    public class TicketReceiptRecordController : ApiController
  {
private TicketReceiptRecordDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new TicketReceiptRecordDao();
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
public TicketReceiptRecordController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBRBBBOL (Ticket Receipt Record)
/// </summary>
///<param name="model">TicketReceiptRecord</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(TicketReceiptRecord model)
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
/// Insere um registro na tabela TBRBBBOL (Ticket Receipt Record)
/// </summary>
///<param name="model">TicketReceiptRecord</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(TicketReceiptRecord model)
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
    /// Seleciona o registro de acordo com o id de registro do conta corrente
    /// </summary>
        /// <param name="pNIDRBB">ID do Registro de Recebimento de Boleto</param>

    /// <returns>TicketReceiptRecord</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pNIDRBB)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNIDRBB);
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
    /// Executa o fechamento de um lote de Registro de Recebimento de Boleto
    /// </summary>
    /// <param name="pNIDRBB">id do Registro de Recebimento de Boleto</param>
/// <returns>int</returns>[HttpPost]
public IHttpActionResult CloseBatch(System.Int32 pNIDRBB)
{
HttpStatusCode go = HttpStatusCode.OK;
int RETURN_VALUE = new int();
if(Init())
{
RETURN_VALUE = WRKOBJ.CloseBatch(pNIDRBB);
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

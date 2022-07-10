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
[RoutePrefix("ticket")]
    public class TicketController : ApiController
  {
private TicketDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new TicketDao();
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
public TicketController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBREGBOL (Registro de Boletos)
/// </summary>
///<param name="model">Ticket</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(Ticket model)
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
/// Insere um registro na tabela TBREGBOL (Registro de Boletos)
/// </summary>
///<param name="model">Ticket</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(Ticket model)
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
    /// Seleciona o registro de Boleto de acordo com o ID
    /// </summary>
        /// <param name="pNIDBOL">ID do Boleto</param>

    /// <returns>Ticket</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pNIDBOL)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNIDBOL);
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
    /// Encerra um boleto
    /// </summary>
    /// <param name="pNIDBOL">ID do Boleto</param>
    /// <param name="pSTAREC">Status do Registro</param>
    /// <param name="pTIPBXA">Tipo de Baixa</param>
    /// <param name="pSTABOL">Status do Boleto</param>
    /// <param name="pDATPGT">Data de Pagamento</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
/// <returns>int</returns>[HttpPos]
public IHttpActionResult CloseTicket(int pNIDBOL,System.Byte pSTAREC,System.Int16 pTIPBXA,System.Int16 pSTABOL,System.DateTime pDATPGT,int pUPDUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
int RETURN_VALUE = new int();
if(Init())
{
RETURN_VALUE = WRKOBJ.CloseTicket(pNIDBOL,pSTAREC,pTIPBXA,pSTABOL,pDATPGT,pUPDUSU);
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

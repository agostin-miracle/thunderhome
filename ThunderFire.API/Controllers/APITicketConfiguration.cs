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
[RoutePrefix("ticketconfiguration")]
    public class TicketConfigurationController : ApiController
  {
private TicketConfigurationDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new TicketConfigurationDao();
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
public TicketConfigurationController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBCFGBOL (Ticket Configuration)
/// </summary>
///<param name="model">TicketConfiguration</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(TicketConfiguration model)
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
/// Insere um registro na tabela TBCFGBOL (Ticket Configuration)
/// </summary>
///<param name="model">TicketConfiguration</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(TicketConfiguration model)
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
    /// Obtêm o registro de configuração de boleto conforme o id informado
    /// </summary>
        /// <param name="pNIDCBL">ID do Registro de Configuração de Boleto</param>

    /// <returns>TicketConfiguration</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pNIDCBL)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNIDCBL);
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
    /// Localiza um id de registro de configuração de boleto de acordo com os parâmetros fornecidos
    /// </summary>
/// <remarks>
/// <para>A busca inicial procura pelo usuário cedente do boleto, no nivel de configuração usuário, caso não encontre, procura pelo Código do Gestor no nivel de gestor, caso não encontre, procura pelo padrão</para>
/// </remarks>
    /// <param name="pUSUPRO">Código do Gestor</param>
    /// <param name="pUSUCED">Código do Cedente</param>
    /// <param name="pTIPBOL">Tipo de Boleto</param>
/// <returns>int</returns>
public IHttpActionResult Locate(System.Int32 pUSUPRO,System.Int32 pUSUCED,System.Byte pTIPBOL)
{
HttpStatusCode go = HttpStatusCode.OK;
int RETURN_VALUE = new int();
if(Init())
{
RETURN_VALUE = WRKOBJ.Locate(pUSUPRO,pUSUCED,pTIPBOL);
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

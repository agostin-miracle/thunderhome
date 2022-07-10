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
[RoutePrefix("monthlypayment")]
    public class MonthlyPaymentController : ApiController
  {
private MonthlyPaymentDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new MonthlyPaymentDao();
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
public MonthlyPaymentController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBREGMEN (Monthly Payment)
/// </summary>
///<param name="model">MonthlyPayment</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(MonthlyPayment model)
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
/// Insere um registro na tabela TBREGMEN (Monthly Payment)
/// </summary>
///<param name="model">MonthlyPayment</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(MonthlyPayment model)
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
    /// Obtêm o registro de mensalidade de acordo com o ID
    /// </summary>
        /// <param name="pNIDMEN">ID do Registro de Mensalidade</param>

    /// <returns>MonthlyPayment</returns>
[HttpGet]
    public IHttpActionResult Select(int pNIDMEN)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNIDMEN);
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
    /// Localiza um registro de mensalidae conforme os parâmetros abaixo
    /// </summary>
        /// <param name="pTIPMEN">Tipo de Mensalidade</param>
    /// <param name="pCODREF">Código de Referência</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pREGBXA">Indicador de Baixa</param>

    /// <returns>List of MonthlyPayment</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pTIPMEN, System.Int32 pCODREF, System.Int32 pCODUSU, System.Byte pREGBXA= 0)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pTIPMEN, pCODREF, pCODUSU, pREGBXA);
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

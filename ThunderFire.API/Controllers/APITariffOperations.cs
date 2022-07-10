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
[RoutePrefix("tariffoperations")]
    public class TariffOperationsController : ApiController
  {
private TariffOperationsDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new TariffOperationsDao();
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
public TariffOperationsController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBTARMOV (Associação de Tarifas e Operações)
/// </summary>
///<param name="model">TariffOperations</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(TariffOperations model)
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
/// Insere um registro na tabela TBTARMOV (Associação de Tarifas e Operações)
/// </summary>
///<param name="model">TariffOperations</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(TariffOperations model)
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
    /// Seleciona o registro de acordo com o codigo do produto
    /// </summary>
        /// <param name="pNIDTXM">ID Tarifa x Movimento</param>

    /// <returns>TariffOperations</returns>
[HttpGet]
    public IHttpActionResult Select(int pNIDTXM)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNIDTXM);
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
    /// Seleciona todos os registros de expansão de tarifa do id de tarifação fornecido
    /// </summary>
    
    /// <returns>List of TariffOperations</returns>
[HttpGet]
    public IHttpActionResult List()
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.List();
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
    /// Obtêm a Lista de Tarifas
    /// </summary>
    
    /// <returns>List of TariffType</returns>
[HttpGet]
    public IHttpActionResult ListTariffs()
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.ListTariffs();
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
    /// Obtêm a Lista de Operacoes
    /// </summary>
    
    /// <returns>List of Operations</returns>
[HttpGet]
    public IHttpActionResult ListOperations()
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.ListOperations();
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

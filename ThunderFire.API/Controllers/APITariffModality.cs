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
[RoutePrefix("tariffmodality")]
    public class TariffModalityController : ApiController
  {
private TariffModalityDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new TariffModalityDao();
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
public TariffModalityController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBMODTAR (Tariff Modality)
/// </summary>
///<param name="model">TariffModality</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(TariffModality model)
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
/// Insere um registro na tabela TBMODTAR (Tariff Modality)
/// </summary>
///<param name="model">TariffModality</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(TariffModality model)
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
    /// Seleciona o registro de acordo com o codigo dA modalidade
    /// </summary>
        /// <param name="pMODCRT">Modalidade de Aplicação da Tarifa</param>

    /// <returns>TariffModality</returns>
[HttpGet]
    public IHttpActionResult Select(int pMODCRT)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pMODCRT);
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
    /// Seleciona todos os registros de modalidade de tarifa existentes
    /// </summary>
    
    /// <returns>List of TariffModality</returns>
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


}
}

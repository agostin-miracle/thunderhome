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
[RoutePrefix("tariff")]
    public class TariffController : ApiController
  {
private TariffDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new TariffDao();
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
public TariffController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBCADTAR (Tariffs)
/// </summary>
///<param name="model">Tariff</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(Tariff model)
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
/// Insere um registro na tabela TBCADTAR (Tariffs)
/// </summary>
///<param name="model">Tariff</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(Tariff model)
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
    /// Obtêm o Id de Tarifação Tarifação de Acordo com o ID de Registro de Tarifação fornecido
    /// </summary>
        /// <param name="pNIDTAR">ID do Registro de Tarifação</param>

    /// <returns>Tariff</returns>
[HttpGet]
    public IHttpActionResult Select(int pNIDTAR)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNIDTAR);
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
    /// Seleciona todos os registros com base nos parâmetros fornecidos
    /// </summary>
        /// <param name="pUSUCFG">Código do Usuario</param>
    /// <param name="pNIVCFG">Nivel de Tarifação</param>

    /// <returns>QueryTariff</returns>
[HttpGet]
    public IHttpActionResult List(int pUSUCFG, System.Byte pNIVCFG)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.List(pUSUCFG, pNIVCFG);
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
    /// Obtêm a tarifa expandida de registro de tarifação
    /// </summary>
/// <remarks>
/// <para>Retorna o id de Registro de Aplicação de uma Tarifa [TBCALTAR]</para>
/// </remarks>
    /// <param name="pNIDTAR">ID do Registro de Tarifação</param>
    /// <param name="pVLRTRA">Valor da Transação</param>
/// <returns>int</returns>[HttpGet]
public IHttpActionResult GetExpandeTariff(System.Int32 pNIDTAR,System.Double pVLRTRA = 0)
{
HttpStatusCode go = HttpStatusCode.OK;
int RETURN_VALUE = new int();
if(Init())
{
RETURN_VALUE = WRKOBJ.GetExpandeTariff(pNIDTAR,pVLRTRA);
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

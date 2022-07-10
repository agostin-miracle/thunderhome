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
[RoutePrefix("generaltable")]
    public class GeneralTableController : ApiController
  {
private GeneralTableDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new GeneralTableDao();
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
public GeneralTableController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBTABGER (Tabela Geral do Sistema)
/// </summary>
///<param name="model">GeneralTable</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(GeneralTable model)
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
/// Insere um registro na tabela TBTABGER (Tabela Geral do Sistema)
/// </summary>
///<param name="model">GeneralTable</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(GeneralTable model)
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
    /// Seleciona o registro de acordo com o id de registro da tabela geral
    /// </summary>
        /// <param name="pKEYTAB">ID de Registro da Tabela</param>

    /// <returns>GeneralTable</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pKEYTAB)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pKEYTAB);
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
    /// Seleciona todos os registros de um Tipo de tabela informado
    /// </summary>
        /// <param name="pNUMTAB">Código da Tabela de Acesso</param>

    /// <returns>List of GeneralTable</returns>
[HttpGet]
    public IHttpActionResult List(System.Int32 pNUMTAB)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.List(pNUMTAB);
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
    /// Obtêm o Id de Registro de uma Tabela Geral Baseada no Código Chave da Tabela
    /// </summary>
    /// <param name="pNUMTAB">Código do Tabela</param>
    /// <param name="pKEYCOD">Código Numérico da Chave</param>
/// <returns>ExecutionResponse</returns>[HttpGet]
public IHttpActionResult FindKey(System.Int32 pNUMTAB,System.Int32 pKEYCOD)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.FindKey(pNUMTAB,pKEYCOD);
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
    /// Obtêm o Id de Registro de uma Tabela Geral Baseada no Código Chave Texto da Tabela
    /// </summary>
    /// <param name="pNUMTAB">Código do Tabela</param>
    /// <param name="pKEYTXT">Código Texto da Chave</param>
/// <returns>ExecutionResponse</returns>[HttpGet]
public IHttpActionResult FindKeyText(System.Int32 pNUMTAB,System.String pKEYTXT)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.FindKeyText(pNUMTAB,pKEYTXT);
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

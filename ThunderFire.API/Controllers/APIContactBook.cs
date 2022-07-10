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
[RoutePrefix("contactbook")]
    public class ContactBookController : ApiController
  {
private ContactBookDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new ContactBookDao();
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
public ContactBookController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBCADCTO (Tabela de Contatos)
/// </summary>
///<param name="model">ContactBook</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(ContactBook model)
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
/// Insere um registro na tabela TBCADCTO (Tabela de Contatos)
/// </summary>
///<param name="model">ContactBook</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(ContactBook model)
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
    /// Seleciona o registro de acordo com o código do cadastro de contatos
    /// </summary>
        /// <param name="pCODCTO">ID do Registro de Contato</param>

    /// <returns>ContactBook</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pCODCTO)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pCODCTO);
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
    /// Obtêm o registro de contato de acordo com os parâmetros informados
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pTIPCTO">Tipo de Contato</param>
    /// <param name="pREGATV">Registro Ativo</param>
    /// <param name="pSTAREC">Status de Registro</param>

    /// <returns>ContactBook</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pCODUSU, System.Byte pTIPCTO, System.Byte pREGATV= 1, System.Byte pSTAREC= 1)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pCODUSU, pTIPCTO, pREGATV, pSTAREC);
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
    /// Localiza um contato com base nos parâmetros fornecidos
    /// </summary>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODEND">Código do Endereço</param>
    /// <param name="pTIPCTO">Tipo de Contato</param>
    /// <param name="pNUMTEL">Telefone</param>
    /// <param name="pCODPAI">Código do Pais</param>
    /// <param name="pCODOPR">Código da Operadora</param>
    /// <param name="pNUMDDD">Número do DDD</param>
    /// <param name="pREGATV">Indicador de Atividade do Registro</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult Find(System.Int32 pCODUSU,System.Int32 pCODEND,System.Int32 pTIPCTO,System.String pNUMTEL,System.Int16? pCODPAI,System.Int16? pCODOPR,System.String pNUMDDD,System.Byte? pREGATV)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.Find(pCODUSU,pCODEND,pTIPCTO,pNUMTEL,pCODPAI,pCODOPR,pNUMDDD,pREGATV);
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

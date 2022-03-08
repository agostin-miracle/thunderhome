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
[RoutePrefix("productcards")]
    public class ProductCardsController : ApiController
  {
private ProductCardsDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new ProductCardsDao();
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
public ProductCardsController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBCADCRT (Cadastro de Cartões)
/// </summary>
///<param name="model">ProductCards</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(ProductCards model)
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
/// Insere um registro na tabela TBCADCRT (Cadastro de Cartões)
/// </summary>
///<param name="model">ProductCards</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(ProductCards model)
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
/// Insere um registro na tabela TBCADCRT (Cadastro de Cartões)
/// </summary>
///<param name="model">ProductCards</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult MigrateTo(ProductCards model)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.MigrateTo(model);
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
    /// Seleciona o registro de acordo com o Código do Cartão
    /// </summary>
        /// <param name="pCODCRT">Código do Cartão</param>

    /// <returns>ProductCards</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pCODCRT)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pCODCRT);
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
    /// Seleciona o registro de acordo com o Código Extendido do Cartão
    /// </summary>
        /// <param name="pNUMCRT">Código Extendido do Cartão</param>

    /// <returns>ProductCards</returns>
[HttpGet]
    public IHttpActionResult Select(System.String pNUMCRT)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNUMCRT);
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
    /// Verifica se o cartão já foi alocado para um portador
    /// </summary>
/// <remarks>
/// <para>ADM : Verifica se o cartão pode ser alocado, retorna :</para>
/// <para>[-1] - indicado que o cartão não possui uma alocação.</para>
/// <para>[ 1] - Indicando que o cartão possui uma alocação.</para>
/// <para>[ 0] - Se o cartão não foi encontrado</para>
/// </remarks>
    /// <param name="pCODCRT">ID do Cartão</param>
/// <returns>int</returns>[HttpGet]
public IHttpActionResult IsAlocated(System.Int32 pCODCRT)
{
HttpStatusCode go = HttpStatusCode.OK;
int RETURN_VALUE = new int();
if(Init())
{
RETURN_VALUE = WRKOBJ.IsAlocated(pCODCRT);
}
else
{
go = HttpStatusCode.ServiceUnavailable;
}
RETURN_VALUE.StatusCode=(int)go;
return Content(go, RETURN_VALUE);
}

    /// <summary>
    /// ADM : Altera o Status de um Cartão
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pSTACRT">Novo Status do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pCODBLO">Código de Desbloqueio</param>
    /// <param name="pDSCMOT">Motivo</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult ChangeStatus(System.Int32 pCODCRT,System.Int16 pSTACRT,System.Int32 pUPDUSU,System.Int16 pCODBLO = 0,System.String pDSCMOT = "''")
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.ChangeStatus(pCODCRT,pSTACRT,pUPDUSU,pCODBLO,pDSCMOT);
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
    /// Efetua a alteração da senha do cartão
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pPSWCRT">Novo Senha do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pNUMIPA">Endereço de Localização</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult ChangePin(System.Int32 pCODCRT,System.String pPSWCRT,System.Int32 pUPDUSU,System.String pNUMIPA)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.ChangePin(pCODCRT,pPSWCRT,pUPDUSU,pNUMIPA);
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
    /// Efetua a alteração do número o CVC do Cartão
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pNUMCVC">Novo Número do CVC</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult ChangeCVC(System.Int32 pCODCRT,System.Int16 pNUMCVC,System.Int32 pUPDUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.ChangeCVC(pCODCRT,pNUMCVC,pUPDUSU);
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
    /// Efetua o cancelamento da associação de um cartão
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult CancelAssociation(System.Int32 pCODCRT,System.Int32 pUPDUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.CancelAssociation(pCODCRT,pUPDUSU);
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

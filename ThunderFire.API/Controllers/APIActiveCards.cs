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
[RoutePrefix("activecards")]
    public class ActiveCardsController : ApiController
  {
private ActiveCardsDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new ActiveCardsDao();
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
public ActiveCardsController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBREGCRT (Active Cards)
/// </summary>
///<param name="model">ActiveCards</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(ActiveCards model)
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
/// Insere um registro na tabela TBREGCRT (Active Cards)
/// </summary>
///<param name="model">ActiveCards</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(ActiveCards model)
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
    /// Seleciona o registro de acordo com o Código do Cartão
    /// </summary>
        /// <param name="pCODCRT">Código do Cartão</param>

    /// <returns>ActiveCards</returns>
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

    /// <returns>ActiveCards</returns>
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
    /// Altera o campo de permissão de compra on-line
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pAPLCON">Habilitar Compra Online</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult ChangeOnlineShop(System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pAPLCON)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.ChangeOnlineShop(pCODCRT,pUPDUSU,pAPLCON);
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
    /// Altera o campo de permissão de saque
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pAPLSAQ">Habilitar Compra Online</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult ChangeWithdraw(System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pAPLSAQ)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.ChangeWithdraw(pCODCRT,pUPDUSU,pAPLSAQ);
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
    /// Altera o Gestor do Cartão em uso
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pUSUPRO">Código do Gestor</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult ChangeOwner(System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Int32 pUSUPRO = 13)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.ChangeOwner(pCODCRT,pUPDUSU,pUSUPRO);
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
    /// Executa Ações de Atualização pela manutenção do Cartão
    /// </summary>
/// <remarks>
/// <para>1 - Reativa a mensalidade de um usuário do cartão</para>
/// <para>3 - Inativa a mensalidade de um usuário do cartão</para>
/// </remarks>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pCODACT">Código de Ação</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult CancelMonthlyFees(System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pCODACT)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.CancelMonthlyFees(pCODCRT,pUPDUSU,pCODACT);
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
    /// Verifica se o cartão e o CPF/CNPJ informado são passíveis de ativação
    /// </summary>
    /// <param name="pNUMCRT">Número do Cartão</param>
    /// <param name="pCODCMF">Código do CPF/CNPJ</param>
/// <returns>int</returns>[HttpGet]
public IHttpActionResult IsActivatable(System.String pNUMCRT,System.String pCODCMF)
{
HttpStatusCode go = HttpStatusCode.OK;
int RETURN_VALUE = new int();
if(Init())
{
RETURN_VALUE = WRKOBJ.IsActivatable(pNUMCRT,pCODCMF);
}
else
{
go = HttpStatusCode.ServiceUnavailable;
}
RETURN_VALUE.StatusCode=(int)go;
return Content(go, RETURN_VALUE);
}

    /// <summary>
    /// Operação@1 -Bloqueia o Cartão@2 - Reverte o Bloqueio Anterior@3 -Fixa o cartão como aguardando desbloqueio
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pTIPOPE">Tipo de Operação de bloqueio</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult LockCard(System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pTIPOPE)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.LockCard(pCODCRT,pUPDUSU,pTIPOPE);
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
    /// Altera o Status de um Cartão
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
    /// Cancela a associação de um cartão
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

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
[RoutePrefix("linkedaccount")]
    public class LinkedAccountController : ApiController
  {
private LinkedAccountDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new LinkedAccountDao();
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
public LinkedAccountController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBCADCVL (Linked Account)
/// </summary>
///<param name="model">LinkedAccount</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(LinkedAccount model)
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
/// Insere um registro na tabela TBCADCVL (Linked Account)
/// </summary>
///<param name="model">LinkedAccount</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(LinkedAccount model)
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
    /// Obtêm o registro da conta vinculada com base no id informado
    /// </summary>
        /// <param name="pNIDCVL">ID da Conta Vinculada</param>

    /// <returns>LinkedAccount</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pNIDCVL)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pNIDCVL);
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
    /// Remove um conta vinculada da lista
    /// </summary>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pNIDCTA">Id da Conta</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
/// <returns>int</returns>POST
public IHttpActionResult RemoveLinkedID(int pCODUSU,int pNIDCTA,int pUPDUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
int RETURN_VALUE = new int();
if(Init())
{
RETURN_VALUE = WRKOBJ.RemoveLinkedID(pCODUSU,pNIDCTA,pUPDUSU);
}
else
{
go = HttpStatusCode.ServiceUnavailable;
}
RETURN_VALUE.StatusCode=(int)go;
return Content(go, RETURN_VALUE);
}

    /// <summary>
    /// Localiza uma conta digital conforme os parâmetros fornecidos
    /// </summary>
/// <remarks>
/// <para>Retona o ID da Conta Virtual associada, caso contrário indica erro de pesquisa</para>
/// </remarks>
    /// <param name="pMETPSQ">Método de Pesquisa (1 - POR CPF/CNPF, 2 - POR CONTA)</param>
    /// <param name="pNOMPSQ">Valor de Pesquisa</param>
    /// <param name="pCODUSU">Código do Usuário</param>
/// <returns>int</returns>
public IHttpActionResult LocateAccount(System.Byte pMETPSQ,System.String pNOMPSQ,System.Int32 pCODUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
int RETURN_VALUE = new int();
if(Init())
{
RETURN_VALUE = WRKOBJ.LocateAccount(pMETPSQ,pNOMPSQ,pCODUSU);
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

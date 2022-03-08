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
[RoutePrefix("loginuser")]
    public class LoginUserController : ApiController
  {
private LoginUserDao WRKOBJ = null;
[NonAction]
private bool Init()
{
  if (WRKOBJ == null)
    WRKOBJ = new LoginUserDao();
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
public LoginUserController ()
{
/* NO CONTENT */
}

/// <summary>
/// Insere um registro na tabela TBLGNUSU (Login de Usuário)
/// </summary>
///<param name="model">LoginUser</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Insert(LoginUser model)
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
/// Insere um registro na tabela TBLGNUSU (Login de Usuário)
/// </summary>
///<param name="model">LoginUser</param>
/// <returns>ExecutionResponse</returns>
public IHttpActionResult Update(LoginUser model)
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
    /// Obtêm o registro de Login do Usuário
    /// </summary>
        /// <param name="pLGNNUM">ID de Registro de Login</param>

    /// <returns>LoginUser</returns>
[HttpGet]
    public IHttpActionResult Select(System.Int32 pLGNNUM)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Select(pLGNNUM);
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
    /// Obtêm o registro de Login Ativo do Usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>

    /// <returns>LoginUser</returns>
[HttpGet]
    public IHttpActionResult Get(System.Int32 pCODUSU)
    {
HttpStatusCode go = HttpStatusCode.OK;
object RETURN_VALUE=null;
if (Init())
{
 RETURN_VALUE = WRKOBJ.Get(pCODUSU);
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
    /// Obtêm o registro de controle de acesso do usuário
    /// </summary>
    /// <param name="pLGNNUM">ID de Registro de Login</param>
/// <returns>AccessControl</returns>[HttpGet]
public IHttpActionResult GetAccessControl(System.Int32 pLGNNUM)
{
HttpStatusCode go = HttpStatusCode.OK;
AccessControl RETURN_VALUE = new AccessControl();
if(Init())
{
RETURN_VALUE = WRKOBJ.GetAccessControl(pLGNNUM);
}
else
{
go = HttpStatusCode.ServiceUnavailable;
}
RETURN_VALUE.StatusCode=(int)go;
return Content(go, RETURN_VALUE);
}

    /// <summary>
    /// Efetua o logoff de um usuario
    /// </summary>
    /// <param name="pLGNNUM">ID do Logon</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult Logoff(System.Int32 pLGNNUM)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.Logoff(pLGNNUM);
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
    /// Efetua um login com base nas credenciais de acesso
    /// </summary>
    /// <param name="pLGNTYP">Tipo de Acesso</param>
    /// <param name="pLGNUSU">Login</param>
    /// <param name="pPSWUSU">Senha</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult Login(System.Byte pLGNTYP,System.String pLGNUSU,System.String pPSWUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.Login(pLGNTYP,pLGNUSU,pPSWUSU);
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
    /// Verifica se o usuario precisa fazer um refresh de senha
    /// </summary>
    /// <param name="pLGNNUM">ID do Login</param>
/// <returns>ExecutionResponse</returns>[HttpGet]
public IHttpActionResult NeedRefresh(System.Int32 pLGNNUM)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.NeedRefresh(pLGNNUM);
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
    /// Efetua um login com base nas credenciais de acesso
    /// </summary>
/// <remarks>
/// <para>Códigos de Retorno</para>
/// <para>[-1]= O Usuário deverá fazer o reset de senha</para>
/// <para>[-2] = Usuário não existente</para>
/// <para>[>0] = Id de Registro de Login</para>
/// </remarks>
    /// <param name="pLGNUSU">Login</param>
    /// <param name="pPSWOLD">Senha Anterior</param>
    /// <param name="pPSWUSU">Senha</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult ChangePassword(System.String pLGNUSU,System.String pPSWOLD,System.String pPSWUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.ChangePassword(pLGNUSU,pPSWOLD,pPSWUSU);
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
    /// Efetua um login com base nas credenciais de acesso
    /// </summary>
    /// <param name="pLGNNUM">Login</param>
    /// <param name="pPSWUSU">Senha</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
/// <returns>ExecutionResponse</returns>[HttpPost]
public IHttpActionResult ResetPassword(System.Int32 pLGNNUM,System.String pPSWUSU,System.Int32 pUPDUSU)
{
HttpStatusCode go = HttpStatusCode.OK;
ExecutionResponse RETURN_VALUE = new ExecutionResponse();
if(Init())
{
RETURN_VALUE.ReturnValue = WRKOBJ.ResetPassword(pLGNNUM,pPSWUSU,pUPDUSU);
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

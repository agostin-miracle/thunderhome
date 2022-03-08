using Livre.Business;
using Livre.Models;
using MazeFire;
using MazeFire.Logs;
using System;
using System.Net;
using System.Reflection;
using System.Web.Http;
using System.Data;
using System.Web.Http.Cors;
using System.Collections.Generic;
namespace ApiServices.Controllers
{
/// <summary>
/// Listas
/// </summary>
[EnableCors(origins: "*", headers: "*", methods: "*")]
[RoutePrefix("listas")]
    public class ListasController : ApiController
  {
/// <summary>
/// Constructor Base
/// </summary>
    public ListasController(){}



}
}

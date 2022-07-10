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

    /// <summary>
    /// Seleciona todos os Códigos de Índices da Tabela Geral
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 0</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  Tables()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.Tables();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Países
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 1</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  Countries()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.Countries();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Unidades da Federação
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 2</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  UF()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.UF();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Status de Registro
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 7</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  RegistrationStatus()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.RegistrationStatus();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Sinal de Operação
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 10</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  Signals()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.Signals();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Bancos
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 12</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  Banks()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.Banks();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Gêneros
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 13</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  Genders()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.Genders();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Módulos do Aplicativo
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 14</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  Modules()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.Modules();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Tipos de Boleto
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 16</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  TicketTypes()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.TicketTypes();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Tipos de Beneficiario
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 20</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  BeneficiaryTypes()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.BeneficiaryTypes();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Origem da Conta
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 23</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  AccountOrigin()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.AccountOrigin();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Condição de Bloqueio
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 29</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  BlockingCondition()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.BlockingCondition();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Indicadores de Lançamento
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 39</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  AccountEntryIndicators()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.AccountEntryIndicators();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Operadoras de Telefonia
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 45</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  TelephoneOperators()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.TelephoneOperators();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Tipos de Mensalidade
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 50</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  MonthlyPaymentTypes()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.MonthlyPaymentTypes();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Níveis de Confinaça
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 54</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  TrustLevel()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.TrustLevel();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Tipo de Transação de Movimento
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 59</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  TransactionMovement()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.TransactionMovement();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Nacionalidades
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 80</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  Nationalities()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.Nationalities();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Logradouros
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 81</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  PublicPlaces()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.PublicPlaces();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Subsistemas
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 93</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  Subsystems()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.Subsystems();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Responsável pela Tarifa
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 105</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  TariffResponsible()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.TariffResponsible();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Associação de Origem
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 106</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  OriginAssociation()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.OriginAssociation();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Regra de Aplicação da Taxa do Depositante
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 912</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  DepositorFee()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.DepositorFee();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Nivel de Configuração
    /// </summary>
    /// <remarks>
    /// <para>Tabela/Atributo de referência : 947</para>
    /// </remarks>
    /// <returns>List of GeneralTable</returns>
[HttpGet]
public  IHttpActionResult  ConfigurationLevel()
{
List<GeneralTable> RETURN_VALUE = new List<GeneralTable>();
RETURN_VALUE = Livre.Business.Listas.ConfigurationLevel();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm o registro de calculo conforme o id informado
    /// </summary>
    /// <returns>Tariffed</returns>
[HttpGet]
public  IHttpActionResult  GetTariffed()
{
Tariffed RETURN_VALUE = new Tariffed();
RETURN_VALUE = Livre.Business.Listas.GetTariffed();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Avalista
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  Guarantor()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.Guarantor();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Gestores Ativos associados à tabela de usuários
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  Managers()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.Managers();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Gestores Ativos associados à tabela de usuários por usuário
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  Managers()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.Managers();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Gestores por linha de produto
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  ManagersByLine()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.ManagersByLine();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de usuários com contas virtuais ativas
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  UsersAccounts(System.Int32? pCODUSU)
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.UsersAccounts(pCODUSU);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Contas Virtuais com o propósito de efetuar a vinculação para um determinado usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  UsersLinkeds(System.Int32 pCODUSU)
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.UsersLinkeds(pCODUSU);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de usuários vinculados ao gerenciamento de Produto
    /// </summary>
        /// <param name="pCODPRO">Código do Produto</param>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  UsersByProduct(System.Int16? pCODPRO)
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.UsersByProduct(pCODPRO);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Usuários com atributo gestor
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  UsersManagers()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.UsersManagers();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de usuários por tipo de usuário
    /// </summary>
        /// <param name="pTIPUSU">Tipo de Usuário</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  UsersByType(System.Byte? pTIPUSU, System.Int32? pCODUSU)
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.UsersByType(pTIPUSU, pCODUSU);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de usuários com permissão de uso da tarifação
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  UserTarifacion()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.UserTarifacion();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de usuários com base no nome fornecido
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  GetUsersByName()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.GetUsersByName();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Usuários contidos em um boleto
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  UsersInTickets()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.UsersInTickets();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Usuários contidos em mensalidades
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  UsersInMonthly()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.UsersInMonthly();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Usuários contidos em um boleto
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  UsersForTickets()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.UsersForTickets();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de registros do cadastro geral conforme parâmetros informados
    /// </summary>
        /// <param name="pCODATR">Atributo</param>
    /// <param name="pSTAUSU">Status do Usuário</param>
    /// <param name="pSRCUSU">ID do Responsável</param>
    /// <param name="pNOMUSU">Nome</param>
    /// <param name="pSTAREC">Status do Registro</param>
    /// <returns>List of QueryUsers</returns>
[HttpGet]
public  IHttpActionResult  Users(System.Int16? pCODATR, System.Int16? pSTAUSU, System.Int32? pSRCUSU, System.String pNOMUSU, System.Byte? pSTAREC)
{
List<QueryUsers> RETURN_VALUE = new List<QueryUsers>();
RETURN_VALUE = Livre.Business.Listas.Users(pCODATR, pSTAUSU, pSRCUSU, pNOMUSU, pSTAREC);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de registros do cadastro geral simplificada por codio e descrição
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pSRCUSU">Código do Gestor</param>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  Users(System.Int32? pCODUSU, System.Int32? pSRCUSU)
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.Users(pCODUSU, pSRCUSU);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Tipos de Endereço
    /// </summary>
    /// <returns>List of AddressType</returns>
[HttpGet]
public  IHttpActionResult  AddressTypes()
{
List<AddressType> RETURN_VALUE = new List<AddressType>();
RETURN_VALUE = Livre.Business.Listas.AddressTypes();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Tipos de Contato
    /// </summary>
    /// <returns>List of ContactType</returns>
[HttpGet]
public  IHttpActionResult  ContactTypes()
{
List<ContactType> RETURN_VALUE = new List<ContactType>();
RETURN_VALUE = Livre.Business.Listas.ContactTypes();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Tipos de Usuário
    /// </summary>
    /// <returns>List of UserType</returns>
[HttpGet]
public  IHttpActionResult  UserTypes()
{
List<UserType> RETURN_VALUE = new List<UserType>();
RETURN_VALUE = Livre.Business.Listas.UserTypes();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Tipos de Atributo
    /// </summary>
    /// <returns>List of AttributeType</returns>
[HttpGet]
public  IHttpActionResult  AttributeTypes()
{
List<AttributeType> RETURN_VALUE = new List<AttributeType>();
RETURN_VALUE = Livre.Business.Listas.AttributeTypes();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Linha de Produtos
    /// </summary>
    /// <returns>List of ProductLine</returns>
[HttpGet]
public  IHttpActionResult  ProductLines()
{
List<ProductLine> RETURN_VALUE = new List<ProductLine>();
RETURN_VALUE = Livre.Business.Listas.ProductLines();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Tipos de Conta
    /// </summary>
    /// <returns>List of AccountType</returns>
[HttpGet]
public  IHttpActionResult  AccountTypes()
{
List<AccountType> RETURN_VALUE = new List<AccountType>();
RETURN_VALUE = Livre.Business.Listas.AccountTypes();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm a Lista de Operacoes
    /// </summary>
        /// <param name="pSUBSYS">Subsistema</param>
    /// <returns>List of Operations</returns>
[HttpGet]
public  IHttpActionResult  Operations(System.Byte? pSUBSYS)
{
List<Operations> RETURN_VALUE = new List<Operations>();
RETURN_VALUE = Livre.Business.Listas.Operations(pSUBSYS);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Status das Transações de acordo com o módulo informado
    /// </summary>
        /// <param name="pCODMOD">Código do Módulo</param>
    /// <returns>List of TransactionStatus</returns>
[HttpGet]
public  IHttpActionResult  Transactions(System.Int16? pCODMOD)
{
List<TransactionStatus> RETURN_VALUE = new List<TransactionStatus>();
RETURN_VALUE = Livre.Business.Listas.Transactions(pCODMOD);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de todos os endereços de um usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pTIPEND">Tipo de Endereço</param>
    /// <param name="pREGATV">Registro Ativo</param>
    /// <param name="pSTAREC">Status de Registro</param>
    /// <returns>List of QueryAddressBook</returns>
[HttpGet]
public  IHttpActionResult  Addresses(System.Int32 pCODUSU, System.Int16 pTIPEND= -1, System.Int16 pREGATV= -1, System.Int16 pSTAREC= -1)
{
List<QueryAddressBook> RETURN_VALUE = new List<QueryAddressBook>();
RETURN_VALUE = Livre.Business.Listas.Addresses(pCODUSU, pTIPEND, pREGATV, pSTAREC);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Seleciona todos os registros de contato do usuário fornecido
    /// </summary>
        /// <param name="pCODUSU">Usuário</param>
    /// <returns>List of QueryContactBook</returns>
[HttpGet]
public  IHttpActionResult  Contacts(System.Int32? pCODUSU)
{
List<QueryContactBook> RETURN_VALUE = new List<QueryContactBook>();
RETURN_VALUE = Livre.Business.Listas.Contacts(pCODUSU);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de todos os cartões da base ativa conforme parâmetros de pesquisa informados
    /// </summary>
        /// <param name="pUSUPRO">Usuário Gestor</param>
    /// <param name="pSTACRT">Status do Cartão</param>
    /// <param name="pNUMCRT">Parte ou Número do Cartão</param>
    /// <param name="pNOMPRT">Nome ou Parte</param>
    /// <returns>List of QueryActiveCards</returns>
[HttpGet]
public  IHttpActionResult  ListCards(System.Int32 pUSUPRO, System.Int16? pSTACRT= null, System.String pNUMCRT= '', System.String? pNOMPRT= null)
{
List<QueryActiveCards> RETURN_VALUE = new List<QueryActiveCards>();
RETURN_VALUE = Livre.Business.Listas.ListCards(pUSUPRO, pSTACRT, pNUMCRT, pNOMPRT);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm os registros de mensalidade conforme os parâmetros informados
    /// </summary>
        /// <param name="pUSUPRO">Gestor de Produto</param>
    /// <param name="pTIPMEN">Tipo de Mensalidade</param>
    /// <param name="pSTAMEN">Status da Mensalidade</param>
    /// <param name="pCODUSU">Código do Usuario</param>
    /// <returns>List of QueryMonthlyPayment</returns>
[HttpGet]
public  IHttpActionResult  MonthlyPayments(int? pUSUPRO, System.Byte pTIPMEN, System.Int32? pSTAMEN, int? pCODUSU)
{
List<QueryMonthlyPayment> RETURN_VALUE = new List<QueryMonthlyPayment>();
RETURN_VALUE = Livre.Business.Listas.MonthlyPayments(pUSUPRO, pTIPMEN, pSTAMEN, pCODUSU);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm os registros de resumo de mensalidade conforme os parâmetros informados
    /// </summary>
        /// <param name="pUSUPRO">Gestor de Produto</param>
    /// <param name="pTIPMEN">Tipo de Mensalidade</param>
    /// <param name="pSTAMEN">Status da Mensalidade</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <returns>List of QueryMonthlyPayment</returns>
[HttpGet]
public  IHttpActionResult  MonthlyPaymentSummary(int? pUSUPRO, System.Byte pTIPMEN, System.Int16? pSTAMEN, System.Int32? pCODUSU)
{
List<QueryMonthlyPayment> RETURN_VALUE = new List<QueryMonthlyPayment>();
RETURN_VALUE = Livre.Business.Listas.MonthlyPaymentSummary(pUSUPRO, pTIPMEN, pSTAMEN, pCODUSU);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de usuários vinculados as mensalidades
    /// </summary>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  ListUsers()
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.ListUsers();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de todos os produtos
    /// </summary>
        /// <param name="pLINPRO">Linha de Produto</param>
    /// <returns>List of Product</returns>
[HttpGet]
public  IHttpActionResult  Products(System.Int16 pLINPRO)
{
List<Product> RETURN_VALUE = new List<Product>();
RETURN_VALUE = Livre.Business.Listas.Products(pLINPRO);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista Tipos de Tarifa
    /// </summary>
    /// <returns>List of TariffType</returns>
[HttpGet]
public  IHttpActionResult  TariffTypes()
{
List<TariffType> RETURN_VALUE = new List<TariffType>();
RETURN_VALUE = Livre.Business.Listas.TariffTypes();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm todos os registros de contas virtuais registradas conforme parametro fornecido
    /// </summary>
        /// <param name="pSTACTA">Status da Conta</param>
    /// <param name="pNOMUSU">Nome ou parte do nome</param>
    /// <returns>List of Accounts</returns>
[HttpGet]
public  IHttpActionResult  Accounts(System.Int16 pSTACTA= null, System.String pNOMUSU= "")
{
List<Accounts> RETURN_VALUE = new List<Accounts>();
RETURN_VALUE = Livre.Business.Listas.Accounts(pSTACTA, pNOMUSU);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Contas vinculadas por usuário
    /// </summary>
        /// <param name="pNOMUSU">Código do Usuario</param>
    /// <returns>List of LinkedAccount</returns>
[HttpGet]
public  IHttpActionResult  LinkedAccounts(System.String? pNOMUSU)
{
List<LinkedAccount> RETURN_VALUE = new List<LinkedAccount>();
RETURN_VALUE = Livre.Business.Listas.LinkedAccounts(pNOMUSU);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Listagem de Gestores de Produtos
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODPRO">Código do Produto</param>
    /// <returns>List of ProductManager</returns>
[HttpGet]
public  IHttpActionResult  ProductManagers(System.Int32 pCODUSU= null, System.Int16 pCODPRO= null)
{
List<ProductManager> RETURN_VALUE = new List<ProductManager>();
RETURN_VALUE = Livre.Business.Listas.ProductManagers(pCODUSU, pCODPRO);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm todos os registro de configuração de boleto de acordo com os parâmetros fornecidos
    /// </summary>
        /// <param name="pNIVCFG">Nivel de Configuração</param>
    /// <param name="pUSUCFG">Código do Usuário de configuração</param>
    /// <returns>List of TicketConfiguration</returns>
[HttpGet]
public  IHttpActionResult  TicketConfigurations(System.Byte pNIVCFG, System.Int32? pUSUCFG)
{
List<TicketConfiguration> RETURN_VALUE = new List<TicketConfiguration>();
RETURN_VALUE = Livre.Business.Listas.TicketConfigurations(pNIVCFG, pUSUCFG);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm informações do boleto para fatura
    /// </summary>
        /// <param name="pNIDBOL">ID do Boleto</param>
    /// <returns>List of TicketInvoice</returns>
[HttpGet]
public  IHttpActionResult  TicketInvoice(System.Int32 pNIDBOL)
{
List<TicketInvoice> RETURN_VALUE = new List<TicketInvoice>();
RETURN_VALUE = Livre.Business.Listas.TicketInvoice(pNIDBOL);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm informações do boleto para obtenção de registro
    /// </summary>
        /// <param name="pNIDBOL">ID do Boleto</param>
    /// <returns>TicketRegister</returns>
[HttpGet]
public  IHttpActionResult  TicketForRegister(System.Int32 pNIDBOL)
{
TicketRegister RETURN_VALUE = new TicketRegister();
RETURN_VALUE = Livre.Business.Listas.TicketForRegister(pNIDBOL);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Boletos conforme parâmetros
    /// </summary>
        /// <param name="pUSUPRO">ID do Gestor</param>
    /// <param name="pUSUCED">ID do Cedente</param>
    /// <param name="pUSUSAC">ID do Sacado</param>
    /// <param name="pCODAVA">ID do Avalista</param>
    /// <param name="pSTABOL">Status do Boleto</param>
    /// <param name="pTIPBOL">Tipo de Boleto</param>
    /// <param name="pNIDBOL">ID do Boleto</param>
    /// <param name="pDATEMI1">Data de Emissão Inicial (yyyyMMdd)</param>
    /// <param name="pDATEMI2">Data de Emissão Final (yyyyMMdd HH:mm:ss)</param>
    /// <param name="pDATVCT1">Data de Vencimento Inicial (yyyyMMdd)</param>
    /// <param name="pDATVCT2">Data de Vencimento Final (yyyyMMdd HH:mm:ss)</param>
    /// <param name="pDATPGT1">Data de Pagamento Inicial (yyyyMMdd)</param>
    /// <param name="pDATPGT2">Data de Pagamento Final (yyyyMMdd HH:mm:ss)</param>
    /// <returns>List of TicketQuery</returns>
[HttpGet]
public  IHttpActionResult  Tickets(System.Int32? pUSUPRO, System.Int32? pUSUCED, System.Int32? pUSUSAC, System.Int32? pCODAVA, System.Int16? pSTABOL, System.Int16? pTIPBOL, System.Int32? pNIDBOL, System.String pDATEMI1, System.String pDATEMI2, System.String pDATVCT1, System.String pDATVCT2, System.String pDATPGT1, System.String pDATPGT2)
{
List<TicketQuery> RETURN_VALUE = new List<TicketQuery>();
RETURN_VALUE = Livre.Business.Listas.Tickets(pUSUPRO, pUSUCED, pUSUSAC, pCODAVA, pSTABOL, pTIPBOL, pNIDBOL, pDATEMI1, pDATEMI2, pDATVCT1, pDATVCT2, pDATPGT1, pDATPGT2);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm todos os registros de operações de um susbsistema e ID de referencia específico
    /// </summary>
        /// <param name="pSUBSYS">ID do SubSistema</param>
    /// <param name="pNIDREF">ID de Referência</param>
    /// <returns>List of OperationsRegister</returns>
[HttpGet]
public  IHttpActionResult  OperationsRegisters(System.Byte pSUBSYS, System.Int32 pNIDREF)
{
List<OperationsRegister> RETURN_VALUE = new List<OperationsRegister>();
RETURN_VALUE = Livre.Business.Listas.OperationsRegisters(pSUBSYS, pNIDREF);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Tipos de Lançamento
    /// </summary>
        /// <param name="pUSETRF">Usar Transferencias</param>
    /// <returns>List of AccountEntryType</returns>
[HttpGet]
public  IHttpActionResult  AccountEntryTypes(System.Byte? pUSETRF)
{
List<AccountEntryType> RETURN_VALUE = new List<AccountEntryType>();
RETURN_VALUE = Livre.Business.Listas.AccountEntryTypes(pUSETRF);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Cadastro de Lançamentos
    /// </summary>
        /// <param name="pTIPLCT">Tipo de Lançamento</param>
    /// <returns>List of AccountEntryPosting</returns>
[HttpGet]
public  IHttpActionResult  AccountEntryPostings(System.Int16? pTIPLCT)
{
List<AccountEntryPosting> RETURN_VALUE = new List<AccountEntryPosting>();
RETURN_VALUE = Livre.Business.Listas.AccountEntryPostings(pTIPLCT);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Lista de Feriados
    /// </summary>
    /// <returns>List of Holydays</returns>
[HttpGet]
public  IHttpActionResult  Holydays()
{
List<Holydays> RETURN_VALUE = new List<Holydays>();
RETURN_VALUE = Livre.Business.Listas.Holydays();
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm todos os registros de transferências de acordo com os parâmetros informados
    /// </summary>
        /// <param name="pTIPLCT">Tipo de Lançamento</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pNIDTRA">ID da Transação</param>
    /// <param name="pSTAREC">Status de Registro</param>
    /// <returns>List of TransferRegistration</returns>
[HttpGet]
public  IHttpActionResult  TransferRegistrations(System.Int16? pTIPLCT, System.Int32? pCODUSU, System.String? pNIDTRA, System.Byte? pSTAREC)
{
List<TransferRegistration> RETURN_VALUE = new List<TransferRegistration>();
RETURN_VALUE = Livre.Business.Listas.TransferRegistrations(pTIPLCT, pCODUSU, pNIDTRA, pSTAREC);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}

    /// <summary>
    /// Obtêm uma lista de usuários com contas virtuais ativas
    /// </summary>
        /// <param name="pFUNLCT">Funcao de Lançamento</param>
    /// <param name="pNOMUSU">Nome do Usuário</param>
    /// <returns>List of MyUsers</returns>
[HttpGet]
public  IHttpActionResult  AccountsByEntryCondition(System.Byte pFUNLCT, System.String pNOMUSU)
{
List<MyUsers> RETURN_VALUE = new List<MyUsers>();
RETURN_VALUE = Livre.Business.Listas.AccountsByEntryCondition(pFUNLCT, pNOMUSU);
if (RETURN_VALUE != null)
{
return Content(HttpStatusCode.OK, RETURN_VALUE);
}
return Content(HttpStatusCode.BadRequest, "");
}



}
}

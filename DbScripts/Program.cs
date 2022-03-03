using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderFire.Business;
using ThunderFire.Domain.Models;
using Dapper;
using NLog;
using System.Xml;
using ThunderFire.Connector;
using ThunderFire.Domain.DTO;

namespace DbScripts
{
    class Program
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {

            String SQL = @"SELECT ResetPassword = CAST (CASE WHEN RSTPSW=1 THEN 1 ELSE 0 END AS BIT)
       ,IsPointOfSale = CAST(0 AS BIT)
       ,IsTedApproval = CAST(0 AS BIT)
       ,LGNNUM       
       ,IsManagerProduct = CASE WHEN EXISTS(SELECT 1 FROM TBUSUPRO (NOLOCK) WHERE CODUSU = A.CODUSU AND STAREC=1) THEN 0 ELSE 1 END
	   ,Mail = ISNULL(F.DSCEND, '')
	   ,Mobile = ISNULL(G.NUMCEL, '')
       ,B.*
	   ,C.*
	   ,E.*
	   ,D.*
  FROM TBLGNUSU (NOLOCK) A
 INNER JOIN TBCADCTA (NOLOCK) B ON (B.CODUSU = A.CODUSU)
 INNER JOIN TBCADGER (NOLOCK) E ON (E.CODUSU = A.CODUSU)
 INNER JOIN TBREGCRT (NOLOCK) C ON (C.ASSUSU = A.CODUSU)
  LEFT JOIN TBCADEND (NOLOCK) D ON (D.CODUSU = A.CODUSU)
  LEFT JOIN (SELECT TOP 1 CODUSU, DSCEND FROM TBCADEND (NOLOCK) WHERE TIPEND=3 AND STAREC=1 AND REGATV=1) F ON (F.CODUSU = A.CODUSU)
  LEFT JOIN (SELECT TOP 1 CODUSU, NUMCEL =NUMDDD + NUMTEL FROM TBCADCTO (NOLOCK) WHERE TIPCTO=3 AND STAREC=1 AND REGATV=1) G ON (G.CODUSU = A.CODUSU)
WHERE (B.STAREC=1 AND B.ORGCTA = 1 AND B.STACTA=250)
AND (A.STAREC=1 AND A.REGATV=1) AND LGNNUM=@LGNNUM";

            AccessControl retorno = new AccessControl();
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {

                List<VirtualAccount> r1 = new List<VirtualAccount>();
                var result =_conn.Query<AccessControl>(SQL,
                            new[]
                            {
                                typeof(AccessControl),
                                typeof(VirtualAccount),
                                typeof(GeneralRegistry),
                                typeof(ActiveCards),
                                typeof(AddressBook)
                            },
                            objects =>
                            {
                                var ac = objects[0] as AccessControl;
                                var va = objects[1] as VirtualAccount;
                                var us = objects[2] as GeneralRegistry;
                                var cr = objects[3] as ActiveCards;
                                var ad = objects[4] as AddressBook;


                                retorno.Account = va;
                                retorno.Address = ad;
                                retorno.User = us;
                                retorno.Cards.Add(cr);
                                return ac;
                            },
                                                splitOn: "nidcta,codusu, codcrt,codend", param:new { LGNNUM = 1019 });
                result = null;
            }

                return;


                //TruncateTables();
                GeneralRegistry model = new GeneralRegistry();
                model.ATRPPE = false;
                model.CODATR = 2;
                model.CODCMF = "05866457807";
                model.CODNAC = 55;
                model.CODPJU = "J";
                model.DATNAC = new DateTime(1965, 4, 21);
                model.DSCOCO = "";
                model.NIVCFA = 3;
                model.NOMMAE = "MARIA DAS DORES SILVA MILAGRES";
                model.NOMUSU = "AGOSTINHO DA SILVA MILAGRES";
                model.NUMIRG = "";
                model.SRCUSU = 0;
                model.STAREC = 1;
                model.STAUSU = 1;
                model.TIPPES = "M";
                model.TIPUSU = 1;
                model.UPDUSU = 0;

                GeneralRegistryDao obj = new GeneralRegistryDao();

                //var retorno = obj.Insert(model);


                LoginUser user = new LoginUser();
                user.CODUSU = 26;
                user.LGNTYP = 1;
                user.LGNUSU = "AGOSTIN";
                user.PSWUSU = "1234";
                user.REGATV = 1;
                user.RSTPSW = 0;
                user.UPDUSU = 1;
                LoginUserDao userdao = new LoginUserDao();
                //var userret = userdao.Insert(user);


                // var userret =userdao.Login(1, "AGOSTIN", "1234");

                //var user1 = userdao.Select((int)userret.ReturnValue);
                //if (userdao.Found)
                //{
                //    userret = userdao.ChangePassword(user1.LGNUSU, user1.PSWUSU, "1235");
                //}

                //var LOGOFF = userdao.Logoff(7);

                TestTabelaGeral();

                _logger.Info("Efetuou logoff");

            }


            static void TestTabelaGeral()
            {
                GeneralTableDao oDao = new GeneralTableDao();
                var list = oDao.List(0);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        foreach (GeneralTable item in list)
                        {
                            _logger.Info(String.Format($" Record {item.NUMTAB} {item.KEYCOD} {item.DSCTAB}"));
                        }
                    }
                }
                else
                    _logger.Error("Erro na pesquisa de dados da tabela geral");

                _logger.Info("Pesquisando um item da tabela geral, tabela 14, codigo 1");
                var rtb1 = oDao.FindKey(14, 1);
                int id = Convert.ToInt32(rtb1.ReturnValue);
                if (id > 0)
                {
                    _logger.Info(String.Format("Retornou o id : {0}", id));
                    GeneralTable record = oDao.Select(id);
                    _logger.Info("Processando a alteração");
                    record.DSCTAB = "CADASTROS";
                    var rsp = oDao.Update(record);
                    int rv = Convert.ToInt32(rsp.ReturnValue);
                    if (rv > 0)
                        _logger.Info("Alteração Registrada");
                    else
                        _logger.Info(rsp.ErrorMessage);
                }

            }

            static void TruncateTables()
            {
                IDbConnection cn = ThunderFire.Connector.ConnectionFactory.GetConnection();

                if (cn != null)
                {
                    cn.Execute("truncate table tblgnusu");
                    cn.Execute("truncate table tbcadger");
                }

            }


            static string ComposeSystemFeatures()
            {
                string file = @"D:\ThunderHome\DbScripts\Docs\access.xml";
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                string sql = "";
                var root = doc.SelectSingleNode("/access");
                XmlNodeList nodes = root.SelectNodes("//item");
                if (nodes != null)
                {

                    for (int i = 0; i < nodes.Count; i++)
                    {
                        byte _sysprf = byte.Parse(nodes[i].SelectSingleNode("@platform").Value);
                        string _syskey = nodes[i].SelectSingleNode("@key").Value;
                        string _sysmth = nodes[i].SelectSingleNode("@method").Value;
                        string _sysprc = nodes[i].SelectSingleNode("@procedure").Value;
                        string _sysdsc = nodes[i].SelectSingleNode("@text").Value;
                        int _sysfun = int.Parse(_sysprf.ToString() + _syskey);
                        sql += string.Format("INSERT INTO TBSYSFUN (SYSFUN,SYSMTH,SYSPRC,SYSDSC) VALUES ({0},'{1}','{2}','{3}')", _sysfun, _sysmth, _sysprc, _sysdsc) + Environment.NewLine;
                    }
                }
                return sql;
            }

        }
    } 

using System;
using System.Linq;
using ThunderFire;
using Dapper;
using NLog;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using ThunderFire.Connector;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;
using ThunderFire.Interface;


namespace ThunderFire.Business
{

    ///<summary>
    /// Regra de Negócio para TBBXABOL
    ///</summary>
    ///<remarks>
    ///Empresa     : Agostinho da Silva Milagres
    ///Copyright   : Copyright © 2012
    ///Descricao   : Gerador de Objetos
    ///Produto     : SQLDBTools
    ///Titulo      : SQLDBTools
    ///Version     : 1.3.0.0
    ///Data        : 10/04/2022 11:51
    ///Alias       : ticketreceiptdetail
    ///Descrição   : Registro de Detalhe do Recebimento de Boleto
    ///</remarks>
    public partial class TicketReceiptDetailDao : BusinessBase, ITicketReceiptDetail
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Instância Base
        /// </summary>
        public TicketReceiptDetailDao()
        {
            this.Connected = true;
            this.KeyTableId = 39;

        }

        /// <summary>
        /// Insere um registro na tabela TBBXABOL (Registro de Detalhe do Recebimento de Boleto)
        /// </summary>
        ///<param name="model">TicketReceiptDetail</param>
        /// <returns>int</returns>
        public ExecutionResponse Insert(TicketReceiptDetail model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            this.HasError = false;
            this.ProcessCode = 10;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _changed = Objects.GetPropertiesValue("Registro de Detalhe do Recebimento de Boleto", model, true);

                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@NIDRBB", model.NIDRBB, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@NIDBOL", model.NIDBOL, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@IDXSEQ", model.IDXSEQ, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@TIPBXA", model.TIPBXA, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DATPGT", model.DATPGT, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@NUMPCL", model.NUMPCL, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@VLRPAG", model.VLRPAG, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@VLRMOR", model.VLRMOR, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@VLRJUR", model.VLRJUR, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@VLRDES", model.VLRDES, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@VLRLIQ", model.VLRLIQ, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@VLRTEX", model.VLRTEX, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@DSCOBS", model.DSCOBS, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRBXABOLINS", p, commandType: CommandType.StoredProcedure);
                    RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                    respond.ReturnValue = RETURN_VALUE;
                    string _errormessage = "";
                    if (RETURN_VALUE > 0)
                    {
                        respond.MessageToUser = "BOLETO BAIXADO COM SUCESSO";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == 0)
                    {
                        respond.MessageToUser = "FALHA NA LEITURA DO REGISTRO DE BAIXA";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -1)
                    {
                        respond.MessageToUser = "FALHA NA ATUALIZAÇÃO DA INCLUSAO DO REGISTRO DE BAIXA";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -2)
                    {
                        respond.MessageToUser = "BOLETO NAO EXISTE";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -3)
                    {
                        respond.MessageToUser = "BOLETO NAO ESTA EM ABERTO";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -4)
                    {
                        respond.MessageToUser = "REGISTRO DE BAIXA JÁ EXISTENTE";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -5)
                    {
                        respond.MessageToUser = "VALOR DO RECEBIMENTO DIFERE DO SALDO DO BOLETO";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -6)
                    {
                        respond.MessageToUser = "FALHA NA ATUALIZAÇÃO DE BAIXA DO BOLETO";
                        _errormessage = "";
                    }
                }
                catch (Exception Error)
                {
                    _logger.Info(Error);
                    this.HasError = true;
                    respond.ReturnValue = RETURN_VALUE;
                    respond.StatusCode = 400;
                    respond.MessageToUser = "FALHA NA INCLUSAO DO REGISTRO";
                    respond.ErrorMessage = Error.Message;
                    var msg = ErrorManager.GetError(ErrorManager.GetErrorCode(Error.Message));
                    if (msg != null)
                    {
                        respond.SourceError = msg.Source;
                        respond.ErrorCode = msg.ErrorCode;
                        respond.ErrorObject = Error;
                        if (!String.IsNullOrEmpty(msg.Message))
                            respond.MessageToUser = msg.Message;
                        respond.Severity = msg.Severity;
                    }
                }
            }
            return respond;
        }
        /// <summary>
        /// Obtêm o Registro de Baixa de acordo com o id
        /// </summary>
        /// <param name="pNIDRBD">ID do Registro de Detalhe de Recebimento de Boleto</param>
        /// <returns>TicketReceiptDetail</returns>
        public TicketReceiptDetail Select(System.Int32 pNIDRBD)
        {
            this.Found = false;
            this.ProcessCode = 0;
            TicketReceiptDetail RETURN_VALUE = null;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    RETURN_VALUE = _conn.Query<TicketReceiptDetail>(sql: "PRBXABOLSEL", param: new
                    {
                        NIDRBD = pNIDRBD
                    }, commandType: CommandType.StoredProcedure, commandTimeout: 120).FirstOrDefault();

                    if (RETURN_VALUE != null)
                        this.Found = true;
                }
                catch (Exception Error)
                {
                    this.HasError = true;
                    this.Found = false;
                    _logger.Info(Error);
                }
            }
            return RETURN_VALUE;
        }

    }
}

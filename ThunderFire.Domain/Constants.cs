using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;

namespace ThunderFire.Domain
{
    public enum MODULOS
    {
        BOLETOS = 1,
        CADASTRO_GERAL = 4,
        CONTAS = 7,
        CARTOES = 5,
        MENSALIDADES = 10,
        POS = 9
    }
    public enum ATRIBUTOS
    {
        EMPRESA = 1,
        USUARIOS = 2,
        GESTORES = 3,
        CLIENTES = 4,
        FORNECEDORES = 5,
        SACADOS = 6
    }

    

    public class Constants
    {


        public const short PRODUTO_BOLETO = 4;

        #region ** LOGIN **

        public readonly byte LOGIN_BY_USER = 1;
        public readonly byte LOGIN_BY_EMAIL = 2;
        public readonly byte LOGIN_BY_CARD = 3;
        public readonly byte LOGIN_BY_CMF = 4;

        #endregion ** LOGIN **


        #region ** AUDITORIA **

        public readonly byte AUDITORIA_INSERIR_LOGIN = 1;
        public readonly byte AUDITORIA_ALTERAR_LOGIN = 2;
        public readonly byte AUDITORIA_CHANGE_PASSWORD = 3;
        public readonly byte AUDITORIA_RESET_PASSWORD = 4;
        public readonly byte AUDITORIA_LOGIN = 5;
        public readonly byte AUDITORIA_LOGOFF = 6;
        #endregion ** LOGIN **


        #region ** ATRIBUTOS **


        #endregion


        #region ** TABELAS INTERNAS **

        public const int TABELA_PAISES = 1;
        public const int TABELA_UNIDADES_FEDERACAO = 2;
        public const int TABELA_PROXIMOS_NUMEROS = 6;
        public const int TABELA_STATUS_REGISTRO = 7;
        public const int TABELA_TIPO_PESSOA = 8;
        public const int TABELA_SINAL_OPERACAO = 10;
        public const int TABELA_BANCOS = 12;
        public const int TABELA_GENERO = 13;
        public const int TABELA_MODULOS = 14;
        public const int TABELA_TIPOS_BOLETO = 16;
        public const int TABELA_TIPO_BENEFICIARIO = 20;
        public const int TABELA_ORIGEM_CONTA = 23;
        public const int TABELA_FORMA_COBRANCA = 30;
        public const int TABELA_OPERACOES_POS = 44;
        public const int TABELA_OPERADORAS_TELEFONIA = 45;
        public const int TABELA_TIPO_MENSALIDADE = 50;
        public const int TABELA_NIVEL_CONFIANCA = 54;
        public const int TABELA_BANDEIRAS = 63;
        public const int TABELA_TIPO_LOGON = 65;
        public const int TABELA_ESTADO_CIVIL = 76;
        public const int TABELA_NACIONALIDADES = 80;
        public const int TABELA_LOGRADOUROS = 81;
        public const int TABELA_REGRAS_EXPANSAO_TARIFAS = 94;
        public const int TABELA_RESPONSAVEL_TARIFA = 105;
        public const int TABELA_NIVEL_APL_EXPANSAO_TARIFAS = 921;
        public const int TABELA_INTERNAS = 299;
        public const int TABELA_NIVEL_CONFIGURACAO_USUARIO = 947;

        #endregion ** TABELAS INTERNAS **

        /// <summary>
        /// Origem de Ativação por Processo Manual
        /// </summary>
        public const byte ORIGEM_ATIVACAO_MANUAL = 1;
        /// <summary>
        /// Origem de Ativação por Instrução do Administrador
        /// </summary>
        public const byte ORIGEM_ATIVACAO_ADMINISTRADOR = 2;
        /// <summary>
        /// Origem de Ativação por Página Web
        /// </summary>
        public const byte ORIGEM_ATIVACAO_PAGE = 3;
        /// <summary>
        /// Origem de Ativação por API
        /// </summary>
        public const byte ORIGEM_ATIVACAO_API = 4;


        #region Status de Registro

        /// <summary>
        /// Status de Registro Inativo
        /// </summary>
        public const int STATUS_REGISTRO_INATIVO = 0;
        /// <summary>
        /// Status de Registro Ativo
        /// </summary>
        public const int STATUS_REGISTRO_ATIVO = 1;
        /// <summary>
        /// Status de Registro Proprietário (Bloqueado)
        /// </summary>
        public const int STATUS_REGISTRO_PROPRIETARIO = 2;

        /// <summary>
        /// Status de Registro Protegido
        /// </summary>
        public const int STATUS_REGISTRO_SISTEMA = 3;

        /// <summary>
        /// Status de Registro em Compensação
        /// </summary>
        public const int STATUS_REGISTRO_COMPENSACAO = 6;

        /// <summary>
        /// Status de Registro Baixado
        /// </summary>
        public const int STATUS_REGISTRO_BAIXADO = 9;

        /// <summary>
        /// Status de Registro Cancelado
        /// </summary>
        public const int STATUS_REGISTRO_CANCELADO = 13;

        /// <summary>
        /// Status de Registro Estornado
        /// </summary>
        public const int STATUS_REGISTRO_ESTORNADO = 16;

        /// <summary>
        /// Status de Registro Estornado Parcial
        /// </summary>
        public const int STATUS_REGISTRO_ESTORNADO_PARCIAL = 17;

        /// <summary>
        /// Status de Registro Arquivado (não há mai necessidade de processamento)
        /// </summary>
        public const int STATUS_REGISTRO_ARQUIVAOO = 100;

        /// <summary>
        /// Status de Registro condicionado as operacoes de pre-autorização de transações
        /// </summary>
        public const int STATUS_REGISTRO_PREAUTORIZACAO = 121;
        /// <summary>
        /// Status de Registro condicionado as operacoes reversaõ de pre-autorização de transações
        /// </summary>
        public const int STATUS_REGISTRO_REVERSAO_AUTORIZACAO = 122;

        /// <summary>
        /// Status de Conta Aguardando aprovação 
        /// </summary>
        public const int STATUS_CONTA_AGUARDANDO_APROVACAOO = 253;
        #endregion Status de Registro





        #region -- SELETORESPADRAO --
        public static List<GeneralTable> AddSeletorBase(List<GeneralTable> l, int pKEYCOD = -1, string pDSCTAB = "-- SELECIONE --")
        {
            List<GeneralTable> _ilist = new List<GeneralTable>();
            _ilist = l;
            GeneralTable _i = new GeneralTable();
            _i.KEYCOD = pKEYCOD;
            _i.DSCTAB = pDSCTAB;
            _ilist.Add(_i);
            _i = null;
            return _ilist.OrderBy(p => p.KEYCOD).ToList();
        }

        public static List<AttributeType> AddSeletorBase(List<AttributeType> l, short pKEYCOD = -1, string pDSCTAB = "-- SELECIONE --")
        {
            List<AttributeType> _ilist = new List<AttributeType>();
            _ilist = l;
            AttributeType _i = new AttributeType();
            _i.CODATR = pKEYCOD;
            _i.DSCATR = pDSCTAB;
            _ilist.Add(_i);
            _i = null;
            return _ilist.OrderBy(p => p.CODATR).ToList();
        }
        public static List<TransactionStatus> AddSeletorBase(List<TransactionStatus> l, short pKEYCOD = -1, string pDSCTAB = "-- SELECIONE --")
        {
            List<TransactionStatus> _ilist = new List<TransactionStatus>();
            _ilist = l;
            TransactionStatus _i = new TransactionStatus();
            _i.CODSTA = pKEYCOD;
            _i.DSCSTA = pDSCTAB;
            _ilist.Add(_i);
            _i = null;
            return _ilist.OrderBy(p => p.CODSTA).ToList();
        }

        public static List<MyUsers> AddSeletorBase(List<MyUsers> l, short pKEYCOD = -1, string pDSCTAB = "-- SELECIONE --")
        {
            List<MyUsers> _ilist = new List<MyUsers>();
            _ilist = l;
            MyUsers _i = new MyUsers();
            _i.CODUSU= pKEYCOD;
            _i.NOMUSU = pDSCTAB;
            _ilist.Add(_i);
            _i = null;
            return _ilist.OrderBy(p => p.CODUSU).ToList();
        }


        #endregion
    }
}

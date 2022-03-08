using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire.Domain
{
    public class Constants
    {

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

        public const int ATRIBUTO_EMPRESA = 1;
        public const int ATRIBUTO_USUARIOS = 2;
        public const int ATRIBUTO_GESTORES = 3;
        public const int ATRIBUTO_CLIENTES = 4;
        public const int ATRIBUTO_FORNECEDORES = 5;
        public const int ATRIBUTO_SACADOS = 6;

        #endregion

        #region ** MODULOS **
        public const int MODULO_CADASTRO_GERAL = 1;
        public const int MODULO_CONTAS= 7;
        public const int MODULO_CARTOES = 5;
        public const int MODULO_MENSALIDADES = 10;
        public const int MODULO_POS = 9;
        #endregion ** MODULOS **
        #region ** TABELAS INTERNAS **

        public const int TABELA_PAISES = 1;
        public const int TABELA_UNIDADES_FEDERACAO = 2;
        public const int TABELA_PROXIMOS_NUMEROS = 6;
        public const int TABELA_STATUS_REGISTRO = 7;
        public const int TABELA_TIPO_PESSOA = 8;
        public const int TABELA_BANCOS = 12;
        public const int TABELA_GENERO = 13;
        public const int TABELA_MODULOS = 14;
        public const int TABELA_TIPO_BENEFICIARIO = 20;
        public const int TABELA_ORIGEM_CONTA = 23;
        public const int TABELA_OPERADORAS_TELEFONIA = 45;
        public const int TABELA_NIVEL_CONFIANCA = 54;
        public const int TABELA_TIPO_LOGON = 65;
        public const int TABELA_ESTADO_CIVIL= 76;
        public const int TABELA_NACIONALIDADES = 80;
        public const int TABELA_LOGRADOUROS = 81;
        public const int TABELA_INTERNAS = 299;

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



    }
}

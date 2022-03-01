using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBTABGER Alias Tabela Geral do Sistema
///</summary>

    public class GeneralTable
    {
                #region "Variáveis Privadas"
        private string _DSCTAB="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// ID de Registro da Tabela Geral
        /// </summary>
        public int KEYTAB{ get;set;} = 0;

        /// <summary>
        /// Código da Tabela
        /// </summary>
        /// <remarks>
/// <para>1 - TABELA DE PAISES</para>
/// <para>2 - UNIDADES DA FEDERACAO</para>
/// <para>3 - PLANOS</para>
/// <para>4 - STATUS</para>
/// <para>5 - NIVEIS DE APLICACAO DA REGRA DE COMISSAO</para>
/// <para>6 - PROXIMOS NUMEROS</para>
/// <para>7 - STATUS DE REGISTRO</para>
/// <para>8 - TIPO DE USUARIO</para>
/// <para>9 - TIPO DE PESSOA</para>
/// <para>10 - SINAL DE OPERACOES</para>
/// <para>11 - GESTORES DE PRODUTO</para>
/// <para>12 - BANCOS</para>
/// <para>13 - GENERO</para>
/// <para>14 - MODULOS DO APLICATIVO</para>
/// <para>15 - PERFIL DE DISTRIBUICAO</para>
/// <para>16 - TIPOS DE BOLETO</para>
/// <para>17 - BOLETOS COM REGISTRO DE CONTA CORRENTE</para>
/// <para>18 - TIPOS DE TARIFA</para>
/// <para>19 - BOLETO INTERNO</para>
/// <para>20 - TIPO DE BENEFICIARIO</para>
/// <para>21 - EXCECOES DE PRODUTOS NA PESQUISA DE GESTOR/LOTE</para>
/// <para>22 - TIPOS DE CONTA</para>
/// <para>23 - ORIGEM DA CONTA</para>
/// <para>24 - TARIFAS E LIMITES</para>
/// <para>25 - TIPOS DE BOLETO COMISSIONAMENTO</para>
/// <para>26 - ACOES DO ROTEIRO</para>
/// <para>29 - TIPO DE VALOR PARA PROCESSAMENTO</para>
/// <para>30 - FORMA DE COBRANCA DE TARIFA</para>
/// <para>31 - TIPO DE REGISTRO DE LOTE</para>
/// <para>32 - OPERACOES DE CREDITO (CARTAO)</para>
/// <para>33 - VIA DE PAGAMENTO (BOLETO)</para>
/// <para>34 - PLANO DE COMISSIONAMENTO</para>
/// <para>35 - INSTRUCOES DE BAIXA</para>
/// <para>36 - TIPOS DE LANCAMENTOS</para>
/// <para>37 - ATRIBUTO DO CADASTRO GERAL</para>
/// <para>38 - LIGACAO DE USUARIO</para>
/// <para>39 - INDICADOR DE LANCAMENTO</para>
/// <para>40 - CODIGO GERENCIAL DE MOVIMENTO</para>
/// <para>41 - CODIGO DE SUMARIZACAO DE MOVIMENTO</para>
/// <para>42 - CODIGOS DE RECORRENCIA</para>
/// <para>43 - NIVEL DE ATRIBUICAO E CONFIGURACAO</para>
/// <para>44 - TIPOS DE OPERACOES POS</para>
/// <para>45 - OPERADORAS</para>
/// <para>46 - TIPO DE RECORRENCIA</para>
/// <para>47 - VALIDACAO DE BOLETO/FATURA</para>
/// <para>48 - TIPO DE LINHA CARTAO</para>
/// <para>49 - SITUACAO DE REGISTRO DE PAGAMENTO</para>
/// <para>50 - TIPOS DE MENSALIDADE</para>
/// <para>51 - SIUACAO DE PAGAMENTO VENDAS</para>
/// <para>52 - CHARGER BACK</para>
/// <para>53 - TIPO DE COMISSAO</para>
/// <para>54 - NIVEL DE CONFIANCA DO USUARIO</para>
/// <para>55 - SUMARIZACAO DE MOVIMENTO</para>
/// <para>56 - ORIGEM DE ATIVACAO DO CARTAO</para>
/// <para>57 - LINHA DE REGISTRO DE LANCAMENTO</para>
/// <para>58 - REGRAS DE ACESSO</para>
/// <para>59 - TIPOS DE REGISTRO DE MOVIMENTO</para>
/// <para>60 - CODIGOS DE ACESSO</para>
/// <para>61 - CONFIGURACAO DE BOLETOS DESBLOQUEIO</para>
/// <para>62 - CONFIGURACAO DE MOVIMENTOS DE BOLETOS DESBLOQUEIO</para>
/// <para>63 - BANDEIRAS DE CARTAO</para>
/// <para>64 - DESCRICAO DE APROVACOES DE REGISTROS POS</para>
/// <para>65 - TIPO DE ACESSO CONTROLE</para>
/// <para>65 - TIPO DE ACESSO CONTROLE</para>
/// <para>66 - ADQUIRENTES</para>
/// <para>68 - TIPO DE CARTAO</para>
/// <para>69 - RAMOS DE ATIVIDADE</para>
/// <para>70 - STATUS DE RECARGA DE LOTE</para>
/// <para>71 - OPERADORAS DE CARTAO</para>
/// <para>73 - IDENTIFICACAO DE MENUS</para>
/// <para>74 - BANCOS ASSOCIADOS AOS MODULO BOLETO RECARGA TED</para>
/// <para>75 - CONFIGURACAO TEMPO DE APLICACAO PROXIMO VALOR DE RECARGA</para>
/// <para>76 - ESTADO CIVIL</para>
/// <para>77 - TIPO DE PORTADOR</para>
/// <para>78 - RELACAO DE PARA TIPO DE LANCAMENTO</para>
/// <para>79 - BANCOS CNAB</para>
/// <para>80 - NACIONALIDADES</para>
/// <para>81 - LOGRADOUROS</para>
/// <para>82 - TIPO DE LOGIN</para>
/// <para>94 - APLICACAO EXPANSAO TARIFA</para>
/// <para>95 - LINK DE PRODUTO</para>
/// <para>96 - RMC</para>
/// <para>100 - CONFIGURACAO DE CADASTRO DE INDICACAO</para>
/// <para>101 - CONFIGURACAO DO CADASTRO BOX1</para>
/// <para>102 - CONFIGURACAO DE TRANSFERENCIAS</para>
/// <para>103 - CONFIGURACAO DE TEMPLATES EMAIL</para>
/// <para>104 - CONFIGURACAO PRODUTOS WEB VENDA</para>
/// <para>105 - RESPONSABILIDADE TARIFA</para>
/// <para>201 - CODIGO DE ATIVACAO DO CARTAO</para>
/// <para>201 - CODIGO DE ATIVACAO DO CARTAO</para>
/// <para>300 - SEGMENTOS</para>
/// <para>301 - ORGAOS DE PAGAMENTO</para>
/// <para>301 - SEGMENTOS DE ARRECADACAO</para>
/// <para>301 - ORGAOS DE PAGAMENTO</para>
/// <para>302 - CONFIGURACAO DE TRANSACAO ECOMMERCE</para>
/// <para>304 - FINALIDADES TED MESMA TITULARIDADE</para>
/// <para>305 - FINALIDADES TED DIFERENTE TITULARIDADE</para>
/// <para>400 - AUDITORIA</para>
/// <para>401 - PERMISSOES DE EXECUCAO</para>
/// <para>700 - CONFIGURACAO DE CAMPANHAS</para>
/// <para>800 - LANCAMENTOS DE MENSALIDADES CCV</para>
/// <para>801 - LANCAMENTOS DE MENSALIDADES CARTAO</para>
/// <para>802 - LANCAMENTOS DE RECARGA</para>
/// <para>803 - LANCAMENTOS DE TRANSFERENCIA P/OUTROS BANCOS</para>
/// <para>804 - LANCAMENTOS DE COMISSAO DE VENDA</para>
/// <para>805 - LANCAMENTOS DE TRANSF CARTAO</para>
/// <para>806 - LANCAMENTOS DE TRANSF ENTRE CC</para>
/// <para>807 - LANCAMENTOS DE ATIVACAO CARTAO</para>
/// <para>808 - CONFIGURACAO DE TARIFA DE POSTAGEM</para>
/// <para>825 - REGISTRO DE VENDAS POR MAQUINA</para>
/// <para>826 - STATUS DE REGISTRO DE VENDAS</para>
/// <para>828 - STATUS DE REGISTRO DE VENDAS</para>
/// <para>830 - REGISTRO DE BAIXA DE CREDITO</para>
/// <para>831 - REGISTRO DE VENDAS POS</para>
/// <para>850 - CODIGOS DE MOVIMENTACAO TED</para>
/// <para>851 - CODIGOS DE MOVIMENTACAO TRANSF. P/ CARTAO</para>
/// <para>851 - CODIGOS DE MOVIMENTACAO DESCARGA</para>
/// <para>852 - CODIGOS MOVIMENTO DESCARGA CARTAO</para>
/// <para>852 - CODIGOS DE MOVIMENTACAO PAGAMENTOS</para>
/// <para>853 - CODIGOS DE STATUS PARA APROVACAO TRANF OUTROS BANCOS</para>
/// <para>858 - CONFIGURACAO EXTRATO TRANSFERENCIAS PARA CARTAO</para>
/// <para>859 - CONFIGURACAO DESCARGA DE CARTAO</para>
/// <para>860 - STATUS DE EXCECAO LISTAGEM CARTOES</para>
/// <para>861 - CONFIGURACAO CONTA CORRENTE EXTRATO</para>
/// <para>863 - CONFIGURACAO TRANSFERENCIA PARA OUTROS BANCOS</para>
/// <para>875 - CODIGOS EXTERNOS DE PRODUTO</para>
/// <para>876 - RELACIONAMENTO ENTRE CODIGOS MCC</para>
/// <para>900 - BOLETO INTERNO DE COBRANCA</para>
/// <para>902 - USUARIOS COM ACESSO VIA TOKEN</para>
/// <para>903 - TEMPO DE PROCESSAMENTO DE TOKEN POR PRODUTO</para>
/// <para>904 - DEFINICAO DE USO DO PROCESSAMENTO DO TOKEN</para>
/// <para>905 - INSTRUCOES DE REGISTRO DE BAIXA DE BOLETO</para>
/// <para>906 - CONFIGURACAO BOLETOS COBRANCA USUARIO</para>
/// <para>907 - TIPO DE REGISTRO DE INSTRUCAO DE BAIXA DE BOLETO</para>
/// <para>910 - TEMPO LIMITE DE PROCESSAMENTO DE CODIGOS DE ACESSO</para>
/// <para>911 - TIPOS DE VALOR PARA BAIXA DE BOLETO</para>
/// <para>912 - APLICACOES DE TARIFA DE DEPOSITANTE</para>
/// <para>913 - APLICAÇÕES DE TARIFA ADMINISTRATIVA</para>
/// <para>913 - APLICAÇÕES DE TARIFA ADMINISTRATIVA BOLETO</para>
/// <para>914 - APLICAÇÕES DE TARIFA ADMINISTRATIVA TRANSACOES</para>
/// <para>920 - TIPOS DE REGISTRO DE BAIXA DE BOLETO</para>
/// <para>948 - RMC BASE DE OPERACAO</para>
/// <para>949 - CONFIGURACAO INTERNA DE BOLETOS</para>
/// <para>950 - ATRIBUTO CADASTRO GERAL - TARIFACAO</para>
/// <para></para>
/// </remarks>
        public int NUMTAB{ get;set;} = 0;

        /// <summary>
        /// Chave Numérica
        /// </summary>
        public int KEYCOD{ get;set;} = 0;

        /// <summary>
        /// Chave Texto
        /// </summary>
        public string KEYTXT{ get;set;} = "";

        /// <summary>
        /// Valor de Informação ou Controle
        /// </summary>
        public int NUMREF{ get;set;} = 0;

        /// <summary>
        /// Descrição do Item da Tabela Geral
        /// </summary>
        public string DSCTAB
        { 
            get { return _DSCTAB;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCTAB= value .ToUpper().NoAccents();
else
_DSCTAB= "";
}

        }

        /// <summary>
        /// Indica se o registro deve Valor do Pagamento
        /// </summary>
        public byte USRDSP{ get;set;} = 1;

        /// <summary>
        /// Indicador de Precedência
        /// </summary>
        public byte IDEPRE{ get;set;} = 0;

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        public byte STAREC{ get;set;} = 1;

        /// <summary>
        /// Data de Inclusão ou cadastramento
        /// </summary>
        public DateTime DATCAD{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data da Ultima Atualização
        /// </summary>
        public DateTime DATUPD{ get;set;} = DateTime.Now;

        /// <summary>
        /// Usuário de Atualização
        /// </summary>
        public int UPDUSU{ get;set;} = 0;

    }
}

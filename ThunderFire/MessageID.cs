//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ThunderFire
//{
//    /// <summary>
//    /// Tratamento de Mensagens ISO-8583
//    /// </summary>
//    public class MessageID
//    {
//        private string _numtr2 = "";
//        private string _valcrt = "";

//        /// <summary>
//        /// Código Fixo
//        /// </summary>
//        public string CODFIX { get; set; } = "";
//        /// <summary>
//        /// Mensagem
//        /// </summary>
//        public string STRMSG { get; set; } = "";

//        /// <summary>
//        /// Identificador de Tipo de Mensagem
//        /// </summary>
//        [ISO(2, "Primary account number (PAN)", 19)]
//        public string NUMPAN { get; set; } = "";


//        /// <summary>
//        /// Identificador de Tipo de Mensagem
//        /// </summary>
//        [ISO(4, "Amount, transaction")]
//        public string CODMIT { get; set; } = "0000";


//        /// <summary>
//        /// Código de Processamento
//        /// </summary>
//        [ISO(3, "Processing Code", 6)]
//        public string CODPCT { get; set; } = "";

//        /// <summary>
//        /// Valor do Movimento
//        /// </summary>
//        [ISO(4, "Amount, transaction")]
//        public double VLRMOV { get; set; } = 0;

//        /// <summary>
//        /// Data do Movimento
//        /// </summary>
//        [ISO(7, "Data do Movimento")]
//        public DateTime DATTRA { get; set; } = new DateTime(1900, 01, 01);

//        /// <summary>
//        /// Data do Movimento
//        /// </summary>
//        [ISO(11, "Número do NSU", 6)]
//        public string NUMNSU { get; set; } = "";

//        /// <summary>
//        /// Hora de Processamento
//        /// </summary>
//        [ISO(12, "Local transaction time (hhmmss)", 6)]
//        public string HORPRO { get; set; } = "";

//        /// <summary>
//        /// Data MMDD
//        /// </summary>
//        [ISO(13, "Local transaction date (MMDD)", 4)]
//        public string DATFMD { get; set; } = "";

//        /// <summary>
//        /// Data de Validade do Cartão)
//        /// </summary>
//        [ISO(14, "Data de Validade do Cartão", 4)]
//        public string VALCRT
//        {
//            get { return _valcrt; }
//            set
//            {
//                string _VALTMP = value;
//                if (_VALTMP.Length == 4)
//                    _valcrt = _VALTMP;
//                else if (_VALTMP.Length == 5)
//                {
//                    if (_VALTMP.Length == 5)
//                    {
//                        string _VALANO = _VALTMP.Substring(3, 2);
//                        string _VALMES = _VALTMP.Substring(0, 2);
//                        _valcrt = _VALANO + _VALMES;
//                    }
//                }
//                else
//                    _valcrt = "0000";
//            }
//        }

//        /// <summary>
//        /// Merchant Type
//        /// </summary>
//        [ISO(18, "Merchant Type", 4)]
//        public string VALMCC { get; set; } = "";

//        /// <summary>
//        /// Código do País
//        /// </summary>
//        [ISO(19, "Código do País", 3)]
//        public string ADQPAI { get; set; } = "";



//        /// <summary>
//        /// Modo de entrada no ponto de serviço
//        /// </summary>
//        [ISO(22, "Modo de entrada no ponto de serviço", 3)]
//        public string MODENT { get; set; } = "";

//        /// <summary>
//        /// Código de identificação da instituição
//        /// </summary>
//        [ISO(32, "Código de identificação da instituição")]
//        public string CODINS { get; set; } = "";

//        /// <summary>
//        /// Número da Trilha 2
//        /// </summary>
//        [ISO(35, "Número da Trilha 2")]
//        public string NUMTR2
//        {
//            get { return _numtr2; }
//            set
//            {
//                _numtr2 = value;
//                if (!string.IsNullOrEmpty(_numtr2))
//                {
//                    this.NUMCRT = _numtr2.Substring(0, 16);
//                }
//            }
//        }
//        /// <summary>
//        /// Número do Cartão
//        /// </summary>
//        public string NUMCRT { get; set; } = "";

//        /// <summary>
//        /// Número de referência de recuperação
//        /// </summary>
//        [ISO(37, "Número de referência de recuperação")]
//        public string NUMREC { get; set; } = "";

//        /// <summary>
//        /// Resposta de Identificação de autorização
//        /// </summary>
//        [ISO(38, "Resposta de Identificação de autorização")]
//        public string RSPNSU { get; set; } = "";

//        /// <summary>
//        /// Código de Resposta
//        /// </summary>
//        [ISO(39, "Código de Resposta", 2)]
//        public string CODRSP { get; set; } = "";

//        /// <summary>
//        /// Número do Terminal
//        /// </summary>
//        [ISO(41, "Número do Terminal", 8)]
//        public string NUMTER { get; set; } = "";

//        /// <summary>
//        /// Código CMF (Estabelecimento)
//        /// </summary>
//        [ISO(42, "Código de identificação do aceitante do cartão", 15)]
//        public string CODCMF { get; set; } = "";

//        /// <summary>
//        /// Dados do Estabelecimento
//        /// </summary>
//        [ISO(43, "Código de identificação do aceitante do cartão", 43)]
//        public string DATEST { get; set; } = "";

//        /// <summary>
//        /// Trilha 1 do Cartão
//        /// </summary>
//        [ISO(45, "Trilha 1 do Cartão", 76)]
//        public string TRLCRT { get; set; } = "";

//        /// <summary>
//        /// Informações Adicionais
//        /// </summary>
//        [ISO(48, "Informacoes Adicionais", 999)]
//        public string  INFADC { get; set; } = "";

//        /// <summary>
//        /// Código da Moeda)
//        /// </summary>
//        [ISO(49, "Código da Moeda", 3)]
//        public string CODMOE { get; set; } = "";

//        /// <summary>
//        /// Senha Criptografada
//        /// </summary>
//        [ISO(52, "Senha Criptografada", 3)]
//        public string PSWREC { get; set; } = "";

//        /// <summary>
//        /// Valores Adicionais
//        /// </summary>
//        [ISO(54, "Valores Adicionais", 120)]
//        public string VALADC { get; set; } = "";

//        /// <summary>
//        /// Dados EMV
//        /// </summary>
//        [ISO(55, "Dados EMV", 999)]
//        public string DATEMV { get; set; } = "";

//        /// <summary>
//        /// Dados Adicionais de Terminal
//        /// </summary>
//        [ISO(60, "Dados Adicionais de Terminal", 5)]
//        public string DATTRM { get; set; } = "";


//        /// <summary>
//        /// Elementos de dados originais
//        /// </summary>
//        [ISO(90, "Elementos de dados originais", 26)]
//        public string VALORG { get; set; } = "";

//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire.Domain
{
    
    /// <summary>
    /// Linha Digitável
    /// </summary>
    public class DigitableLine:ErrorBase
    {
        /// <summary>
        /// Captura de Erros
        /// </summary>
        public TrapError TrappedError = new TrapError();

        private string Trimmer(string _text)
        {
            _text = _text.Replace(".", "");
            _text = _text.Replace(" ", "");
            _text = _text.Replace("-", "");
            return _text.Trim(); ;
        }
        /// <summary>
        /// Valida uma Linha Digitável
        /// </summary>
        /// <param name="pNUMTRL">Trailler</param>
        /// <param name="pDATVCT">Data de Vencimento</param>
        /// <param name="pVLRDOC">Valor do documento</param>
        /// <returns>LinhaDigitavel</returns>
        public LinhaDigitavel Validate(string pNUMTRL, string pDATVCT, string pVLRDOC)
        {
            pNUMTRL = Trimmer(pNUMTRL);
            LinhaDigitavel item = new LinhaDigitavel();
            string linha = "";
            bool go = true;
            int DigitoGeral = -1;
            int digito1 = 0;
            int digito2 = 0;
            int digito3 = 0;
            int digito4 = 0;
            byte direction = 0;
            byte EXEM10 = 1;

            int CALCULED_DV = 0;
            bool BASE11 = true;

            try
            {
                if (pNUMTRL != "")
                {
                    direction = byte.Parse(pNUMTRL.Substring(0, 1));

                    if (pNUMTRL.Length == 48 && direction == 8)
                    {
                        item.Segmento = byte.Parse(pNUMTRL.Substring(1, 1));
                        item.TipoValor = byte.Parse(pNUMTRL.Substring(2, 1));

                        string linha1 = pNUMTRL.Substring(0, 11);
                        string linha2 = pNUMTRL.Substring(12, 11);
                        string linha3 = pNUMTRL.Substring(24, 11);
                        string linha4 = pNUMTRL.Substring(36, 11);

                        int _CODORG = int.Parse(linha2.Substring(4, 4));
                        if (_CODORG == 89)
                            EXEM10 = 0;
                        if (_CODORG == 5701)
                            EXEM10 = 0;
                        if (_CODORG == 328)
                            EXEM10 = 0;
                        if (_CODORG == 379)
                            EXEM10 = 0;

                        digito1 = int.Parse(pNUMTRL.Substring(11, 1));
                        digito2 = int.Parse(pNUMTRL.Substring(23, 1));
                        digito3 = int.Parse(pNUMTRL.Substring(35, 1));
                        digito4 = int.Parse(pNUMTRL.Substring(47, 1));
                        DigitoGeral = int.Parse(pNUMTRL.Substring(3, 1));

                        EXEM10 = 1;

                        CALCULED_DV = EXEVAL(EXEM10, linha1);

                        BASE11 = false;
                        if (CALCULED_DV != digito1)
                        {
                            SetError("PAYMENTINVALIDDAC", "1");
                            go = false;
                            BASE11 = true;
                        }
                        /*
                         * DIGITO2
                         */
                        if (go)
                        {
                            CALCULED_DV = EXEVAL(EXEM10, linha2);
                            if (CALCULED_DV != digito2)
                            {
                                SetError("PAYMENTINVALIDDAC","2");
                                go = false;
                                BASE11 = true;
                            }
                        }

                        if (go)
                        {
                            CALCULED_DV = EXEVAL(EXEM10, linha3);

                            if (CALCULED_DV != digito3)
                            {
                                if (MOD10(linha3) != digito3)
                                {
                                    SetError("PAYMENTINVALIDDAC","3");
                                    go = false;
                                    BASE11 = true;
                                }
                            }
                        }
                        if (go)
                        {
                            CALCULED_DV = EXEVAL(EXEM10, linha4);
                            if (CALCULED_DV != digito4)
                            {

                                SetError("PAYMENTINVALIDDAC","4");
                                go = false;
                                BASE11 = true;
                            }
                        }


                        if (BASE11)
                        {
                            go = true;
                            EXEM10 = 0;
                            CALCULED_DV = EXEVAL(EXEM10, linha1);
                            if (CALCULED_DV != digito1)
                            {
                                SetError("PAYMENTINVALIDDAC","1");
                                go = false;
                            }
                            /*
                             * DIGITO2
                             */
                            if (go)
                            {
                                CALCULED_DV = EXEVAL(EXEM10, linha2);
                                if (CALCULED_DV != digito2)
                                {

                                    SetError("PAYMENTINVALIDDAC","2");
                                    go = false;
                                }
                            }

                            if (go)
                            {
                                CALCULED_DV = EXEVAL(EXEM10, linha3);

                                if (CALCULED_DV != digito3)
                                {
                                    if (MOD10(linha3) != digito3)
                                    {
                                        SetError("PAYMENTINVALIDDAC","3");
                                        go = false;
                                    }
                                }
                            }
                            if (go)
                            {
                                CALCULED_DV = EXEVAL(EXEM10, linha4);
                                if (CALCULED_DV != digito4)
                                {
                                    SetError("PAYMENTINVALIDDAC","4");
                                    go = false;
                                }
                            }

                        }

                        if (go)
                        {

                            linha = linha1 + linha2 + linha3 + linha4;

                            if (linha.Length == 44)
                            {
                                CALCULED_DV = VALDIGGER(EXEM10, linha);
                                if (CALCULED_DV != DigitoGeral)
                                {
                                    SetError("PAYMENTINVALIDDAC","GERAL");
                                    go = false;
                                }

                                if (go)
                                {

                                    if (direction == 8)
                                    {
                                        item.Produto = 8;
                                        item.Digito = (byte)DigitoGeral;
                                        item.Valor = ToValue(linha.Substring(4, 11));
                                        item.Orgao = short.Parse(linha.Substring(15, 4));
                                        SetError("OK");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //AAABCCCCCXDDDDDDDDDDYEEEEEEEEEEZKUUUUVVVVVVVVVV
                        //BLOCO 1     BLOCO 2      BLOCO 3   4    BLOCO 5  
                        pNUMTRL = pNUMTRL.PadRight(47, '0');
                        int l = pNUMTRL.Length;
                        string linha1 = pNUMTRL.Substring(0, 9);
                        string linha2 = pNUMTRL.Substring(10, 10);
                        string linha3 = pNUMTRL.Substring(21, 10);
                        string linha4 = pNUMTRL.Substring(36, 11);
                        linha = pNUMTRL;
                        item.Banco = short.Parse(linha.Substring(0, 3));
                        item.Moeda = byte.Parse(linha.Substring(3, 1));
                        digito1 = int.Parse(linha.Substring(9, 1));
                        string livre1 = linha.Substring(4, 5);
                        string livre2 = linha.Substring(10, 10);
                        digito2 = int.Parse(linha.Substring(20, 1));
                        string livre3 = linha.Substring(21, 10);
                        digito3 = int.Parse(linha.Substring(31, 1));
                        digito4 = int.Parse(linha.Substring(32, 1));
                        int fator = int.Parse(linha.Substring(33, 4));
                        if (fator > 0)
                            item.DataVencimento = DataVencimento(fator);

                        string value = linha.Substring(37, 10);
                        item.Valor = ToValue(value);

                        if (MOD10(linha1) != digito1)
                        {
                            SetError("PAYMENTINVALIDDAC","1");
                            go = false;
                        }
                        if (MOD10(linha2) != digito2)
                        {
                            SetError("PAYMENTINVALIDDAC","1");
                            go = false;
                        }
                        if (MOD10(linha3) != digito3)
                        {
                            SetError("PAYMENTINVALIDDAC","3");
                            go = false;
                        }
                    }

                }
                else
                {
                    SetError("PAYMENTEMPTYDIGITABLELINE","2");
                    go = false;
                }
            }
            catch (Exception Error)
            {
                string errorcode = ErrorManager.GetErrorCode(Error);
                SetError(errorcode, ErrorManager.GetStringMsg(errorcode), Error.Message, Error);
                go = false;
            }

            item.OK = go;

            if (item.OK)
            {
                if (item.DataVencimento.HasValue)
                {
                    if (pDATVCT != "")
                    {
                        if (item.DataVencimento.Value.ToString("dd/MM/yyyy") != pDATVCT)
                        {
                            SetError("PAYMENTINVALIDDUEDATE", ErrorManager.GetStringMsg("PAYMENTINVALIDDUEDATE"), "", null);
                            item.OK = false;
                        }
                    }
                }
            }
            if (item.OK)
            {
                if (item.Valor > 0)
                {
                    if (pVLRDOC.Trim() != "")
                    {
                        string _VLRMOV = pVLRDOC.ToString().Replace(",", "").Replace(".", "").Trim();
                        if (((Converter.ToDouble(_VLRMOV) / 100) - item.Valor) != 0)
                        {
                            SetError("PAYMENTDIFVALUE", ErrorManager.GetStringMsg("PAYMENTDIFVALUE"), "", null);
                            item.OK = false;
                        }
                    }
                }
            }
            return item;
        }

        internal DateTime DataVencimento(int fator)
        {
            DateTime result = new DateTime(1997, 10, 07);
            result = result.AddDays(fator);
            return result;
        }
        internal double ToValue(string _value)
        {
            double r = 0;
            double val = double.Parse(_value.Substring(0, _value.Length - 2));
            double dec = double.Parse(_value.Substring(_value.Length - 2, 2));
            r = val + (dec / 100);
            return r;
        }

        internal int VALDIGGER(byte EXEM10, string linha)
        {
            int RETURN_VALUE = 0;
            int intSoma = 0;
            int somando = 0;
            int intResto = 0;
            string valid = linha.Substring(0, 3) + linha.Substring(4);
            if (EXEM10 == 0)
            {

                int[] intPesos = { 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                for (int i = 0; i < valid.Length; i++)
                {
                    intSoma += Convert.ToInt32(valid[i].ToString()) * intPesos[i];
                }
                intResto = intSoma % 11;
                if (intResto == 1)
                    RETURN_VALUE = 0;
                else
                {
                    if (intResto > 0)
                        RETURN_VALUE = 11 - intResto;
                    else
                        RETURN_VALUE = 0;
                }


            }
            if (EXEM10 == 1)
            {
                int[] intPesos1 = { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };

                for (int i = 0; i < valid.Length; i++)
                {
                    somando = Convert.ToInt32(valid[i].ToString()) * intPesos1[i];
                    if (somando != 0)
                    {
                        if ((somando % 9) == 0)
                            intSoma += 9;
                        else
                            intSoma += (somando % 9);
                    }
                }
                intResto = intSoma % 10;
                RETURN_VALUE = 10 - intResto;
                if (RETURN_VALUE >= 10)
                    RETURN_VALUE = 0;
            }
            return RETURN_VALUE;
        }

        internal int EXEVAL(int EXEM10, string numero)
        {
            if (EXEM10 == 1)
                return MOD10(numero);
            else
                return MOD11(numero);
        }
        internal int MOD10(string numero)
        {
            int[] peso = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int soma = 0;
            int somando = 0;
            if (numero.Length <= 16)
            {
                soma = 0;
                int j = peso.Length - numero.Length;

                for (int i = 0; i < numero.Length; i++)
                {
                    somando = Convert.ToInt32(numero[i].ToString()) * peso[j];
                    j++;
                    if (somando != 0)
                    {
                        if ((somando % 9) == 0)
                            soma += 9;
                        else
                            soma += (somando % 9);
                    }
                }

                soma = soma % 10;
                soma = 10 - soma;
                if (soma == 10)
                    soma = 0;
            }
            return soma;
        }


        /// <summary>
        ///  Calculo de digito Modulo 11
        /// </summary>
        /// <param name="numero">Informar o numero para calculo digito</param>
        /// <returns>Retorna o digito</returns>
        public static int MOD11(string numero)
        {
            int[] intPesos = { 9, 8, 7, 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int digito = 0;
            if (numero.Length <= 16)
            {
                int intSoma = 0;
                int j = intPesos.Length - numero.Length;
                for (int i = 0; i < numero.Length; i++)
                {
                    intSoma += Convert.ToInt32(numero[i].ToString()) * intPesos[j];
                    j++;
                }
                digito = intSoma % 11;
                digito = 11 - digito;
                if (digito >= 10)
                    digito = 0;

            }
            return digito;
        }
    }



    /// <summary>
    /// Estrutura da Linha Digitavel
    /// </summary>
    public class LinhaDigitavel
    {
        /// <summary>
        /// Código do Banco
        /// </summary>
        public short Banco { get; set; } = 0;
        /// <summary>
        /// Moeda
        /// </summary>
        public byte Moeda { get; set; } = 1;
        /// <summary>
        /// Produto
        /// </summary>
        public byte Produto { get; set; } = 0;
        /// <summary>
        /// Segmento
        /// </summary>
        public byte Segmento { get; set; }
        /// <summary>
        /// Tipo de Valor
        /// </summary>
        public byte TipoValor { get; set; }
        /// <summary>
        /// Digito
        /// </summary>
        public byte Digito { get; set; }
        /// <summary>
        /// Valor
        /// </summary>
        public double Valor { get; set; }
        /// <summary>
        /// Orgão
        /// </summary>
        public short Orgao { get; set; }
        /// <summary>
        /// Data de Vencimento
        /// </summary>
        public DateTime? DataVencimento { get; set; }
        /// <summary>
        /// status de retorno
        /// </summary>
        public bool OK { get; set; }
        /// <summary>
        /// Instância do Objeto
        /// </summary>
        public LinhaDigitavel()
        {
            Produto = 0;
            Segmento = 6;
            TipoValor = 0;
            Digito = 0;
            Valor = 0;
            Orgao = 0;
            DataVencimento = null;
            OK = false;
        }
    }
}

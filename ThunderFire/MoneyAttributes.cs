using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Classe de Controle de Atributos de Valor
    /// </summary>
    public class MoneyAttributes
    {
        /// <summary>
        /// Define o simbolo da moeda
        /// </summary>
        public string CurrenctSymbol { get; set; }
        /// <summary>
        /// Define a unidade monetária da moeda no singular
        /// </summary>
        public string SingleMonetaryUnit { get; set; }
        /// <summary>
        /// Define a unidade monetária da moeda no plural
        /// </summary>
        public string PluralMonetaryUnit { get; set; }
        /// <summary>
        /// Define se no extenso o simbolo da moeda deve ser prefixado o valor por extenso;
        /// </summary>
        public bool AddCurrencySymbol { get; set; }
        /// <summary>
        /// Mensagem de erro para valor não fornecido
        /// </summary>
        public string MessageValueNotSupplied { get; set; }
        /// <summary>
        /// Mensagem de Erro para valor não suportado;
        /// </summary>
        public string MessageValueNotSupported { get; set; }
        /// <summary>
        /// Meses
        /// </summary>
        public string[] Meses = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
        /// <summary>
        /// Dia da Semana (Day of Week)
        /// </summary>
        public string[] Dow = { "Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sábado" };
        /// <summary>
        /// Unidades
        /// </summary>
        public string[] Units = { "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
        /// <summary>
        /// Dezenas
        /// </summary>
        public string[] Tens = { "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove" };
        /// <summary>
        /// Centena
        /// </summary>
        public string Hundred;
        /// <summary>
        /// Centenas
        /// </summary>
        public string[] Hundreds = { "Cem", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos", "Setecentos", "Oitocentos", "Novecentos" };
        /// <summary>
        /// Milhares
        /// </summary>
        public string[] Miles = { "Mil", "Mil" };
        /// <summary>
        /// Unidade de Milhar
        /// </summary>
        public string Mile;
        /// <summary>
        /// Milhões
        /// </summary>
        public string[] Million = { "Milhão", "Milhões" };
        /// <summary>
        /// Bilhões
        /// </summary>
        public string[] Billion = { "Bilhão", "Bilhões" };
        /// <summary>
        /// Centenas
        /// </summary>
        public string[] Cents = { "centavo", "centavos" };
        /// <summary>
        /// Dezena maior que 10
        /// </summary>
        public string[] TensOver = { "Dez", "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };

        /// <summary>
        /// Retona a strin correspondente as unidades
        /// </summary>
        /// <param name="exp">indexador</param>
        /// <returns>string</returns>
        public string GetUnit(int exp)
        {
            return Units[exp].ToString();
        }
        /// <summary>
        /// Retona a string correspondente as centenas
        /// </summary>
        /// <returns>string</returns>

        public string GetHundred()
        {
            return Hundred;
        }
        /// <summary>
        /// Retona a string correspondente as centenas
        /// </summary>
        /// <param name="exp">indexador</param>
        /// <returns>string</returns>
        public string GetHundreds(int exp)
        {
            return Hundreds[exp].ToString();
        }
        /// <summary>
        /// Retona a string correspondente as dezenas
        /// </summary>
        /// <param name="exp">indexador</param>
        /// <returns>string</returns>
        public string GetTen(int exp)
        {
            return Tens[exp].ToString();
        }
        /// <summary>
        /// Retona a string correspondente as dezenas maiores que 19
        /// </summary>
        /// <param name="exp">indexador</param>
        /// <returns>string</returns>
        public string GetTenOver(int exp)
        {
            return TensOver[exp].ToString();
        }

        /// <summary>
        /// Retona a string correspondente a casa de bilhoes
        /// </summary>
        /// <param name="exp">indexador</param>
        /// <returns>string</returns>
        public string GetBillionText(int exp)
        {
            if (exp == 1)
                return Billion[0].ToString();
            else
                if (exp > 0)
                return Billion[1].ToString();
            return " ";
        }
        /// <summary>
        /// Retona a string correspondente a casa de milhoes
        /// </summary>
        /// <param name="exp">indexador</param>
        /// <returns>string</returns>
        public string GetMillionText(int exp)
        {
            if (exp == 1)
                return Million[0].ToString();
            else
                if (exp > 0)
                return Million[1].ToString();
            return " ";
        }
        /// <summary>
        /// Retona a string correspondente a casa de milhoes
        /// </summary>
        /// <param name="exp">indexador</param>
        /// <returns>string</returns>
        public string GetMiles(int exp)
        {
            if (exp == 1)
                return Miles[0].ToString();
            else
                if (exp > 0)
                return Miles[1].ToString();
            return " ";
        }
        /// <summary>
        /// Retorna a Unidade de Milhar
        /// </summary>
        /// <returns>string</returns>
        public string GetMile()
        {
            return Mile;
        }

        /// <summary>
        /// Retona a string correspondente a casa de centavos
        /// </summary>
        /// <param name="exp">indexador</param>
        /// <returns>string</returns>
        public string GetCents(int exp)
        {
            if (exp == 1)
                return Cents[0].ToString();
            else
                if (exp > 0)
                return Cents[1].ToString();
            return " ";
        }


        /// <summary>
        /// Construtor
        /// </summary>
        public MoneyAttributes()
        {
            GetDefaultParameters();
        }

        /// <summary>
        /// Le as Atuais definições de parametros de valor
        /// </summary>
        /// <remarks>
        /// As atuais definições são assumidas conforme a tabela abaixo, para se alterar estas definições basta inserir na section 'AppSettings' no arquivo de configuração do aplicativo de acordo com a definição de 'Keys' abaixo e a definição desejada
        /// </remarks>
        /// <list type="table">
        /// <listheader><term>Key</term><description>Definição</description></listheader>  
        /// <item><term>CurrentSymbol</term><description>R$</description></item>
        /// <item><term>SingleMonetaryUnit</term><description>Real</description></item>
        /// <item><term>PluralMonetaryUnit</term><description>Reais</description></item>
        /// <item><term>AddCurrencySimbol</term><description>false</description></item>
        /// <item><term>MessageValueNotSupplied</term><description>Valor não Informado</description></item>
        /// <item><term>MessageValueNotSupported</term><description>Valor não Suportado</description></item>
        /// <item><term>Mile</term><description>Mil</description></item>
        /// <item><term>Hundred</term><description>Cento</description></item>
        /// </list>

        public void GetDefaultParameters()
        {
            this.CurrenctSymbol = Configuration.GetAppValue("CurrenctSymbol", "R$ ");
            this.SingleMonetaryUnit = Configuration.GetAppValue("SingleMonetaryUnit", "Real");
            this.PluralMonetaryUnit = Configuration.GetAppValue("PluralMonetaryUnit", "Reais");
            this.AddCurrencySymbol = bool.Parse(Configuration.GetAppValue("AddCurrencySymbol", "false"));
            this.MessageValueNotSupplied = Configuration.GetAppValue("MessageValueNotSupplied", "Valor não Informado");
            this.MessageValueNotSupported = Configuration.GetAppValue("MessageValueNotSupported", "Valor não suportado");
            this.Mile = Configuration.GetAppValue("Mile", "Mil");
            this.Hundred = Configuration.GetAppValue("Hundred", "Cento");
        }
    }
}

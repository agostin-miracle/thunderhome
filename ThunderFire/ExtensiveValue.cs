using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Valor por Extenso
    /// </summary>
    public class ExtensiveValue
    {
        /// <summary>
        /// Determina o Valor por Extenso de um número
        /// </summary>
        /// <param name="pValor">Valor a ser avaliado</param>
        /// <returns>string</returns>
        public static string Get(decimal pValor)
        {
            MoneyAttributes objeto = new MoneyAttributes();
            string strValorExtenso = ""; //Variável que irá armazenar o valor por extenso do número informado
            decimal dblValorInteiro = 0;
            decimal dblCentavos = 0;
            int exp = 0;

            //Verificar se foi informado um dado indevido
            if (pValor == 0 || pValor <= 0)
            {
                strValorExtenso = objeto.MessageValueNotSupplied;
            }
            if (pValor > (decimal)9999999999.99)
            {
                strValorExtenso = objeto.MessageValueNotSupported;
            }
            else //Entrada padrão do método
            {
                //Gerar Extenso parte Inteira
                dblValorInteiro = (long)pValor;
                dblCentavos = (pValor - dblValorInteiro);
                if (dblValorInteiro > 0)
                {
                    string _value = "00000000000" + dblValorInteiro.ToString().Replace(".", "").Replace(",", "");
                    _value = _value + ("00" + (dblCentavos).ToString().Replace(".", "").Replace(",", "")).Right(2);
                    _value = _value.Right(12); ;

                    for (int i = 0; i < 12; i++)
                    {
                        exp = Convert.ToInt32(_value.Substring(i, 1).ToString());

                        switch (i)
                        {
                            case 0:
                                {
                                    if (exp > 0)
                                        strValorExtenso += objeto.GetUnit(exp - 1) + " " + objeto.GetBillionText(exp) + " ";
                                    break;
                                }
                            case 1:
                                {
                                    if (Convert.ToInt32(_value.Substring(i, 3).ToString()) > 0)
                                    {
                                        if (exp > 0)
                                        {
                                            if (exp > 1)
                                                strValorExtenso += objeto.GetHundreds(exp - 1);
                                            else
                                            {
                                                if (_value.ToString().Substring(i + 1, 1).CompareTo("0") != 0)
                                                    strValorExtenso += objeto.GetHundred();
                                                else
                                                    strValorExtenso += objeto.GetHundreds(0);
                                            }
                                        }
                                        exp = Convert.ToInt32(_value.Substring(i + 1, 1).ToString());
                                        if (exp > 1)
                                        {
                                            strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                            strValorExtenso += objeto.GetTenOver(exp - 1);
                                        }
                                        else
                                        {
                                            if (_value.ToString().Substring(i + 1, 1).CompareTo("0") != 0)
                                            {
                                                strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                strValorExtenso += objeto.GetTen(0);
                                            }
                                        }

                                        exp = Convert.ToInt32(_value.Substring(i + 2, 1).ToString());
                                        if (Convert.ToInt32(_value.Substring(i + 1, 1).ToString()) != 1)
                                        {
                                            if (exp > 1)
                                            {
                                                strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                strValorExtenso += objeto.GetUnit(exp - 1);
                                            }
                                            else
                                            {
                                                if (exp == 1)
                                                    strValorExtenso += objeto.GetUnit(0);
                                            }
                                        }

                                        strValorExtenso += " " + objeto.GetMillionText(Convert.ToInt32(_value.Substring(0, i + 3))) + " ";

                                    }
                                    break;
                                }


                            case 4:
                                {
                                    if (Convert.ToInt32(_value.Substring(i, 3).ToString()) > 0)
                                    {
                                        if (exp > 0)
                                        {
                                            if (exp > 1)
                                                strValorExtenso += objeto.GetHundreds(exp - 1);
                                            else
                                            {
                                                if (_value.ToString().Substring(i + 1, 1).CompareTo("0") != 0)
                                                    strValorExtenso += " " + objeto.GetHundred() + " ";
                                                else
                                                    strValorExtenso += objeto.GetHundreds(0);
                                            }
                                        }
                                        exp = Convert.ToInt32(_value.Substring(i + 1, 1).ToString());
                                        if (exp > 1)
                                        {
                                            strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                            strValorExtenso += objeto.GetTenOver(exp - 1);
                                        }
                                        else
                                        {
                                            if (_value.ToString().Substring(i + 1, 1).CompareTo("0") != 0)
                                            {
                                                strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                strValorExtenso += objeto.GetTen(0);
                                            }
                                        }

                                        exp = Convert.ToInt32(_value.Substring(i + 2, 1).ToString());
                                        if (Convert.ToInt32(_value.Substring(i + 1, 1).ToString()) != 1)
                                        {
                                            if (exp > 1)
                                            {
                                                strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                strValorExtenso += objeto.GetUnit(exp - 1);
                                            }
                                            else
                                            {
                                                if (exp == 1)
                                                    strValorExtenso += objeto.GetUnit(0);
                                            }
                                        }
                                        strValorExtenso += " " + objeto.GetMile() + " ";

                                    }
                                    break;
                                }

                            case 7:
                                {
                                    if (Convert.ToInt32(_value.Substring(i, 3).ToString()) > 0)
                                    {
                                        if (exp > 0)
                                        {
                                            if (exp > 1)
                                                strValorExtenso += objeto.GetHundreds(exp - 1);
                                            else
                                            {
                                                if (_value.ToString().Substring(i + 1, 1).CompareTo("0") != 0)
                                                    strValorExtenso += " " + objeto.GetHundred() + " ";
                                                else
                                                    strValorExtenso += objeto.GetHundreds(0);
                                            }
                                        }
                                        exp = Convert.ToInt32(_value.Substring(i + 1, 1).ToString());
                                        if (exp > 0)
                                        {
                                            if (exp > 1)
                                            {
                                                strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                strValorExtenso += objeto.GetTenOver(exp - 1);
                                            }
                                            else
                                            {
                                                if (_value.ToString().Substring(i + 1, 1).CompareTo("0") == 0)
                                                {
                                                    strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                    strValorExtenso += objeto.GetTenOver(0);
                                                }
                                                else
                                                {
                                                    strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                    strValorExtenso += objeto.GetTen(exp - 1);
                                                }
                                            }
                                        }

                                        exp = Convert.ToInt32(_value.Substring(i + 2, 1).ToString());
                                        if (exp > 0)
                                        {
                                            if (Convert.ToInt32(_value.Substring(i + 1, 1).ToString()) != 1)
                                            {
                                                if (exp > 1)
                                                {
                                                    strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                    strValorExtenso += objeto.GetUnit(exp - 1);
                                                }
                                                else
                                                {
                                                    if (exp == 1)
                                                    {
                                                        strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                        strValorExtenso += objeto.GetUnit(0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(_value.Substring(0, i + 3).ToString()) == 1)
                                        strValorExtenso += " " + objeto.SingleMonetaryUnit + " ";
                                    else
                                        if (Convert.ToInt32(_value.Substring(0, i + 3).ToString()) != 0)
                                        strValorExtenso += " " + objeto.PluralMonetaryUnit + " ";
                                    else
                                        strValorExtenso += " ";

                                    break;
                                }

                            case 10:
                                {
                                    if (Convert.ToInt32(_value.Substring(i, 2).ToString()) > 0)
                                    {
                                        exp = Convert.ToInt32(_value.Substring(i, 1).ToString());
                                        if (exp > 0)
                                        {
                                            if (exp > 1)
                                                strValorExtenso += " e " + objeto.GetTenOver(exp - 1);
                                            else
                                            {
                                                if (_value.ToString().Substring(i + 1, 1).CompareTo("0") != 0)
                                                {
                                                    strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                    strValorExtenso += objeto.GetTen(Convert.ToInt32(_value.Substring(i + 1, 1).ToString()) - 1);
                                                }
                                                else
                                                {
                                                    strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                    strValorExtenso += objeto.GetTenOver(exp - 1);
                                                }
                                            }
                                        }

                                        exp = Convert.ToInt32(_value.Substring(i + 1, 1).ToString());
                                        if (exp > 0)
                                        {
                                            if (Convert.ToInt32(_value.Substring(i, 1).ToString()) != 1)
                                            {
                                                if (exp > 1)
                                                {
                                                    strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                    strValorExtenso += objeto.GetUnit(exp - 1);
                                                }
                                                else
                                                {
                                                    if (exp == 1)
                                                    {
                                                        strValorExtenso += strValorExtenso.Trim().CompareTo("") == 0 ? "" : " e ";
                                                        strValorExtenso += objeto.GetUnit(0);
                                                    }
                                                }
                                            }
                                        }
                                        strValorExtenso += objeto.GetCents(Convert.ToInt32(_value.Substring(i, 2).ToString()));

                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            objeto = null;
            return strValorExtenso.Replace("  ", " ").Trim();
        }
    }
}
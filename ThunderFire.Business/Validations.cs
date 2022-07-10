using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderFire.Domain.Models;

namespace ThunderFire.Business
{
    public static class Validations
    {


        public static string MessageToUser { get; set; }


        public static string Iso { get; set; }

        /// <term>-2</term>
        /// <description>Valor maior ou igual ao valor mínimo</description>
        /// </item>
        /// <item>
        /// <term>-3</term>
        /// <description>O Valor está dentro da faixa de limite</description>
        /// </item>
        /// </list>
        ///</remarks>
        public static int ValueRange(double pvalue, double pvlrmin, double pvlrmax)
        {
            // Não há limite definido
            if (pvlrmin == 0 && pvlrmax == 0)
                return 0;

            if (pvlrmax > 0)
            {
                if (pvalue > pvlrmax)
                    return -1;
            }
            if (pvalue < pvlrmin)
                return -2;

            if (!(pvalue >= pvlrmin && pvalue <= pvlrmax))
            {
                return -3;
            }
            return 0;
        }




        /// <summary>
        /// Valida o Limite de Aplicação de Valor
        /// </summary>
        /// <param name="tarifa">Tarifacao</param>
        /// <param name="pVLRMOV">Valor do Movimento</param>
        /// <returns>true, se o limite for válido</returns>
        public static bool ValidLimit(Tariff tarifa, double pVLRMOV)
        {
            Iso = "00";
            bool go = true;
            int _TRFVAL = ValueRange(pVLRMOV, tarifa.VLRINF, tarifa.VLRMAX);
            if (_TRFVAL == 0)
            {
                go = true;
            }
            else
            {
                if (_TRFVAL == -1)
                {
                    MessageToUser = String.Format("O VALOR DE {1:C2} E MAIOR QUE O LIMITE MAXIMO DE {2:C2}", "R$", pVLRMOV, tarifa.VLRMAX);
                }
                if (_TRFVAL == -2)
                {
                    MessageToUser = String.Format("O VALOR DE {1:C2} E MENOR QUE O LIMITE MINIMO DE {2:C2}", "R$", pVLRMOV, tarifa.VLRINF);
                }
                if (_TRFVAL == -3)
                {
                    MessageToUser = String.Format("O VALOR DE {1:C2} ESTA FORA DA FAIXA DE {2:C2} E {3:C2} PERMITIDA", "R$", pVLRMOV, tarifa.VLRINF, tarifa.VLRMAX);
                }
                Iso = "13";
                go = false;
            }

            return go;
        }

    }
}

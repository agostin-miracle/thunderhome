using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Models
{
    public class ManagementModel : ModelsBase
    {
        public List<ActiveCards> Lista = null;
        public List<MyUsers> ListaGestor = null;
        public List<TransactionStatus> ListaStatus = null;
        public int CurrentUser { get; set; } = 0;
        public int CUSUPRO { get; set; } = 0;
        public short CSTACRT { get; set; } = 0;
    }


    public class MonthlyPaymentModel : ModelsBase
    {
        public List<ActiveCards> Lista = null;
        public List<MyUsers> ListaGestor = null;
        public List<TransactionStatus> ListaStatus = null;
        public List<GeneralTable> ListaTipoMensalidade = null;
        public List<MyUsers> Listausuarios = null;
        public int CurrentUser { get; set; } = 0;
        public int CUSUPRO { get; set; } = 0;
        public short CSTAMEN { get; set; } = 0;

        public int CCODUSU { get; set; } = 0;
        public byte CTIPMEN { get; set; } = 0;

    }



    public class BilletConfigurationModel : ModelsBase
    {
        public int CUSUCFG { get; set; } = 0;
        public short CNIVCFG { get; set; } = 0;
        public short PNIVCFG { get; set; } = 0;
        public int PUSUCFG { get; set; } = 0;
        public int FUSUCFG { get; set; } = 0;
        public short FNIVCFG { get; set; } = 0;

        public short FCODTAR { get; set; } = 0;
        public short FTIPBOL { get; set; } = 0;
        public short FAPLTDP { get; set; } = 0;

        public short FTARTDP { get; set; } = 0;
        public List<MyUsers> ListaUsuarios = new List<MyUsers>();
        public List<GeneralTable> ListaTipoBoletos = new List<GeneralTable>();

        public List<TariffType> ListaTarifas = new List<TariffType>();
        public List<GeneralTable> ListaTaxaDepositante = new List<GeneralTable>();

    }
}
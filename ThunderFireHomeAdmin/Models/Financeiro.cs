using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Models
{
    public class TicketModel
    {

        public List<MyUsers> CListaCedentes { get; set; }
        public List<MyUsers> CListaGestores { get; set; }
        public List<MyUsers> CistaSacados { get; set; }


        public List<MyUsers> ListaCedentes { get; set; }
        public List<MyUsers> ListaGestores { get; set; }
        public List<MyUsers> ListaSacados { get; set; }
        public List<MyUsers> ListaAvalistas { get; set; }
        public List<GeneralTable> ListaResponsavel { get; set; }
        public List<TransactionStatus> ListaStatusBoleto { get; set; }
        public List<GeneralTable> ListaTipoBoleto { get; set; }


        public int? PUSUPRO { get; set; }

        public int CUSUPRO { get; set; }
        public int CUSUCED { get; set; }
        public int CUSUSAC { get; set; }
        public short CSTABOL { get; set; }
        public short CTIPBOL { get; set; }
        public short CNIDBOL { get; set; }

        public short FSTABOL { get; set; }
        public short FTIPBOL { get; set; }

        public int FUSUPRO { get; set; }
        public int FUSUCED { get; set; }
        public int FUSUSAC { get; set; }

        public int FCODAVA { get; set; }
        public short FRSPTAR { get; set; }

        public string CDATEM1 { get; set; }
        public string CDATEM2 { get; set; }


        public string CDATVC1 { get; set; }
        public string CDATVC2 { get; set; }

        public string CDATPG1 { get; set; }
        public string CDATPG2 { get; set; }

    }


    public class TicketReceiptRecordModel : ModelsBase
    {

    }


    public class TransferRegistrationModel : ModelsBase
    {

        public List<GeneralTable> ListaIndicadorDebito { get; set; }
        public List<GeneralTable> ListaIndicadorCredito { get; set; }
        public List<AccountEntryType> ListaTipoLancamento { get; set; }

        public List<MyUsers> ListaContaDebito { get; set; }
        public List<MyUsers> ListaContaCredito { get; set; }

        public short CTIPLCT { get; set; }
        public short FTIPLCT { get; set; }
        public short FINDDEB { get; set; }
        public short FINDCRD { get; set; }

        public int FCTADEB { get; set; }
        public int FCTACRD { get; set; }

    }

    public class AccountEntryTypeModel : ModelsBase
    {
        public List<GeneralTable> ListaIndicadorLancamento { get; set; }
        public List<GeneralTable> ListaIndicadorDebito { get; set; }
        public List<GeneralTable> ListaIndicadorCredito { get; set; }
        public List<TariffType> ListaTipoTarifa { get; set; }

        public List<GeneralTable> ListaAssociacaoOrigem { get; set; }
        public short FINDLCT { get; set; }
        public short FINDDEB { get; set; }
        public short FINDCRD { get; set; }
        public short FCODTAR { get; set; }
        public short FORGDEB { get; set; }
        public short FORGCRD { get; set; }

    }

    public class AccountEntryPostingModel : ModelsBase
    {
        public List<AccountEntryType> ListaTipoLancamento { get; set; }
        public List<GeneralTable> ListaIndicador { get; set; }
        public List<GeneralTable> ListaTransacaoMovimento { get; set; }

        public List<Operations> ListaOperacao { get; set; }
        public List<TransactionStatus> ListaStatusTransacao { get; set; }

        public short CTIPLCT { get; set; }
        public short FCODTRM { get; set; }
        public short FTIPLCT { get; set; }
        public short FTIPLCT1 { get; set; }
        public short FTIPLCT2 { get; set; }
        public short FTIPLCT3 { get; set; }
        public short FINDDEB { get; set; }
        public short FINDCRD { get; set; }
        public short FMOVDEB { get; set; }
        public short FMOVCRD { get; set; }
        public short FSTAREG { get; set; }

    }

}
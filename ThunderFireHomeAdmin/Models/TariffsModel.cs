using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Models
{
    public class TariffsModel:ModelsBase
    {
        public byte CNIVCFG { get; set; } = 0;
        public int CUSUCFG { get; set; } = 0;

        public byte FNIVCFG { get; set; } = 0;
        public int FUSUCFG { get; set; } = 0;
        public short FCODTAR { get; set; } = 0;
        public short FCODBND { get; set; } = 0;
        public short FTIPLIN { get; set; } = 0;
        public short FMODCRT { get; set; } = 0;
        public int FCODEXP { get; set; } = 0;
        public int FFMTCOB { get; set; } = 0;
        public int FRSPTAR { get; set; } = 0;

        public List<MyUsers> ListaUsuarios = null;
        public List<GeneralTable> ListaResponsavel = null;
        public List<GeneralTable> ListaNivelConfiguracao = null;
        public List<GeneralTable> ListaBandeiras = null;
        public List<GeneralTable> ListaLinhas = null;
        public List<GeneralTable> ListaFormaCobranca = null;
        public List<TariffModality> ListaModalidade = null;
        public List<TariffType> ListaTarifa = null;
        public List<ExpandedTypes> ListaTiposExpansao = null;
    }

    public class ExpandedTariffModel : ModelsBase
    {
        public byte FTIPEXP { get; set; } = 0;
        public byte CTIPEXP { get; set; } = 0;
        public List<ExpandedTypes> ListaTiposExpansao = null;
    }

    /// <summary>
    /// Tipos de Expansão 
    /// </summary>
    public class ExpandedTypesModel : ModelsBase
    {
        public byte FLVLREG { get; set; } = 0;
        public List<GeneralTable> ListaRegrasExpansao = null;
    }

    /// <summary>
    /// Tarifas x Operacoes 
    /// </summary>
    public class TariffOperationsModel : ModelsBase
    {
        public short FCODTAR { get; set; } = 0;
        public short FCODMOV { get; set; } = 0;
        public List<TariffType> ListaTarifas = null;
        public List<Operations> ListaOperacoes = null;
    }


    /// <summary>
    /// Tarifas x Operacoes 
    /// </summary>
    public class TariffModalityModel : ModelsBase
    {
    }


}
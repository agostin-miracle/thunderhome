using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Models
{
    public class OperationsModel
    {
        public int FSIGOPE { get; set; }
        public int FSUBSYS { get; set; }
        public int CSUBSYS { get; set; }
        public int FCNDBLO { get; set; }
        public List<GeneralTable> ListaSinais = null;
        public List<GeneralTable> ListaSubSistema = null;
        public List<GeneralTable> ListaCondicaoBloqueio = null;

    }
}
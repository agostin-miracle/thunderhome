using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Models
{
    
    public class GeneralTableModel : ModelsBase
    {
        public int CNUMTAB { get; set; }
        public int FNUMTAB { get; set; }

        public List<GeneralTable> ListaTabela = null;
    }
}
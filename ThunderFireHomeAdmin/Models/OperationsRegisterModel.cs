using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Models
{
    public class OperationsRegisterModel:ModelsBase
    {

        public List<OperationsRegister> Lista { get; set; }
        public List<Operations> ListaOperacoes { get; set; }
        public List<GeneralTable> ListaSinal { get; set; }

        public short FCODMOV { get; set; } = 0;
        public byte pSUBSYS { get; set; } = 0;
        public int pNIDREF { get; set; } = 0;
        public int FSIGOPE { get; set; } = 0;
    }
}
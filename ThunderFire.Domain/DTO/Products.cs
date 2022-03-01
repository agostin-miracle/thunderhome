using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderFire.Domain.Models;

namespace ThunderFire.Domain.DTO
{
    public class ProductsAll:Products
    {
        public string DSCREC { get; set; }
        public string DSCUPD { get; set; }
        public string DSCLIN { get; set; }
        public string DSCSTA { get; set; }
    }


    public class ProductsActiveCards
    {
        public short CODPRO { get; set; }
        public string DSCPRO { get; set; }
        public short STACRT { get; set; }
        public string DSCSTA { get; set; }
        public string DSCOPR { get; set; }
        public int QTDCRT { get; set; }
    }

    public class ProductManagementUsers
    {
        public short CODUSU { get; set; }
        public string NOMUSU { get; set; }
    }

}

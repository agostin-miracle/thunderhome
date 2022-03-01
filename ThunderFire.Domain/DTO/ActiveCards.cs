using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderFire.Domain.Models;

namespace ThunderFire.Domain.DTO
{
    public class ActiveCardsQuery : ActiveCards
    {
        public string NOMUSU { get; set; }
        public string DSCTOM { get; set; }
        public string DSCREC { get; set; }
        public string DSCSTA { get; set; }
        public string DSCLGN { get; set; }
        public string NOMPRO { get; set; }
    }
}

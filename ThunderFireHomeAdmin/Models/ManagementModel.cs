using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Models
{
    public class ManagementModel : ModelsBase
    {
        public List<ActiveCards> Lista = null;
        public List<MyUsers> ListaGestor = null;
        public List<QueryTransactionStatus> ListaStatus = null;
        public int CurrentUser { get; set; } = 0;
        public int CUSUPRO { get; set; } = 0;
        public short CSTACRT { get; set; } = 0;
    }
}
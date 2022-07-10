using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.DTO;

namespace ThunderFireHomeAdmin.Models
{

    public class LinkedAccountModel : ModelsBase
    {
        public List<MyUsers> ListaUsuario = null;

        public byte CanRemoveLinkId { get; set; } = 1;

        public int PCODUSU { get; set; }
        public int CCODUSU { get; set; }
        public int FCODUSU { get; set; }
    }

}
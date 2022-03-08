using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Models
{
    public class GeneralRegistryModel : ModelsBase
    {
        public List<QueryGeneralRegistry> Lista = null;
        public List<AttributeType> ListaAtributo = null;
        public List<UserType> ListaTipoUsuario = null;
        public List<GeneralTable> ListaNivelConfianca = null;
        public List<GeneralTable> ListaStatusRegistro = null;
        public List<QueryGeneralRegistry> ListaGestor = null;
        public List<QueryTransactionStatus> ListaStatusUsuario = null;
        public List<GeneralTable> ListaGenero = null;
        public List<GeneralTable> ListaNacionalidade = null;
        
    }

}
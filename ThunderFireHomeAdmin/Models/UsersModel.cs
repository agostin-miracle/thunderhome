using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Models
{
    public class UsersModel : ModelsBase
    {
        public List<AttributeType> ListaAtributo = null;
        public List<UserType> ListaTipoUsuario = null;
        public List<GeneralTable> ListaNivelConfianca = null;
        public List<MyUsers> ListaGestor = null;
        public List<TransactionStatus> ListaStatusUsuario = null;
        public List<GeneralTable> ListaGenero = null;
        public List<GeneralTable> ListaNacionalidade = null;


        public List<GeneralTable> ListaStatusRegistro = null;
        public List<MyUsers> CListaGestor = null;
        public List<TransactionStatus> CListaStatusUsuario = null;
        public List<AttributeType> CListaAtributo = null;
        public int FSRCUSU { get; set; }
        public int CSRCUSU { get; set; }
        public short CSTAUSU { get; set; }
        public short CSTAREC { get; set; }
        public short FSTAUSU { get; set; }
        public short CCODATR { get; set; }
        public short FCODATR { get; set; }
        public string FTIPPES { get; set; }
        public byte FNIVCFA { get; set; }
        public byte FCODNAC { get; set; }
        public byte FTIPUSU { get; set; }
    }

}
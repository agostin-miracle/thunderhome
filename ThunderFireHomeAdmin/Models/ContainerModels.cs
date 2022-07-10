using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;

namespace ThunderFireHomeAdmin.Models
{
    public class LogonModel : ModelsBase
    {
        public int AccessType { get; set; } = 1;
    }

  



    public class UserTypeModel : ModelsBase
    {
        public List<UserType> Lista = null;
    }

    public class AddressTypeModel : ModelsBase
    {
        public List<AddressType> Lista = null;
    }

    public class ContactTypeModel : ModelsBase
    {
        public List<ContactType> Lista = null;
    }

   

    public class ProductLineModel : ModelsBase
    {
        public List<ProductLine> Lista = null;
    }


    public class ProductManagerModel : ModelsBase
    {
        public List<Product> ListaProduto = null;
        public List<MyUsers> ListaGestor = null;
        public int FCODUSU { get; set; }
        public short FCODPRO { get; set; }
        public int CCODUSU { get; set; }
        public short CCODPRO { get; set; }

    }

    public class ProductModel : ModelsBase
    {
        public List<ProductLine> ListaLinhaProduto = null;
        public List<ProductLine> CListaLinhaProduto = null;
        public short CLINPRO { get; set; }
        public short FLINPRO { get; set; }
    }

    public class AddressBookModel : ModelsBase
    {
        public int CodigoUsuario = 0;
        public int UsuarioSelecionado = 0;
        public List<QueryAddressBook> Lista = null;
        public List<MyUsers> ListaUsuario= null;
        public List<AddressType> ListaTipoEndereco = null;
        public List<GeneralTable> ListaTipoLogradouro = null;
        public List<GeneralTable> ListaUF = null;
        public List<GeneralTable> ListaPais = null;

        public int PCODUSU { get; set; }
        /// <summary>
        /// Código do Endereço
        /// </summary>
        public int CCODEND { get; set; }
        public int CCODUSU { get; set; }
        public int FCODUSU { get; set; }
        public int FCODPAI { get; set; }
        public string FCODUFE { get; set; }
        public short FTIPEND { get; set; }
        public short FTIPLOG { get; set; }
    }

    public class ContactBookModel : ModelsBase
    {
        public List<AddressBook> Lista = null;
        public List<MyUsers> ListaUsuario = null;
        public List<ContactType> ListaTipoContato = null;
        public List<GeneralTable> ListaOperadora = null;
        public List<GeneralTable> ListaPais = null;

        public int PCODUSU { get; set; }
        public int CCODUSU { get; set; }

        /// <summary>
        /// Código do Endereço
        /// </summary>
        public int CCODEND { get; set; } = 0;

        public int FCODPAI { get; set; }
        public int FCODUSU { get; set; }
        public short FCODOPR { get; set; }
        public short FTIPCTO { get; set; }

    }
    public class TransactionStatusModel : ModelsBase
    {
        public short CCODMOD { get; set; }
        public short FCODMOD { get; set; }
        public short FSIGOPE { get; set; }
        public List<GeneralTable> ListaModulos = null;
    }

    public class VirtualAccountModel : ModelsBase
    {
        public List<MyUsers> ListaUsuario = null;
        public List<MyUsers> CListaUsuario = null;
        public List<GeneralTable> ListaBancos = null;
        public List<GeneralTable> ListaTipoBeneficiario = null;
        public List<AccountType> ListaTipoConta = null;
        public List<TransactionStatus> ListaStatusConta = null;
        public List<TransactionStatus> CListaStatusConta = null;
        public List<GeneralTable> ListaOrigemConta = null;

        public int PCODUSU { get; set; }
        public int CCODUSU { get; set; }
        public int FCODUSU { get; set; }
        public short FTIPCTA { get; set; } = 1;
        public short FTIPBNF { get; set; } = 1;
        public short FORGCTA { get; set; } = 1;
        public string FNUMBCO { get; set; } = "000";

        public short FSTACTA { get; set; }
        public short CSTACTA { get; set; }
        public short PROCESS_ID { get; set; } = 12;
        public bool APPROVAL_ACCOUNT { get; set; }

    }

}
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

    public class AttributeTypeModel : ModelsBase
    {
        public List<AttributeType> Lista = null;
    }

    public class AccountTypeModel : ModelsBase
    {
        public List<AccountType> Lista = null;
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

    public class GeneralTableModel : ModelsBase
    {
        public List<GeneralTable> Lista = null;
        public List<GeneralTable> ListaTabela = null;
    }

    public class ProductLineModel : ModelsBase
    {
        public List<ProductLine> Lista = null;
    }


    public class ProductManagementModel : ModelsBase
    {
        public List<QueryProductManagement> Lista= null;
        public List<QueryProducts> ListaProduto = null;
        public List<QueryGeneralRegistry> ListaGestor = null;
    }

    public class ProductsModel : ModelsBase
    {
        public List<QueryProducts> Lista = null;
        public List<ProductLine> ListaLinhaProduto = null;
    }

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

    public class AddressBookModel : ModelsBase
    {
        public int CodigoUsuario = 0;
        public int UsuarioSelecionado = 0;
        public List<QueryAddressBook> Lista = null;
        public List<QueryGeneralRegistry> ListaUsuario= null;
        public List<AddressType> ListaTipoEndereco = null;
        public List<GeneralTable> ListaTipoLogradouro = null;
        public List<GeneralTable> ListaUF = null;
        public List<GeneralTable> ListaPais = null;
    }

    public class ContactBookModel : ModelsBase
    {
        public int CodigoUsuario = 0;
        public int CodigoEndereco = 0;
        public List<AddressBook> Lista = null;
        public List<QueryGeneralRegistry> ListaUsuario = null;
        public List<ContactType> ListaTipoContato = null;
        public List<GeneralTable> ListaOperadora = null;
        public List<GeneralTable> ListaPais = null;
    }
    public class TransactionStatusModel : ModelsBase
    {
        public List<GeneralTable> ListaModulos = null;
    }

    public class VirtualAccountModel : ModelsBase
    {
        public int CodigoUsuario = 0;
        public int UsuarioSelecionado = 0;
        public List<GeneralRegistry> ListaUsuario = null;
        public List<GeneralTable> ListaBancos = null;
        public List<GeneralTable> ListaTipoBeneficiario = null;
        public List<AccountType> ListaTipoConta = null;
        public List<QueryTransactionStatus> ListaStatusConta = null;
        public List<GeneralTable> ListaOrigemConta = null;
    }

}
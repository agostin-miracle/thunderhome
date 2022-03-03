using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Identificação de Campo de Origem
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class FieldAttribute : System.Attribute
    {
        private string _name;
        private string _description = "";
        private int _size = 0;
        private string _type = "";
        private string _comments;
        private int _ordinal = 0;
        private string _defaultvalue = "";
        private bool _nulavel = false;
        /// <summary>
        /// Atributos de campo
        /// </summary>
        /// <param name="name">Nome do Campo</param>
        /// <param name="description">Descrição</param>
        /// <param name="size">Tamanho</param>
        /// <param name="type">Tipo</param>
        /// <param name="comments">Comentários</param> 
        /// <param name="ordinal">Posição Ordinal</param> 
        /// <param name="defaultvalue">Valor Default</param> 
        /// <param name="nulavel">Nulável</param>  
        /// <param name="sqltype">SQL Type</param>  
        /// <param name="obsoleto">Indica se o campo é obsoleto para uso</param>  
        public FieldAttribute(string name, string description, int size, string type, string comments, int ordinal, string defaultvalue, bool nulavel, string sqltype, bool obsoleto)
        {
            this._name = name;
            this._description = description;
            this._size = size;
            this._type = type;
            this._comments = comments;
            this.OrdinalPosition = ordinal;
            this.DefaultValue = defaultvalue;
            this.Nulavel = nulavel;
            this.SQLType = sqltype;
            this.Obsoleto = obsoleto;
        }

        /// <summary>
        /// Atributos de campo
        /// </summary>
        /// <param name="name">Nome do Campo</param>
        /// <param name="description">Descrição</param>
        /// <param name="size">Tamanho</param>
        /// <param name="type">Tipo</param>
        /// <param name="comments">Comentários</param> 
        /// <param name="ordinal">Posição Ordinal</param> 
        /// <param name="defaultvalue">Valor Default</param> 
        /// <param name="nulavel">Nulável</param>  
        /// <param name="sqltype">SQL Type</param>  
        public FieldAttribute(string name, string description, int size, string type, string comments, int ordinal, string defaultvalue, bool nulavel, string sqltype)
        {
            this._name = name;
            this._description = description;
            this._size = size;
            this._type = type;
            this._comments = comments;
            this.OrdinalPosition = ordinal;
            this.DefaultValue = defaultvalue;
            this.Nulavel = nulavel;
            this.SQLType = sqltype;
        }

        /// <summary>
        /// Título
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Descrição do Campo
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// Tamanho do Campo
        /// </summary>
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
        /// <summary>
        /// Tipo do Campo
        /// </summary>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// Comentários
        /// </summary>
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
        /// <summary>
        /// Posição Ordinal
        /// </summary>
        public int OrdinalPosition
        {
            get { return _ordinal; }
            set { _ordinal = value; }
        }
        /// <summary>
        /// Valor Default
        /// </summary>
        public string DefaultValue
        {
            get { return _defaultvalue; }
            set { _defaultvalue = value; }
        }
        /// <summary>
        /// Nulável
        /// </summary>
        public bool Nulavel
        {
            get { return _nulavel; }
            set { _nulavel = value; }
        }
        /// <summary>
        /// SQL Type
        /// </summary>
        public string SQLType { get; set; } = "";
        /// <summary>
        /// Indica se o campo é obsoleto para uso
        /// </summary>
        public bool Obsoleto { get; set; } = false;

    }

    /// <summary>
    /// Caption Attribute
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class CaptionAttribute : System.Attribute
    {
        private string _caption;

        /// <summary>
        /// Caption Instance
        /// </summary>
        /// <param name="caption">Rótulo</param>
        public CaptionAttribute(string caption)
        {
            this._caption = caption;
        }

        /// <summary>
        /// Rótulo
        /// </summary>
        public string Caption
        {
            get { return _caption; }
        }
    }

    /// <summary>
    /// Atributos para Identificação de Classes
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class ClassIdAttribute : System.Attribute
    {

        /// <summary>
        /// Procedure Instance
        /// </summary>
        /// <param name="name">Nome da Classe</param>
        /// <param name="tablename">Nome da Tabela</param>
        /// <param name="description">Descrição da Tabela</param>
        public ClassIdAttribute(string name, string tablename, string description)
        {
            this.Name = name;
            this.TableName = tablename;
            this.Description = description;
        }
        /// <summary>
        /// Nome da Classe
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nome da Tabela
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; }

    }


    /// <summary>
    /// Atributos para Procedures
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class ProcNameAttribute : System.Attribute
    {
        private string _procname;
        private string _description = "";

        /// <summary>
        /// Procedure Instance
        /// </summary>
        /// <param name="procname">Nome da Procedure</param>
        public ProcNameAttribute(string procname)
        {
            this._procname = procname;
        }
        /// <summary>
        /// Nome da Procedure
        /// </summary>
        public string ProcName
        {
            get { return _procname; }
        }
        /// <summary>
        /// Descrição
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

    }

    /// <summary>
    /// Field Item for Class
    /// </summary>
    public class FieldItem
    {
        /// <summary>
        /// Título
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição do Campo
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Tamanho do Campo
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Tipo do Campo
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Comentários
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// Posição Ordinal
        /// </summary>
        public int OrdinalPosition { get; set; }
        /// <summary>
        /// Valor Default
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// Nulável
        /// </summary>
        public bool Nulavel { get; set; }
        /// <summary>
        /// SQL Type
        /// </summary>
        public string SQLType { get; set; } = "";
        /// <summary>
        /// Indica se o campo é obsoleto para uso
        /// </summary>
        public bool Obsoleto { get; set; } = false;

        /// <summary>
        /// Instância de Classe
        /// </summary>
        public FieldItem() { }
        /// <summary>
        /// Instância de Classe
        /// </summary>
        /// <param name="name">Nome do Campo</param>
        /// <param name="description">Descrição</param>
        /// <param name="size">Tamanho</param>
        /// <param name="type">Tipo</param>
        /// <param name="comments">Comentários</param> 
        /// <param name="ordinal">Posição Ordinal</param> 
        /// <param name="defaultvalue">Valor Default</param> 
        /// <param name="nulavel">Nulável</param>  
        /// <param name="sqltype">SQL Type</param>  
        /// <param name="obsoleto">Indica se o campo é obsoleto para uso</param>  
        public FieldItem(string name, string description, int size, string type, string comments, int ordinal, string defaultvalue, bool nulavel, string sqltype, bool obsoleto)
        {
            this.Name = name;
            this.Description = description;
            this.Size = size;
            this.Type = type;
            this.Comments = comments;
            this.OrdinalPosition = ordinal;
            this.DefaultValue = defaultvalue;
            this.Nulavel = nulavel;
            this.SQLType = sqltype;
            this.Obsoleto = obsoleto;
        }
    }

    /// <summary>
    /// Identificação de Campo de Origem
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class ISOAttribute : System.Attribute
    {
        /// <summary>
        /// Id do Campo
        /// </summary>
        public int Field { get; set; } = 0;
        /// <summary>
        /// Descriçãos
        /// </summary>
        public string Description { get; set; } = "";
        /// <summary>
        /// Tamanho
        /// </summary>
        public int Size { get; set; } = 0;
        /// <summary>
        /// Tipo
        /// </summary>
        public Type Type { get; set; } = typeof(System.String);
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="field">ID do Campo</param>
        /// <param name="description">Descrição</param>
        public ISOAttribute(int field, string description) : this(field, description, 0, typeof(System.String)) { }
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="field">ID do Campo</param>
        /// <param name="description">Descrição</param>
        /// <param name="size">Tamanho</param>
        public ISOAttribute(int field, string description, int size) : this(field, description, size, typeof(System.String)) { }
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="field">ID do Campo</param>
        /// <param name="description">Descrição</param>
        /// <param name="type">Tipo</param>
        public ISOAttribute(int field, string description, Type type) : this(field, description, 0, type) { }
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="field">ID do Campo</param>
        /// <param name="description">Descrição</param>
        /// <param name="size">Tamanho</param>
        /// <param name="type">Tipo</param>
        public ISOAttribute(int field, string description, int size, Type type)
        {
            this.Field = field;
            this.Description = description;
            this.Type = type;
            this.Size = size;
        }
    }
}
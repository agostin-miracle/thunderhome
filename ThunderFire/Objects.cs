using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ThunderFire
{
    /// <summary>
    /// Objects
    /// </summary>
    public class Objects
    {
        /// <summary>
        /// Captura de Erros
        /// </summary>
        public static TrapError TrappedError=new TrapError();

        /// <summary>
        /// Converte um objeto para uma string efetuando o parse por &amp;
        /// </summary>
        /// <param name="obj">Objeto a ser efetuado o parse</param>
        /// <returns>string</returns>
        public static string CreateContentString(Object obj)
        {
            TrappedError.SetError();
            string s = "";
            try
            {
                Type t = obj.GetType();
                PropertyInfo[] props = t.GetProperties();

                foreach (PropertyInfo prop in props)
                {
                    string _name = prop.Name.ToUpper();
                    object _value = prop.GetValue(obj);

                    if (_value == null)
                        _value = "";
                    s += string.Format("{0}={1}", _name, _value.ToString());
                    s += "&";

                }

                FieldInfo[] fields = t.GetFields();

                foreach (FieldInfo field in fields)
                {
                    string _name = field.Name.ToUpper();
                    string _value = field.GetValue(obj).ToString();
                    s += string.Format("{0}={1}", _name, _value.ToString());
                    s += "&";
                }
            }
            catch (Exception Error)
            {
                string LastErrorCode = ThunderFire.ErrorManager.GetErrorCode(Error);
                if (!String.IsNullOrWhiteSpace(LastErrorCode))
                    TrappedError.SetError(LastErrorCode);
                else
                    TrappedError.UserError = Error.Message;
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
                return "";
            }
            return s.Substring(0, s.Length - 1);
        }

        /// <summary>
        /// Cria um DataTable a partir de um objeto
        /// </summary>
        /// <param name="o">Objeto</param>
        /// <returns>DataTable</returns>
        public static DataTable CreateDataTable(object o)
        {
            TrappedError.SetError();
            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            o.GetType().GetProperties().ToList().ForEach(f =>
            {
                try
                {
                    f.GetValue(o, null);
                    dt.Columns.Add(f.Name, f.PropertyType);
                    dt.Rows[0][f.Name] = f.GetValue(o, null);
                }
                catch (Exception Error){
                    string LastErrorCode = ThunderFire.ErrorManager.GetErrorCode(Error);
                    if (!String.IsNullOrWhiteSpace(LastErrorCode))
                        TrappedError.SetError(LastErrorCode);
                    else
                        TrappedError.UserError = Error.Message;
                    TrappedError.ErrorMessage = Error.Message;
                    TrappedError.ErrorObject = Error;

                }
            });
            return dt;
        }       


        /// <summary>
        /// Converte um objeto em uma string
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>string</returns>
        public static string GetPropertiesValue(Object obj)
        {
            TrappedError.SetError();
            string tmp = "";
            try
            {
                Type t = obj.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (var prop in props)
                {
                    string _name = prop.Name.ToUpper();
                    object _value = prop.GetValue(obj);

                    if (_value == null)
                        _value = "";
                    tmp+= string.Format("{0}={1}", _name, _value.ToString().Trim()) + Environment.NewLine;
                }

                //FieldInfo[] fields = t.GetFields();

                //foreach (var field in fields)
                //{
                //    string _name = field.Name.ToUpper();
                //    string _value = field.GetValue(obj).ToString();
                //    tmp.AppendLine(string.Format("Field {0} = {1}", _name, _value.ToString()));
                //}
            }
            catch (Exception Error)
            {
                tmp+= string.Format("Error= {0}", Error.Message)+ Environment.NewLine;
                string LastErrorCode = ThunderFire.ErrorManager.GetErrorCode(Error);
                if (!String.IsNullOrWhiteSpace(LastErrorCode))
                    TrappedError.SetError(LastErrorCode);
                else
                    TrappedError.UserError = Error.Message;
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;

            }
            return tmp.Trim();
        }

        private static string SetField(string name, string value)
        {
            return String.Format("'{0}':'{1}'", name, value);
        }

        public static string GetPropertiesValue(string title, object obj, bool byline = true)
        {
            TrappedError.SetError();
            string tmp = "";
            try
            {
                Type t = obj.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (var prop in props)
                {
                    string _name = prop.Name.ToUpper();
                    object _value = prop.GetValue(obj);

                    if (_value == null)
                        _value = "";
                    if (!String.IsNullOrWhiteSpace(tmp))
                        tmp += ",";
                    tmp += SetField(_name, _value.ToString());

                }

                //FieldInfo[] fields = t.GetFields();

                //foreach (var field in fields)
                //{
                //    string _name = field.Name.ToUpper();
                //    string _value = field.GetValue(obj).ToString();
                //    tmp.AppendLine(string.Format("Field {0} = {1}", _name, _value.ToString()));
                //}
            }
            catch (Exception Error)
            {
                tmp += string.Format("Error= {0}", Error.Message) + Environment.NewLine;
                string LastErrorCode = ThunderFire.ErrorManager.GetErrorCode(Error);
                if (!String.IsNullOrWhiteSpace(LastErrorCode))
                    TrappedError.SetError(LastErrorCode);
                else
                    TrappedError.UserError = Error.Message;
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;

            }

            if (!String.IsNullOrWhiteSpace(tmp))
                tmp = "{" + tmp.Trim() + "}";

            //if (!String.IsNullOrWhiteSpace(title))
            //    tmp = title.Trim() + " " + tmp;

            return tmp.Trim();
        }
        /// <summary>
        /// Compara dois objetos do mesmo tipo e retorna as suas diferenças
        /// </summary>
        /// <typeparam name="T">Classe Alvo de Comparação</typeparam>
        /// <param name="Object1">Objeto de Comparação</param>
        /// <param name="object2">Objeto de Comparação</param>
        /// <returns>string</returns>
        public static string Compare<T>(T Object1, T object2)
        {
            string changes = "";
            Type type = typeof(T);

            if (object.Equals(Object1, default(T)) || object.Equals(object2, default(T)))
                changes = "";

            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                string Object1Value = string.Empty;
                string Object2Value = string.Empty;
                if (type.GetProperty(property.Name).GetValue(Object1, null) != null)
                    Object1Value = type.GetProperty(property.Name).GetValue(Object1, null).ToString();
                if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                    Object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                if (Object1Value.Trim() != Object2Value.Trim())
                    changes += ChangeField(property.Name, Object1Value, Object2Value);
            }
            return changes;
        }

        /// <summary>
        ///  Formata um mensagem de Alteração de valor
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="de"></param>
        /// <param name="para"></param>
        /// <returns>string</returns>
        private static string ChangeField(string caption, string de, string para)
        {
            return string.Format("Alterou {0} de {1} para {2}", caption, de, para);
        }
    }


    /// <summary>
    /// Compara objetos
    /// </summary>
    /// <typeparam name="T">Tipo de Objeto</typeparam>
    public static class ObjectHelper<T>
    {
        /// <summary>
        /// Define a mensagem de resultado de comparação
        /// </summary>
        public static string DescriptionChanged { get; set; } = "Campo {0} alterado de {1} para {2}";

        /// <summary>
        /// Compara dois objetos
        /// </summary>
        /// <param name="x">Objeto 1 </param>
        /// <param name="y">Objeto 2</param>
        /// <returns>int, resultado da comparação</returns>
        public static int Compare(T x, T y)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public);
            FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public);
            int compareValue = 0;

            foreach (PropertyInfo property in properties)
            {
                IComparable valx = property.GetValue(x, null) as IComparable;
                if (valx == null)
                    continue;
                object valy = property.GetValue(y, null);
                compareValue = valx.CompareTo(valy);
                if (compareValue != 0)
                    return compareValue;
            }
            foreach (FieldInfo field in fields)
            {
                IComparable valx = field.GetValue(x) as IComparable;
                if (valx == null)
                    continue;
                object valy = field.GetValue(y);
                compareValue = valx.CompareTo(valy);
                if (compareValue != 0)
                    return compareValue;
            }

            return compareValue;
        }
        /// <summary>
        /// Compara dois objetos
        /// </summary>
        /// <param name="x">Objeto 1 </param>
        /// <param name="y">Objeto 2</param>
        /// <remarks>
        /// O uso deste método requer que as propriedades estejam decorados com o atributo Field
        /// </remarks>
        /// <returns>StringBuilder com o descritivo de cada comparação</returns>
        public static StringBuilder CompareWrite(T x, T y)
        {
            string _caption = "";
            Type type = typeof(T);
            //PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            StringBuilder Log = new StringBuilder();
            int compareValue = 0;
            foreach (PropertyInfo property in properties)
            {
                IComparable valx = property.GetValue(x, null) as IComparable;
                if (valx == null)
                    continue;
                object valy = property.GetValue(y, null);
                compareValue = valx.CompareTo(valy);
                _caption = property.Name;
                var caption = property.GetCustomAttribute(typeof(CaptionAttribute), true);
                if (caption != null)
                    _caption = ((CaptionAttribute)caption).Caption;

                if (compareValue != 0)
                {
                    Log.Append(String.Format(DescriptionChanged, property.Name.ToString(), valx.ToString(), valy.ToString()));
                    Log.AppendLine();
                }
            }
            foreach (FieldInfo field in fields)
            {
                IComparable valx = field.GetValue(x) as IComparable;
                if (valx == null)
                    continue;
                object valy = field.GetValue(y);
                compareValue = valx.CompareTo(valy);
                var caption = field.GetCustomAttribute(typeof(CaptionAttribute), true);
                _caption = field.Name;
                if (caption != null)
                    _caption = ((CaptionAttribute)caption).Caption;
                if (compareValue != 0)
                {
                    Log.Append(String.Format(DescriptionChanged, _caption, valx.ToString(), valy.ToString()));
                    Log.AppendLine();
                }
            }
            return Log;
        }
    }
}
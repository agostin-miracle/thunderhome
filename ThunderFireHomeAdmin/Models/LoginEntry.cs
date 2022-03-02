using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire;

namespace ThunderFireHomeAdmin.Models
{
    public class LoginEntry
    {
        protected string _PSWUSU = "";
        public byte LGNTYP { get; set; } = 1;
        public string LGNUSU { get; set; } = "";
        public string PSWUSU
        {
            get { return _PSWUSU; }
            set {
                _PSWUSU = ThunderFire.UseRijndael.Encrypt(value);
            }
        }

    }
}
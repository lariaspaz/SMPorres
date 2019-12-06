using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ApiInscripción.Lib.Security.Authentication
{
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public BasicAuthenticationIdentity(string name, string password) : base(name, "Basic")
        {
            this.Password = password;
        } /// 

        /// Basic Auth Password for custom authentication /// 
        public string Password { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Consultas.Lib.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RolName { get; set; }

        public IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            return this.RolName == role;
        }

        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
    }
}
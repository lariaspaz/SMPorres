using Consultas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Consultas.Lib.Security
{
    public class CustomMembershipUser : MembershipUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RolUsuarioWeb Rol { get; set; }

        public CustomMembershipUser(AlumnoWeb user) : base("CustomMembership", user.Nombre, user.Id, "",
            string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now,
            DateTime.Now)
        {
            UserId = user.Id;
            FirstName = user.Nombre;
            LastName = user.Apellido;
            Rol = user.RolUsuarioWeb;
        }
    }
}
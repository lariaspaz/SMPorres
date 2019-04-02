using Consultas.Lib.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Lib
{
    public static class Session
    {
        public static CustomPrincipal CurrentUser
        {
            get
            {
                return (CustomPrincipal) HttpContext.Current.User;
            }
        }

        public static int CurrentUserId
        {
            get
            {
                return CurrentUser.UserId;
            }
        }
    }
}
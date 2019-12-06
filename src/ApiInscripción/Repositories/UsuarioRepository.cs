using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiInscripción.Repositories
{
    public class UsuarioRepository
    {
        public bool ExisteUsuario(string usuario, string password)
        {
            return (usuario == "admin") && (password == "123456");
        }
    }
}
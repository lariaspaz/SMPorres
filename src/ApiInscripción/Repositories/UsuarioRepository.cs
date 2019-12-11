using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiInscripción.Repositories
{
    public class UsuarioRepository
    {
        public static readonly string Usuario = "5f069cd8f8a54711bc09";
        public static readonly string Contraseña = "8fVHrkjz4P8wruEf0tviB/aWnLDJpz7UpXFjLfpUVFE=";

        public bool ExisteUsuario(string usuario, string password)
        {
            //return (usuario == "admin") && (password == "123456");
            return (usuario == Usuario) && (password == Contraseña);
        }
    }
}
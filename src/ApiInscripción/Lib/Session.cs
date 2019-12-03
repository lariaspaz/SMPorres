using ApiInscripción.Models;

namespace ApiInscripción.Lib
{
    public static class Session
    {
        private static Usuario _usuario = null;

        public static Usuario CurrentUser { get
            {
                if (_usuario == null)
                {
                    using (var db = new SMPorresEntities())
                    {
                        _usuario = db.Usuarios.Find(10000);
                    }
                }
                return _usuario;
            }
        }
    }
}

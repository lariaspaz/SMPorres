using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class CabecerasArchivosRepository
    {
        public static CabeceraArchivo Insertar(SMPorresEntities db, TipoArchivo tipo, string archivo)
        {
            var ca = new CabeceraArchivo();
            ca.Id = db.CabecerasArchivos.Any() ? db.CabecerasArchivos.Max(t => t.Id) + 1 : 1;
            ca.IdTipoArchivo = (int)tipo;
            ca.NombreArchivo = System.IO.Path.GetFileName(archivo);
            ca.IdUsuario = Lib.Session.CurrentUser.Id;
            ca.Hash = Lib.Security.Cryptography.CalcularMD5(archivo);
            ca.Fecha = Lib.Configuration.CurrentDate;
            db.CabecerasArchivos.Add(ca);
            return ca;
        }

        public static bool ExisteArchivo(TipoArchivo tipo, string archivo)
        {
            string firma = Lib.Security.Cryptography.CalcularMD5(archivo);
            using (var db = new SMPorresEntities())
            {
                return db.CabecerasArchivos.Any(ca => ca.Hash == firma);
            }
        }
    }
}

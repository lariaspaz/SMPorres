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
        public static void Insertar(TipoArchivo tipo, string archivo)
        {
            using (var db = new SMPorresEntities())
            {
                var ca = new CabeceraArchivo();
                ca.Id = db.CabecerasArchivos.Any() ? db.CabecerasArchivos.Max(t => t.Id) : 1;
                ca.Nombre = archivo;
                ca.IdUsuario = Lib.Session.CurrentUser.Id;
                ca.Firma = Lib.Security.Cryptography.CalcularMD5(archivo);
                ca.Fecha = Lib.Configuration.CurrentDate;
            }
        }

        public static bool ExisteArchivo(TipoArchivo tipo, string archivo)
        {
            string firma = Lib.Security.Cryptography.CalcularMD5(archivo);
            using (var db = new SMPorresEntities())
            {
                return db.CabecerasArchivos.Any(ca => ca.Firma == firma);
            }
        }
    }
}

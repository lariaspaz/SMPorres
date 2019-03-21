using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    public class RendicionBSERepository
    {
        public static void GrabarRendición(string archivo)
        {
            var db = new SMPorresEntities();
            try
            {
            }
            finally
            {
                db.Dispose();
            }
        }

        public static List<RendicionBSE> CargarRendición(string archivo)
        {
            List<RendicionBSE> result = new List<RendicionBSE>();
            var líneas = File.ReadAllLines(archivo).Skip(1);
            foreach (var línea in líneas)
            {
                var campos = línea.Split('\t');
                var rend = new RendicionBSE();
                rend.CodigoSucursal = Int32.Parse(campos[0]);
                rend.NombreSucursal = campos[1];
                rend.Moneda = campos[3];
                rend.Comprobante = campos[4];
                rend.TipoMovimiento = campos[5];
                rend.Importe = campos[6];
                rend.FechaProceso = campos[7];
                rend.CuilUsuario = campos[8];
                rend.NombreUsuario = campos[9];
                rend.Hora = campos[10];
                rend.CodigoBarra = campos[11];
                rend.GrupoTerminal = campos[12];
                rend.NroRendicion = campos[13];
                rend.FechaMovimiento = campos[14];
                result.Add(rend);
            }
            return result;
        }
    }
}

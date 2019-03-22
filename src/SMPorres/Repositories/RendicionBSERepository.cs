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
            int i = 1;
            foreach (var línea in líneas)
            {
                var campos = línea.Split('\t');
                var rend = new RendicionBSE();
                rend.Id = i++;
                rend.CodigoSucursal = Int32.Parse(campos[0]);
                rend.NombreSucursal = campos[1];
                rend.Moneda = campos[2];
                rend.Comprobante = campos[3];
                rend.TipoMovimiento = campos[4];
                rend.Importe = campos[5];
                rend.FechaProceso = campos[6];
                rend.CuilUsuario = campos[7];
                rend.NombreUsuario = campos[8];
                rend.Hora = campos[9];
                rend.CodigoBarra = campos[10];
                rend.GrupoTerminal = campos[11];
                rend.NroRendicion = campos[12];
                rend.FechaMovimiento = campos[13];
                result.Add(rend);
            }
            return result;
        }
    }
}

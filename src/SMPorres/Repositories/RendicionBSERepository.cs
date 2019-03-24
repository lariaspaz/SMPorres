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
        public static void GrabarRendición(string archivo, List<PagoBSE> pagos)
        {
            using (var db = new SMPorresEntities())
            {
                var trx = db.Database.BeginTransaction();
                try
                {
                    var ca = CabecerasArchivosRepository.Insertar(db, TipoArchivo.RendiciónBSE, archivo);
                    var id = db.RendicionesBSE.Any() ? db.RendicionesBSE.Max(t => t.Id) : 1;
                    foreach (var p in pagos)
                    {
                        var rend = new RendicionBSE();
                        rend.Id = id++;
                        rend.IdCabeceraArchivo = ca.Id;
                        rend.CodigoSucursal = p.CodigoSucursal;
                        rend.NombreSucursal = p.NombreSucursal;
                        rend.Moneda = p.Moneda;
                        rend.Comprobante = p.Comprobante;
                        rend.TipoMovimiento = p.TipoMovimiento;
                        rend.Importe = p.Importe;
                        rend.FechaProceso = p.FechaProceso;
                        rend.CuilUsuario = p.CuilUsuario;
                        rend.NombreUsuario = p.NombreUsuario;
                        rend.Hora = p.Hora;
                        rend.CodigoBarra = p.CodigoBarra;
                        rend.GrupoTerminal = p.GrupoTerminal;
                        rend.NroRendicion = p.NroRendicion;
                        rend.FechaMovimiento = p.FechaMovimiento;
                        db.RendicionesBSE.Add(rend);

                        if (p.DetallePago != null)
                        {
                            var pago = p.DetallePago;
                            pago.Fecha = rend.FechaPago;
                            pago.IdMedioPago = 2; //BSE
                            pago.IdArchivo = ca.Id;
                            pago.Descripcion = "Importado desde " + ca.NombreArchivo;
                            PagosRepository.PagarCuota(db, pago);
                        }
                    }
                    db.SaveChanges();
                    trx.Commit();
                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
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

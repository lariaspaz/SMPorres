using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class PagosBSERepository
    {
        public static IEnumerable<PagoBSE> ObtenerPagosRelacionados(List<RendicionBSE> pagosBSE)
        {
            using (var db = new SMPorresEntities())
            {
                //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var comprobantes = pagosBSE.Select(p2 => Int32.Parse(p2.Comprobante));
                //var t = db.Pagos.Where(p => comprobantes.Contains(p.Id));
                var query = (from p in db.Pagos
                             where comprobantes.Contains(p.Id)
                             select new
                             {
                                 p.Id,
                                 TipoDocumento = p.PlanPago.Alumno.TipoDocumento.Descripcion,
                                 p.PlanPago.Alumno.NroDocumento,
                                 p.PlanPago.Alumno.Nombre,
                                 p.PlanPago.Alumno.Apellido,
                                 Carrera = p.PlanPago.Curso.Carrera.Nombre,
                                 Curso = p.PlanPago.Curso.Nombre,
                                 p.ImporteCuota
                             })
                            .ToList();
                var query2 = from pbse in pagosBSE
                             join p in query on Int32.Parse(pbse.Comprobante) equals p.Id into pq
                             from pago in pq.DefaultIfEmpty()
                             select new PagoBSE
                             {
                                 Id = pago?.Id ?? 0,
                                 IdCabeceraArchivo = pbse.IdCabeceraArchivo,
                                 CodigoSucursal = pbse.CodigoSucursal,
                                 NombreSucursal = pbse.NombreSucursal,
                                 Moneda = pbse.Moneda,
                                 Comprobante = pbse.Comprobante,
                                 TipoMovimiento = pbse.TipoMovimiento,
                                 Importe = pbse.Importe,
                                 FechaProceso = pbse.FechaProceso,
                                 CuilUsuario = pbse.CuilUsuario,
                                 NombreUsuario = pbse.NombreUsuario,
                                 Hora = pbse.Hora,
                                 CodigoBarra = pbse.CodigoBarra,
                                 GrupoTerminal = pbse.GrupoTerminal,
                                 NroRendicion = pbse.NroRendicion,
                                 FechaMovimiento = pbse.FechaMovimiento,
                                 Documento = String.Format("{0} {1:##,###,##0}", pago?.TipoDocumento ?? "", pago?.NroDocumento ?? 0),
                                 Alumno = String.Format("{0} {1}", pago?.Nombre ?? "<No existe>", pago?.Apellido ?? ""),
                                 Carrera = pago?.Carrera ?? "",
                                 Curso = pago?.Curso ?? "",
                                 FechaVto = ObtenerFechaVto(pbse.CodigoBarra),
                                 FechaPago = ObtenerFechaPago(pbse.FechaMovimiento),
                                 ImportePagado = ObtenerImporte(pbse.Importe)
                             };
                return query2.ToList();
            }
        }

        private static decimal ObtenerImporte(string importe)
        {
            return Decimal.Parse(importe.Replace(".", ","));
        }

        private static DateTime ObtenerFechaPago(string fechaMovimiento)
        {
            var año = Int32.Parse(fechaMovimiento.Substring(0, 4));
            var mes = Int32.Parse(fechaMovimiento.Substring(5, 2));
            var día = Int32.Parse(fechaMovimiento.Substring(8, 2));
            return new DateTime(año, mes, día);
        }

        private static DateTime ObtenerFechaVto(string codBarra)
        {
            var año = Int32.Parse(codBarra.Substring(11, 2));
            var días = Int32.Parse(codBarra.Substring(13, 3));
            return new DateTime(2000 + año, 1, 1).AddDays(días);
        }

        public static void ValidarPagos(ref IEnumerable<PagoBSE> pagos)
        {

        }
    }
}

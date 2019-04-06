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
                var comprobantes = pagosBSE.Select(p2 => Int32.Parse(p2.Comprobante));
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
                             join p in query on pbse.IdComprobante equals p.Id into pq
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
                                 Documento = String.Format("{0} {1:N0}", pago?.TipoDocumento ?? "", pago?.NroDocumento ?? 0),
                                 Alumno = String.Format("{0} {1}", pago?.Nombre ?? "", pago?.Apellido ?? ""),
                                 Carrera = pago?.Carrera ?? "",
                                 Curso = pago?.Curso ?? ""
                             };
                return query2.ToList();
            }
        }
    }
}

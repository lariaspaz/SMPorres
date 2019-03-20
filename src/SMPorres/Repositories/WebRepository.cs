using SMPorres.ConsultasWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    public class WebRepository
    {
        public IEnumerable<Alumno> ObtenerDatos()
        {
            using (var db = new Models.SMPorresEntities())
            {
                var result = (from a in db.Alumnos select a).ToList()
                            .Select(a => new Alumno
                            {
                                Id = a.Id,
                                Nombre = a.Nombre,
                                Apellido = a.Apellido,
                                TipoDocumento = a.TipoDocumento.Descripcion,
                                NroDocumento = a.NroDocumento
                            })
                            .ToList();
                foreach (var a in result)
                {
                    Func<int, Pago[]> obtenerPagos =
                        idCurso => (from pp in db.PlanesPago
                                   join p in db.Pagos on pp.Id equals p.IdPlanPago
                                   join c in db.Cuotas on p.NroCuota equals c.NroCuota into pc
                                   from c in pc.DefaultIfEmpty()
                                   where pp.IdAlumno == a.Id && pp.IdCurso == idCurso && p.Fecha != null
                                   select new Pago
                                   {
                                       Id = p.Id,
                                       IdPlanPago = p.IdPlanPago,
                                       NroCuota = p.NroCuota,
                                       FechaVto = (c == null) ? default(DateTime) : c.VtoCuota,
                                       Fecha = p.Fecha ?? default(DateTime),
                                       ImporteCuota = p.ImporteCuota,
                                       ImporteBeca = p.ImporteBeca ?? 0,
                                       ImporteRecargo = p.ImporteRecargo ?? 0,
                                       ImportePagado = p.ImportePagado ?? 0
                                   })
                                   .ToArray();

                    a.CursosAlumnos = (from ca in db.CursosAlumnos where ca.IdAlumno == a.Id select ca).ToList()
                                        .Select(ca => new CursoAlumno
                                        {
                                            IdCurso = ca.IdCurso,
                                            Curso = ca.Curso.Nombre,
                                            IdCarrera = ca.Curso.Carrera.Id,
                                            Carrera = ca.Curso.Carrera.Nombre,
                                            Pagos = obtenerPagos(ca.IdCurso)
                                        })
                                        .ToArray();
                }
                return result;
            }
        }
    }
}

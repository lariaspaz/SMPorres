using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class CursosRepository
    {
        public static IList<Curso> ObtenerCursosPorIdCarrera(int idCarrera)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.Cursos where c.IdCarrera == idCarrera select c)
                                .ToList()
                                .Select(
                                    c => new Curso
                                    {
                                        Id = c.Id,
                                        Nombre = c.Nombre,
                                        IdCarrera = c.IdCarrera,
                                        ImporteCuota = c.ImporteCuota,
                                        ImporteMatricula = c.ImporteMatricula,
                                        DescuentoMatricula = c.DescuentoMatricula,
                                        FechaVencDescuento = c.FechaVencDescuento,
                                        Cuota1 = c.Cuota1,
                                        Cuota2 = c.Cuota2,
                                        Cuota3 = c.Cuota3,
                                        Modalidad = c.Modalidad,
                                        Estado = c.Estado
                                    });
                return query.OrderBy(c => c.Nombre).ToList();
            }
        }

        public static Curso Insertar(string nombre, int idCarrera, decimal importeMatrícula, decimal descuentoMatrícula, DateTime descuentoHasta,
             decimal cuota1, decimal cuota2, decimal cuota3, decimal importeCuota, int modalidad, short estado)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Cursos.Any() ? db.Cursos.Max(c1 => c1.Id) + 1 : 1;
                var c = new Curso
                {
                    Id = id,
                    Nombre = nombre,
                    IdCarrera = idCarrera,
                    ImporteMatricula = importeMatrícula,
                    DescuentoMatricula = descuentoMatrícula,
                    FechaVencDescuento = descuentoHasta,
                    Cuota1 = cuota1,
                    Cuota2 = cuota2,
                    Cuota3 = cuota3,
                    ImporteCuota = importeCuota,
                    Modalidad = modalidad,
                    Estado = (short)EstadoCurso.Activo //alta = estado activo
                };
                db.Cursos.Add(c);
                db.SaveChanges();
                return c;
            }
        }

        public static void Actualizar(int id, string nombre, int idCarrera, decimal importeMatrícula, decimal descuentoMatrícula, DateTime descuentoHasta,
             decimal cuota1, decimal cuota2, decimal cuota3, decimal importeCuota, int modalidad, short estado)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Cursos.Any(t => t.Id == id))
                {
                    throw new Exception(String.Format("No existe el curso {0} - {1}", id, nombre));
                }
                var c = db.Cursos.Find(id);
                c.Nombre = nombre;
                c.IdCarrera = idCarrera;
                //c.Estado = estado;    
                //c.Modalidad = modalidad;  //la baja desde listado / botón eliminar
                var trx = db.Database.BeginTransaction();
                try
                {
                    if (c.DescuentoMatricula != descuentoMatrícula)
                    {
                        c.DescuentoMatricula = descuentoMatrícula;
                    }
                    if (c.FechaVencDescuento != descuentoHasta)
                    {
                        c.FechaVencDescuento = descuentoHasta;
                    }
                    if (c.Cuota1 != cuota1)
                    {
                        c.Cuota1 = cuota1;
                    }
                    if (c.Cuota2 != cuota2)
                    {
                        c.Cuota2 = cuota2;
                    }
                    if (c.Cuota3 != cuota3)
                    {
                        c.Cuota3 = cuota3;
                    }
                    if (c.Modalidad != modalidad)
                    {
                        c.Modalidad = modalidad;
                    }
                    if (c.ImporteMatricula != importeMatrícula)
                    {
                        c.ImporteMatricula = importeMatrícula;
                        PagosRepository.ActualizarCuotas(id, importeMatrícula, true);
                    }
                    if (c.ImporteCuota != importeCuota)
                    {
                        c.ImporteCuota = importeCuota;
                        PagosRepository.ActualizarCuotas(id, importeCuota, false);
                        PlanesPagoRepository.Actualizar(id, importeCuota);
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

        public static void Eliminar(int id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Cursos.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el curso con Id " + id);
                }
                var c = db.Cursos.Find(id);
                if (c.CursosAlumnos.Any())
                {
                    throw new Exception("No puede eliminar el curso porque tiene alumnos relacionados.");
                }
                c.Estado = (short)EstadoCurso.Baja;
                //db.Cursos.Remove(c);
                db.SaveChanges();
            }
        }

        internal static Curso ObtenerCursoPorId(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Cursos.Find(id);
            }
        }

        internal static Curso ObtenerCursoPorNombre(string curso)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Cursos.Where(x => x.Nombre == curso).FirstOrDefault();
            }
        }

        internal static short ObtieneMaxCuota(int? modalidad)
        {
            short maxCuota = 0;
            //Anual, Primer cuatrimestre, Segundo cuatrimestre y Sin cursado
            if (modalidad == 1) maxCuota = 9;   // "Anual";
            if (modalidad == 2) maxCuota = 4;   // "Primer cuatrimestre";
            if (modalidad == 3) maxCuota = 9;   // "Segundo cuatrimestre";
            if (modalidad == 4) maxCuota = 0;   // "Sin cursado";
            return maxCuota;
        }

        internal static short ObtieneMinCuota(int? modalidad)
        {
            short minCuota = 0;
            //Anual, Primer cuatrimestre, Segundo cuatrimestre y Sin cursado
            if (modalidad == 1) minCuota = 1;   // "Anual";
            if (modalidad == 2) minCuota = 1;   // "Primer cuatrimestre";
            if (modalidad == 3) minCuota = 5;   // "Segundo cuatrimestre";
            if (modalidad == 4) minCuota = 0;   // "Sin cursado";
            return minCuota;
        }

        public static string ObtenerModalidadCurso(int curso)
        {
            using (var db = new SMPorresEntities())
            {
                var c = db.Cursos.Where(x => x.Id == curso).FirstOrDefault();
                string modalidad = ModalidadCurso(Convert.ToInt16(c.Modalidad));
                return modalidad;
            }
        }

        internal static string ModalidadCurso(int curso)
        {
            //Anual, Primer cuatrimestre, Segundo cuatrimestre y Sin cursado
            if (curso == 1) return "Anual";
            if (curso == 2) return "Primer cuatrimestre";
            if (curso == 3) return "Segundo cuatrimestre";
            if (curso == 4) return "Sin cursado";
            return "";
        }
    }
}

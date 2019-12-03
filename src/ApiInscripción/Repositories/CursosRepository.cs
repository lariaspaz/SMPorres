using ApiInscripción.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInscripción.Repositories
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

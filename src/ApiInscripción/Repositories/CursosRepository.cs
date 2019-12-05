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
        public static int ObtenerCursoInicial(int idCarrera)
        {
            using (var db = new SMPorresEntities())
            {
                var idCurso = (from c in db.Cursos
                               where c.IdCarrera == idCarrera && c.Estado == (short)EstadoCurso.Activo
                               orderby c.Id
                               select c.Id).First();
                return idCurso;
            }
        }

        internal static Curso ObtenerCursoPorId(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Cursos.Find(id);
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
    }
}

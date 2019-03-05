using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    public class StoredProcs
    {
        public static List<ConsAlumnosMorosos_Result> ConsAlumnosMorosos(DateTime fecha, short tipo,
            int idCarrera, int idCurso)
        {
            using (var db = new SMPorresEntities())
            {                
                return db.ConsAlumnosMorosos(fecha, tipo, idCarrera, idCurso).ToList();
            }
        }

        public static List<ConsAlumnosMorosos_Result> ConsAlumnosMorosos2(DateTime fecha, short tipo,
            int idCarrera, int idCurso, short beca)
        {
            using (var db = new SMPorresEntities())
            {
                //return db.ConsAlumnosMorosos2(fecha, tipo, idCarrera, idCurso, beca).ToList();
                return db.ConsAlumnosMorosos(fecha, tipo, idCarrera, idCurso).ToList();
            }
        }
    }
}

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
        public static List<ConsAlumnosMorosos_Result> ConsAlumnosMorosos(DateTime fecha, short tipo, int idCurso)
        {
            using (var db = new SMPorresEntities())
            {                
                return db.ConsAlumnosMorosos(fecha, tipo, idCurso).ToList();
            }
        }
    }
}

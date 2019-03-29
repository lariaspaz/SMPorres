using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    static class BecasAlumnosRepository
    {
        public static BecaAlumno Insertar(int idAlumno, int idPago, short beca)
        {
            using (var db = new SMPorresEntities())
            {
                var trx = db.Database.BeginTransaction();
                try
                {
                    var id = db.BecasAlumnos.Any() ? db.BecasAlumnos.Max(ba => ba.Id) + 1 : 1;
                    var b = new BecaAlumno
                    {
                        Id = id,
                        IdAlumno = idAlumno,
                        PorcentajeBeca = beca
                    };
                    db.BecasAlumnos.Add(b);
                    var p = db.Pagos.Find(idPago);
                    p.BecaAlumno = b;
                    db.SaveChanges();
                    trx.Commit();
                    return b;
                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }
        }

        internal static BecaAlumno Actualizar(int id, short beca)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.BecasAlumnos.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la beca con Id " + id);
                }
                var b = db.BecasAlumnos.Find(id);
                b.PorcentajeBeca = beca;
                db.SaveChanges();
                return b;
            }
        }
    }
}

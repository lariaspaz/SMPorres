using Consultas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Repositories
{
    public class TasasMoraRepository
    {
        public void Actualizar(List<Models.WebServices.TasaMora> tasasMora)
        {
            using (var db = new SMPorresEntities())
            {
                using (var trx = db.Database.BeginTransaction())
                    try
                    {
                        db.TasasMoraWebs.RemoveRange(db.TasasMoraWebs);
                        for (int i = 0; i < tasasMora.Count; i++)
                        {
                            var item = tasasMora.ElementAt(i);
                            var t = new TasasMoraWeb
                            {
                                Id = i + 1,
                                Desde = item.Desde,
                                Hasta = item.Hasta,
                                Tasa = item.Tasa,
                                Estado = item.Estado
                            };
                            db.TasasMoraWebs.Add(t);
                        }
                        db.SaveChanges();
                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        trx.Rollback();
                        throw ex;
                    }

            }
        }
    }
}
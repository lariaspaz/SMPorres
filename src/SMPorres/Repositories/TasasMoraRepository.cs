using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    public class TasasMoraRepository
    {
        public static IList<TasaMora> ObtenerTasas()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from t in db.TasasMora select t)
                                .ToList()
                                .Select(
                                    t => new TasaMora
                                    {
                                        Id = t.Id,
                                        Tasa = t.Tasa,
                                        Desde = t.Desde,
                                        Hasta = t.Hasta,
                                        Estado = t.Estado
                                    });
                return (from t in query
                        orderby t.Desde ascending, t.Hasta descending, t.Tasa ascending
                        select t).ToList();
            }
        }

        public static TasaMora Insertar(double tasa, DateTime desde, DateTime hasta)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.TasasMora.Any() ? db.TasasMora.Max(t1 => t1.Id) + 1 : 1;
                var t = new TasaMora
                {
                    Id = id,
                    Tasa = tasa,
                    Desde = desde,
                    Hasta = hasta,
                    Estado = (byte)EstadoTasaMora.Activa
                };
                db.TasasMora.Add(t);
                db.SaveChanges();
                return t;
            }
        }

        public static void Actualizar(decimal id, double tasa, DateTime desde, DateTime hasta, short estado)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.TasasMora.Any(t1 => t1.Id == id))
                {
                    throw new Exception("No existe la tasa con Id " + id);
                }
                var t = db.TasasMora.Find(id);
                t.Tasa = tasa;
                t.Desde = desde;
                t.Hasta = hasta;
                t.Estado = estado;
                db.SaveChanges();
            }
        }

        public static void Eliminar(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.TasasMora.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la tasa con Id " + id);
                }
                var t1 = db.TasasMora.Find(id);
                db.TasasMora.Remove(t1);
                db.SaveChanges();
            }
        }

        internal static TasaMora ObtenerTasaPorId(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.TasasMora.Find(id);
            }
        }

        public enum ValidarTasasResult
        {
            //Ok
            Ok,

            //No hay un rango para el año 2019
            NoHayRangoPara2019,

            //No hay rango para la fecha de hoy
            NoHayRangoParaHoy,

            //Hay rangos no definidos
            HayRangosNoDefinidos
        }

        public static ValidarTasasResult ValidarTasas()
        {
            using (var db = new SMPorresEntities())
            {
                //var tasas = db.TasasMora
                //                .Where(t => t.Estado == (short)EstadoTasaMora.Activa)
                //                .Except(
                //                    from t1 in db.TasasMora
                //                    join t2 in db.TasasMora on t1.Desde equals
                //                        System.Data.Entity.DbFunctions.AddDays(t2.Hasta, 1)
                //                    select t1
                //                );
                ////solamente no puede tener antecesor el primer rango
                //if (tasas.Count() == 1)
                //{
                //    var hoy = Lib.Configuration.CurrentDate;
                //    return tasas.Any(t => t.Desde <= hoy && hoy <= t.Hasta);
                //}
                //else
                //{
                //    return false;
                //}
                //DateTime.Today


                var tasas = from t in db.TasasMora
                            where t.Estado == (short) EstadoTasaMora.Activa
                            select new
                            {
                                t.Id,
                                t.Desde,
                                t.Hasta,
                                TieneSiguiente = db.TasasMora.Any(
                                    t2 =>
                                        t2.Estado == 1 &&
                                        t2.Id != t.Id &&
                                        t2.Desde == System.Data.Entity.DbFunctions.AddDays(t.Hasta, 1)
                                        )
                            };
                if (tasas.Count(t => !t.TieneSiguiente) == 1)
                    if (tasas.Any(t => t.Desde <= DateTime.Today && DateTime.Today <= t.Hasta))
                        if (tasas.OrderBy(t => t.Desde).First().Desde > new DateTime(2019, 4, 1))
                            //Console.WriteLine("No hay un rango para el año 2019");
                            return ValidarTasasResult.NoHayRangoPara2019;
                        else
                            //Console.WriteLine("Ok");
                            return ValidarTasasResult.Ok;
                    else
                        //Console.WriteLine("No hay rango para la fecha de hoy");
                        return ValidarTasasResult.NoHayRangoParaHoy;
                else
                    //Console.WriteLine("Hay rangos no definidos.");
                    return ValidarTasasResult.HayRangosNoDefinidos;
            }
        }
    }
}

using NotificacionesDeuda.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificacionesDeuda.Repositories
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
                            return ValidarTasasResult.NoHayRangoPara2019;
                        else
                            return ValidarTasasResult.Ok;
                    else
                        return ValidarTasasResult.NoHayRangoParaHoy;
                else
                    return ValidarTasasResult.HayRangosNoDefinidos;
            }
        }
    }
}

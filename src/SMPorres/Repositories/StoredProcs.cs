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
            int idCarrera, int idCurso, short tipoBecado)
        {
            using (var db = new SMPorresEntities())
            {                
                return db.ConsAlumnosMorosos(fecha, tipo, idCarrera, idCurso, tipoBecado).ToList();
            }
        }

        public static List<ConsTotalPagos_Result> ConsTotalPagos(DateTime desde, DateTime hasta,
            int idCarrera, int idCurso, int IdMedioPago)
        {
            using (var db = new SMPorresEntities())
            {
                return db.ConsTotalPagos(desde, hasta, idCarrera, idCurso, IdMedioPago).ToList();
            }
        }

        public static List<ConsInformeEconomico_Result> ConsInformeEconómico(short CicloLectivo)
        {
            using (var db = new SMPorresEntities())
            {
                return db.ConsInformeEconomico(CicloLectivo).ToList();
            }
        }

        public static List<ConsInformeFinanciero_Result> ConsInformeFinanciero(DateTime desde, DateTime hasta)
        {
            using (var db = new SMPorresEntities())
            {
                return db.ConsInformeFinanciero(desde, hasta).ToList();
            }
        }
    }
}

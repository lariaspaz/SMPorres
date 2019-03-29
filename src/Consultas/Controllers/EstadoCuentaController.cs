using Consultas.Models.ViewModels;
using Consultas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consultas.Controllers
{
    public class EstadoCuentaController : Controller
    {
        // GET: EstadoCuenta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MostrarCursosAlumno()
        {
            return PartialView(new CursoAlumnoRepository().ObtenerCursosAlumno());
        }

        public ActionResult Details(int id)
        {
            var result = new DetallePago();
            var pagos = new PagosRepository().ObtenerPagos(id);
            var ca = new CursoAlumnoRepository().ObtenerCursoAlumnoPorId(id);
            result.Curso = String.Format("{0} de {1}", ca.Curso, ca.Carrera);
            result.Pagos = pagos;
            result.PróximaCuota = pagos.FirstOrDefault(p => p.Fecha == null);
            if (result.PróximaCuota != null)
            {
                result.PróximaCuota.Vencido = result.PróximaCuota.FechaVto < Lib.Configuration.CurrentDate;
            }
            return PartialView(result);
        }
    }
}
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

        public ActionResult MostrarCarreras()
        {
            var carreras = from c in new CursoAlumnoRepository().ObtenerCarreras()
                           select new SelectListItem { Text = c.Descripción, Value = c.Id.ToString() };
            return PartialView(carreras);
        }

        public ActionResult MostrarCursos(int idCarrera)
        {
            var cursos = from c in new CursoAlumnoRepository().ObtenerCursos(idCarrera)
                           select new SelectListItem { Text = c.Descripción, Value = c.Id.ToString() };
            return PartialView(cursos);
        }

        public ActionResult MostrarCursosAlumno()
        {            
            return PartialView(new CursoAlumnoRepository().ObtenerCursosAlumno());
        }

        public ActionResult Consultar()
        {
            return PartialView();
        }

        public ActionResult Details(int id)
        {
            return PartialView(new PagosRepository().ObtenerPagos(id));
        }
    }
}
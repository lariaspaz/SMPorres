using Consultas.CustomAuthentication;
using Consultas.Models.ViewModels;
using Consultas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Consultas.Models;

namespace Consultas.Controllers
{
    [CustomAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MostrarCursosAlumno()
        {
            return PartialView(new CursosAlumnosRepository().ObtenerCursosAlumno());
        }

        public ActionResult Details(int id)
        {
            var ca = new CursosAlumnosRepository().ObtenerCursoAlumnoPorId(id);
            var pagos = new PagosRepository().ObtenerPagos(id);
            var result = new DetallePago();
            var prox = pagos.FirstOrDefault(p => p.Fecha == null);
            result.Curso = String.Format("{0} de {1}", ca.Curso, ca.Carrera);
            result.Pagos = pagos;
            result.PróximaCuota = prox;
            result.Id = id;            
            ViewBag.Cuotas = new object();
            if (prox != null)
            {
                prox.Vencido = result.PróximaCuota.FechaVto < Lib.Configuration.CurrentDate.Date;
                prox.AplicaBeca = (prox.PorcentajeBeca > 0 && prox.TipoBeca == (byte)TipoBeca.AplicaSiempre) ||
                                 (prox.PorcentajeBeca > 0 && !prox.Vencido);
                prox.PagaATérmino = prox.ImportePagoTermino > 0 && !result.PróximaCuota.Vencido;
            }
            var cuotas = from p in pagos
                         where p.Fecha == null
                         select p.NroCuota;
            ViewBag.Cuotas = new SelectList(cuotas);
            return PartialView(result);
        }

        private string ObtenerDetalle(PagoWeb item)
        {
            string detalle = "";
            if (item.NroCuota == 0)
            {
                PagosRepository.ObtenerConceptoMatrícula(item.IdPlanPago, item);
            }
            else
            {
                detalle = String.Format("Matrícula Cuota Nº {0}", item.NroCuota);
            }
            return detalle;
        }

        public ActionResult ImprimirCupon(int cuota, DateTime fechaCompromiso)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            //var id = new PagosRepository().ObtenerIdPrimeraCuotaImpaga();
            var id = new PagosRepository().ObtenerIdCuota(cuota);
            if (id > 0)
            {
                var repo = new CuponPagoRepository();
                using (var dt = repo.ObtenerDatos(false, id, fechaCompromiso))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        using (var reporte = new Reports.CupónDePago())
                        {
                            reporte.Database.Tables["CupónPago"].SetDataSource(dt);
                            reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,
                                System.Web.HttpContext.Current.Response, false, "Cupon-de-pago");
                            return new EmptyResult();
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ImprimirPermisoExamen(int id, DateTime? vto)
        {
            var repo = new PermisoExámenRepository();
            var p = repo.CargarPermisoExámen(id, vto);
            if (p != null)
            {
                using (var dt = repo.ObtenerDatos(p))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        using (var reporte = new Reports.PermisoExámen())
                        {
                            reporte.Database.Tables["PermisoExámen"].SetDataSource(dt);
                            reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,
                                System.Web.HttpContext.Current.Response, false, "Permiso-de-exámen");
                            return new EmptyResult();
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
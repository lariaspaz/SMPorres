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
            var result = new DetallePago();
            var pagos = new PagosRepository().ObtenerPagos(id);
            var ca = new CursosAlumnosRepository().ObtenerCursoAlumnoPorId(id);
            result.Curso = String.Format("{0} de {1}", ca.Curso, ca.Carrera);
            result.Pagos = pagos;
            result.PróximaCuota = pagos.FirstOrDefault(p => p.Fecha == null) ?? null; //error Plan de pago c/ todas cuotas canceladas
            var permisoExámen = new PermisoExámenRepository().CargarPermisoExámen(id, result.PróximaCuota.FechaVto);
            result.DatosPermiso = permisoExámen;
            ViewBag.Cuotas = new object();
            if (result.PróximaCuota != null)
            {
                result.PróximaCuota.Vencido = result.PróximaCuota.FechaVto < Lib.Configuration.CurrentDate.Date;
                //ViewBag.Cuotas = new SelectList(Enumerable.Range(result.PróximaCuota.NroCuota, 9 - result.PróximaCuota.NroCuota + 1));
                ViewBag.Cuotas = new SelectList(Enumerable.Range(result.PróximaCuota.NroCuota, result.Pagos.Max(x=> x.NroCuota) + 1));   
            }
            else
            {
                ViewBag.Cuotas = new SelectList(Enumerable.Range(1, 9));
            }
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
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult ImprimirPermisoExamen(PermisoExámen permisoExámen)
        {
            //Response.ClearContent();
            //Response.ClearHeaders();

            //var pagos = new PagosRepository().ObtenerPagos(idCurso);
            //var próximaCuota = pagos.FirstOrDefault(p => p.Fecha == null) ?? null; //error Plan de pago c/ todas cuotas canceladas
            //var permisoExámen = new PermisoExámenRepository().CargarPermisoExámen(idCurso, próximaCuota.FechaVto);
            
            if (permisoExámen != null)
            {
                var repo = new PermisoExámenRepository();
                using (var dt = repo.ObtenerDatos(permisoExámen))
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
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }
    }
}
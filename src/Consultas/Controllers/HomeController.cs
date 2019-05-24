﻿using Consultas.CustomAuthentication;
using Consultas.Models.ViewModels;
using Consultas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            result.PróximaCuota = pagos.FirstOrDefault(p => p.Fecha == null);
            ViewBag.Cuotas = new object();
            if (result.PróximaCuota != null)
            {
                result.PróximaCuota.Vencido = result.PróximaCuota.FechaVto < Lib.Configuration.CurrentDate.Date;
                ViewBag.Cuotas = new SelectList(Enumerable.Range(result.PróximaCuota.NroCuota, 9 - result.PróximaCuota.NroCuota + 1));
            }
            else
            {
                ViewBag.Cuotas = new SelectList(Enumerable.Range(1, 9));
            }
            return PartialView(result);
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
   }
}
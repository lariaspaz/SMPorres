using Consultas.CustomAuthentication;
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
    public class EstadoCuentaController : Controller
    {
        // GET: EstadoCuenta
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
            if (result.PróximaCuota != null)
            {
                result.PróximaCuota.Vencido = result.PróximaCuota.FechaVto < Lib.Configuration.CurrentDate;
            }
            return PartialView(result);
        }

        public ActionResult ImprimirCupon(DateTime fechaCompromiso)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            var id = new PagosRepository().ObtenerIdPrimeraCuotaImpaga();
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

                            //using (var f = new frmReporte(reporte, txtDescripción.Text)) f.ShowDialog();

                            //System.Data.DataTable tblOrderDetail = new System.Data.DataTable("OrderDetail");
                            //ReportDocument doc = new ReportDocument();
                            
                            //CrystalReport1 objReport = new CrystalReport1();
                            //objReport.SetDataSource(tblOrderDetail);

                            //CrystalReportViewer1.RefreshReport();

                        }
                    }
                    else
                    {
                        //ShowError("No hay datos para mostrar.");
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }

        private void Test()
        {
            //System.Data.DataTable tblOrderDetail = new System.Data.DataTable("OrderDetail");
            //CrystalReport1 objReport = new CrystalReport1();
            //objReport.SetDataSource(tblOrderDetail);

            //ReportDocument doc = new ReportDocument();

            //CrystalReportViewer1.ReportSource = objReport;
            //CrystalReportViewer1.RefreshReport();

            ////file name 'Test PDF.pdf' can be anything, you may also use GUID or something similar to get unique file name
            ////objReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Page.Response, true, "Test PDF.pdf");
            //objReport.ExportToDisk(ExportFormatType.PortableDocFormat, "C:\\Data\\Testy.pdf"); // file path is just for demo
        }
    }
}
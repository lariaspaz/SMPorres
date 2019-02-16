using SMPorres.Lib.AppForms;
using SMPorres.Models;
using SMPorres.Reports.Designs;
using SMPorres.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPorres.Forms.Pagos
{
    public partial class frmInfCupónDePago : FormBase
    {
        private int _idPago;

        private enum TipoImpresión
        {
            Matrícula,
            Cuota
        }

        public frmInfCupónDePago(int idPago)
        {
            InitializeComponent();

            _idPago = idPago;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            using (var dt = ObtenerDatos())
            {
                if (dt.Rows.Count > 0)
                {
                    MostrarReporte(dt);
                }
                else
                {
                    ShowError("No hay datos para mostrar.");
                }
            }
        }

        private static void MostrarReporte(DataTable dt)
        {
            using (var reporte = new CupónDePago())
            {
                reporte.Database.Tables["CupónPago"].SetDataSource(dt);
                using (var f = new frmReporte("", reporte)) f.ShowDialog();
            }
        }

        private DataTable ObtenerDatos()
        {
            //var dt = CrearDataTable();
            //var alumnos = AlumnosRepository.ObtenerAlumnosPorEstado(IdCarrera, IdCurso, Estado);
            //foreach (var a in alumnos)
            //{
            //    dt.Rows.Add(String.Format("{0} de {1}", a.Curso, a.Carrera), a.Nombre, a.Apellido, a.EMail, a.LeyendaEstado());
            //}
            //return dt;

            var cupón = new Reports.DataSet.dsImpresiones.CupónPagoDataTable();
            var row = cupón.NewCupónPagoRow();
            var idPago = String.Format("{0:0000000}", _idPago);
            var fechaEmisión = String.Format("{0:dd/MM/yyyy}", Lib.Configuration.CurrentDate);
            var fechaVencimiento = dtFechaPago.Text;
            var pago = PagosRepository.ObtenerPago(_idPago);
            var nombre = String.Format("{0} {1}", pago.PlanPago.Alumno.Nombre, pago.PlanPago.Alumno.Apellido);
            var tipoDocumento = pago.PlanPago.Alumno.TipoDocumento.Descripcion;
            var documento = pago.PlanPago.Alumno.NroDocumento.ToString("N0");
            var curso = pago.PlanPago.Curso.Nombre;
            var carrera = pago.PlanPago.Curso.Carrera.Nombre;
            var importe = String.Format("{0:C2}", pago.ImporteCuota);
            var total = String.Format("{0:C2}", pago.ImporteCuota);
            var total_codbar = pago.ImporteCuota;
            var fechaVtoJuliano = String.Format("{0:yy}{1:000}", dtFechaPago.Value, (dtFechaPago.Value - new DateTime(dtFechaPago.Value.Year, 1, 1)).TotalDays);
            var importe_codbar = Math.Truncate(total_codbar).ToString("00000000") + ((total_codbar - Math.Truncate(total_codbar)) * 100).ToString("00");
            var códigoBarra = idPago + fechaVtoJuliano + importe_codbar;
            códigoBarra += Lib.Calculos.DígitoVerificador.CalculaDigitoVerificador(códigoBarra, "10");
            if (pago.NroCuota > 0)
            {

            }
            else
            {
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento, 
                    carrera, curso, total, códigoBarra, "1", "Matrícula", importe);

                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, total, códigoBarra, "1", "Recargo", importe);
            }
            return cupón;
        }

        private DataTable CrearDataTable()
        {
            var dt = new Reports.DataSet.dsImpresiones.CupónPagoDataTable();
            var rowCupónPagoRow = (Reports.DataSet.dsImpresiones.CupónPagoRow)(dt.NewRow());
            dt.Rows.Add(rowCupónPagoRow);
            return dt;
        }
    }
}

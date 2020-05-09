using SMPorres.Lib.AppForms;
using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMPorres.Repositories;
using System.Globalization;
using SMPorres.Reports.Designs;
using SMPorres.Reports.DataSet;

namespace SMPorres.Forms.Alumnos
{
    public partial class frmPermisoDeExámen : FormBase
    {
        string _situación = "";
        int _cuotasimpagas = 0;
        string _cuotas = "";
        string _fecha = "";
        string _alumno = "";
        string _cursoyCarrera = "";
        string _proxVencimiento = "";

        public frmPermisoDeExámen(Alumno alumno, string cursoSeleccionado)
        {
            InitializeComponent();
            cargarDatos(alumno, cursoSeleccionado);
        }

        private void cargarDatos(Alumno alumno, string cursoyCarrera)
        {
            obtenerDatos(alumno, cursoyCarrera);   // debe buscar situación del alumno (Al día, )

            txtTítulo.Text = _situación;
            txtLínea1.Text = _fecha + _alumno + _cursoyCarrera + _cuotas;
            txtLínea2.Text = _proxVencimiento;
        }

        private void obtenerDatos(Alumno alumno, string cursoyCarrera)
        {

            _cuotasimpagas = CuotasRepository.CuotasVencidasImpagas(alumno);

            _situación = cargarSituación(_cuotasimpagas);

            _fecha = cargarFecha(DateTime.Today);

            _alumno = cargarAlumno(alumno);

            _cursoyCarrera = cursoyCarrera;

            _cuotas = cargarCuotasImpagas(alumno, _cuotasimpagas);

            _proxVencimiento = cargarPróxVencimiento();
        }

        private string cargarPróxVencimiento()
        {
            string pv = "";
            if (CuotasRepository.PróximaCuota != null)
            {
                pv += "El próximo vencimiento de cuota es el día " +
                    CuotasRepository.PróximaCuota.VtoCuota.ToShortDateString().ToString() + ".";
            }
            else
            {
                pv += "No tiene cuotas próximas a vencer.";
            }

            return pv;
        }

        private string cargarCuotasImpagas(Alumno alumno, int _cuotasimpagas)
        {
            string c = "";
            if (_cuotasimpagas <= 0) c = ", no registra cuotas adeudadas.";
            if (_cuotasimpagas > 0)
            {
                c = ", se encuentra deudor"; 
                if (alumno.Sexo == "F") c += "a";
                c += " en las cuotas ";

                foreach (var item in CuotasRepository.CuotasImpagas(alumno))
                {
                    c += item + ", ";
                }
                c += "de su plan de pagos.";
            }
            return c;
        }

        private string cargarAlumno(Alumno alumno)
        {
            bool masculino = true;
            if (alumno.Sexo == "F") masculino = false;

            string al = ", se certifica que ";
            if (masculino)
            {
                al += "el alumno ";
            }
            else
            {
                al += "la alumna ";
            }

            if (!String.IsNullOrEmpty(alumno.Apellido.Trim())) al += alumno.Apellido + " ,";
            if (!String.IsNullOrEmpty(alumno.Nombre.Trim())) al += alumno.Nombre;

            if (masculino)
            {
                al += " inscripto en ";
            }
            else
            {
                al += " inscripta en ";
            }

            return al;
        }

        private string cargarSituación(int _cuotasimpagas)
        {
            string situacion = "Alumno al día";
            if (_cuotasimpagas > 0) situacion = "Alumno moroso";
            return situacion;
        }

        private string cargarFecha(DateTime today)
        {
            DateTime hoy = DateTime.Today;
            string día = hoy.Day.ToString();
            string mes = obtenerNombreMesNumero(hoy.Month);
            string año = hoy.Year.ToString();
            string fecha = "";
            if (hoy.Day > 1)
            {
                fecha = "A los " + día + " días del mes de " + mes + " de " + año;
            }
            else
            {
                fecha = "El " + día + " del mes de " + mes + " de " + año;
            }
            return fecha;
        }

        private string obtenerNombreMesNumero(int numeroMes)
        {
            try
            {
                DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                string nombreMes = formatoFecha.GetMonthName(numeroMes);
                return nombreMes;
            }
            catch
            {
                return "Desconocido";
            }
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
                    ShowError("No hay ningún registro que coincida con su consulta.");
                }
            }
        }

        private void MostrarReporte(DataTable dt)
        {
            using (var reporte = new InscripciónExamen())
            {
                string título = "";
                if (_cuotasimpagas <= 0) título = "Solicitud de Inscripción";
                if (_cuotasimpagas > 0) título = "Solicitud de Inscripción - Con Excepción";
                reporte.Database.Tables["PermisoExamen"].SetDataSource(dt);
                using (var f = new frmReporte(reporte, título)) f.ShowDialog();
            }
        }

        private DataTable ObtenerDatos()
        {
            string l1 = txtLínea1.Text.Trim();
            string l2 = txtLínea2.Text.Trim();
            var tabla = new dsImpresiones.PermisoExamenDataTable();
            tabla.AddPermisoExamenRow(l1, l2);
            return tabla;
        }
    }
}

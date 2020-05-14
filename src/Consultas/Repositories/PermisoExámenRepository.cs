using Consultas.Models;
using Consultas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Consultas.Repositories
{
    public class PermisoExámenRepository
    {
        public PermisoExámen CargarPermisoExámen(int idCursoAlumno, DateTime próximoVencimiento)
        {
            using (var db = new SMPorresEntities())
            {
                var ca = new CursosAlumnosRepository().ObtenerCursoAlumnoPorId(idCursoAlumno);
                var alumno = db.AlumnosWeb.Find(ca.IdAlumnoWeb);
                var pe = new PermisoExámen();

                pe.Carrera = ca.Carrera;
                pe.Curso = ca.Curso;
                pe.AlumnoApellido = alumno.Apellido;
                pe.AlumnoNombre = alumno.Nombre;
                pe.PróximaCuota = próximoVencimiento;

                return pe;
            }
        }

        public DataTable ObtenerDatos(PermisoExámen permiso)
        {
            var p = new dsConsultas.PermisoExámenDataTable();
            //txtTítulo.Text = _situación;
            //txtLínea1.Text = _fecha + _alumno + _cursoyCarrera + _cuotas;
            //txtLínea2.Text = _proxVencimiento;
            var fecha = CargarFecha(Consultas.Lib.Configuration.CurrentDate.Date);
            var alumno = CargarAlumno(permiso.AlumnoApellido, permiso.AlumnoNombre);
            var cursoyCarrera = permiso.Curso + " de " + permiso.Carrera;
            var cuotas = ", no registra cuotas adeudadas.";
            var txtL1 = fecha + alumno + cursoyCarrera + cuotas;

            var txtL2 = CargarPróxVencimiento(permiso.PróximaCuota);

            return GenerarPermisoExámen(p, txtL1, txtL2);
        }

        private DataTable GenerarPermisoExámen(dsConsultas.PermisoExámenDataTable p, string txtL1, string txtL2)
        {
            p.AddPermisoExámenRow(txtL1, txtL2);
            return p;
        }

        private string CargarPróxVencimiento(DateTime vencimiento)
        {
            string pv = "";
            if (vencimiento != null)
            {
                pv += "El próximo vencimiento de cuota es el día " +
                    vencimiento.ToString() + ".";
            }
            else
            {
                pv += "No tiene cuotas próximas a vencer.";
            }

            return pv;
        }

        private string CargarAlumno(string apellido, string nombre)
        {
            string al = ", se certifica que ";
            if (!String.IsNullOrEmpty(apellido.Trim())) al += apellido + " ,";
            if (!String.IsNullOrEmpty(nombre.Trim())) al += nombre;
            al += " inscripto en ";
            return al;
        }

        private string CargarFecha(DateTime today)
        {
            DateTime hoy = DateTime.Today;
            string día = hoy.Day.ToString();
            string mes = ObtenerNombreMesNumero(hoy.Month);
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

        private string ObtenerNombreMesNumero(int numeroMes)
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
    }
}
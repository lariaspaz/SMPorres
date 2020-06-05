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
        public PermisoExámen CargarPermisoExámen(int idCursoAlumno, DateTime? próximoVencimiento)
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
                pe.PróximoVencimiento = próximoVencimiento;

                return pe;
            }
        }

        public DataTable ObtenerDatos(PermisoExámen permiso)
        {
            var p = new dsConsultas.PermisoExámenDataTable();
            var fecha = ObtenerFecha(Lib.Configuration.CurrentDate.Date);
            var alumno = ObtenerFAlumno(permiso.AlumnoApellido, permiso.AlumnoNombre);
            var cursoyCarrera = $"{permiso.Curso} de {permiso.Carrera}";
            var cuotas = ", no registra cuotas adeudadas.";
            var línea1 = fecha + alumno + cursoyCarrera + cuotas;

            var línea2 = "No tiene cuotas próximas a vencer.";
            if (permiso.PróximoVencimiento.HasValue)
                línea2 = String.Format("El próximo vencimiento de cuota es el día {0:dd/MM/yyyy}.", 
                    permiso.PróximoVencimiento);

            p.AddPermisoExámenRow(línea1, línea2);
            return p;
        }

        private string ObtenerFAlumno(string apellido, string nombre)
        {
            string al = ", se certifica que ";
            if (!String.IsNullOrEmpty(apellido)) al += apellido + " ,";
            if (!String.IsNullOrEmpty(nombre)) al += nombre;
            al += " inscripto en ";
            return al;
        }

        private string ObtenerFecha(DateTime today)
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
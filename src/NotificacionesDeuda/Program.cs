using NotificacionesDeuda.Controller;
using NotificacionesDeuda.Models;
using NotificacionesDeuda.Repositories;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesDeuda
{
    class Program
    {
        static void Main(string[] args)
        {
            NotificacionesDeuda();
        }

        public static void NotificacionesDeuda()
        {
            IList<Carreras> carreras = CarrerasRepository.ObtenerCarreras();
            foreach (var item in carreras)
            {
                var morososPorCarrera = AlumnosMorososRepository.ObtenerAlumnosMorosos(item.Id);
                ProcesarEMailMorosos(morososPorCarrera);
            }
        }

        public static void ProcesarEMailMorosos(IEnumerable<AlumnoMoroso> morosos)
        {
            var dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var template = System.IO.File.ReadAllText($@"{dir}\Plantillas\default.cshtml");

            foreach (var item in morosos)
            {
                Moroso moroso = CargarMoroso(item);
                var result = Engine.Razor.RunCompile(template, moroso.Id.ToString(), typeof(Moroso), moroso, null);
                EmailController.EnviarMail("Notificación de deuda - " + DateTime.Now, result, true, item.EMail);
            }
        }

        public static Moroso CargarMoroso(AlumnoMoroso item)
        {
            DateTime hoy = DateTime.Now;
            Moroso AlumnoMoroso = new Moroso();
            AlumnoMoroso.Id = Convert.ToInt32(String.Format("{0:yy}{1:000}", hoy, hoy.DayOfYear)) + item.IdPago;
            AlumnoMoroso.Nombre = item.Nombre + " " + item.Apellido;
            AlumnoMoroso.Fecha = DateTime.Today.ToLongDateString();
            AlumnoMoroso.CantidadCuotas = item.CuotasAdeudadas;
            AlumnoMoroso.Carrera = item.Carrera;
            //AlumnoMoroso.Importe = item.ImporteDeuda.ToString();
            AlumnoMoroso.Importe = string.Format("{0:###,###.00}", item.ImporteDeuda);
            return AlumnoMoroso;
        }
    }
}

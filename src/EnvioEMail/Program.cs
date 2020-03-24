using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvioEMail.Models;
using EnvioEMail.Repositories;
using EnvioEMail.Controllers;

using RazorEngine;
using RazorEngine.Templating;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace EnvioEMail
{
    class Program
    {
        static void Main(string[] args)
        {
            NotificacionesDeuda();
        }

        private static void NotificacionesDeuda()
        {
            IList<Carreras> carreras = CarrerasRepository.ObtenerCarreras();
            foreach (var item in carreras)
            {
                var morososPorCarrera = AlumnosMorososRepository.ObtenerAlumnosMorosos(item.Id);
                
                ProcesarEMailMorosos(morososPorCarrera);
            }
        }

        private static void ProcesarEMailMorosos(IEnumerable<AlumnoMoroso> morosos)
        {
            var dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var template = System.IO.File.ReadAllText($@"{dir}\Plantillas\default.cshtml");

            foreach (var item in morosos)
            {
                Moroso moroso = cargarMoroso(item);

                var result = Engine.Razor.RunCompile(template, moroso.Id.ToString(), typeof(Moroso), moroso, null);
                EMailController.EnviarMail("Notificación de deuda - " + DateTime.Now, result, true);

            }
        }

        public static Moroso cargarMoroso(AlumnoMoroso item)
        {
            DateTime hoy = System.DateTime.Now;
            Moroso AlumnoMoroso = new Moroso();
            AlumnoMoroso.Id = Convert.ToInt32( String.Format("{0:yy}{1:000}", hoy, hoy.DayOfYear) ) + item.IdPago;
            AlumnoMoroso.Nombre = item.Nombre + ", " + item.Nombre;
            AlumnoMoroso.Fecha = DateTime.Today.ToLongDateString();
            AlumnoMoroso.CantidadCuotas = item.CuotasAdeudadas;
            AlumnoMoroso.Carrera = item.Carrera;
            AlumnoMoroso.Importe = item.ImporteDeuda.ToString();
            return AlumnoMoroso;
        }

    }
}

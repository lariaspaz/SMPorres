using NotificacionesDeuda.Lib;
using NotificacionesDeuda.Models;
using NotificacionesDeuda.Repositories;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesDeuda.Controller
{
    internal class NotificacionDeudaController
    {
        public void Notificar()
        {
            var carreras = CarrerasRepository.ObtenerCarreras();
            var cantidad = Configuration.MaxCount;
            foreach (var item in carreras)
            {
                var morososPorCarrera = AlumnosMorososRepository.ObtenerAlumnosMorosos(item.Id);
                if (cantidad > -1)
                {
                    morososPorCarrera = morososPorCarrera.Take(cantidad).ToList();
                }
                ProcesarEMailMorosos(morososPorCarrera);
                cantidad = cantidad - morososPorCarrera.Count();
            }
        }

        private void ProcesarEMailMorosos(IEnumerable<AlumnoMoroso> morosos)
        {
            var dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var template = System.IO.File.ReadAllText($@"{dir}\Plantillas\default.cshtml");

            foreach (var item in morosos)
            {
                Moroso moroso = CargarMoroso(item);
                var result = Engine.Razor.RunCompile(template, moroso.Id.ToString(), typeof(Moroso), moroso, null);
                var destinatario = String.IsNullOrEmpty(Configuration.To) ? item.EMail : Configuration.To;
                EmailController.EnviarMail("Notificación de deuda - " + DateTime.Now, result, true, destinatario);
            }
        }

        private Moroso CargarMoroso(AlumnoMoroso item)
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

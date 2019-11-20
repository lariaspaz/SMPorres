using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvioEMail.Models;
using EnvioEMail.Repositories;

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
                var morosos = AlumnosMorososRepository.ObtenerAlumnosMorosos(item.Id);
                
                ProcesarEMailMorosos(morosos);
            }
        }

        private static void ProcesarEMailMorosos(IEnumerable<AlumnoMoroso> morosos)
        {
            foreach (var item in morosos)
            {
                EMail eMail = new EMail();
                if (!string.IsNullOrEmpty(item.EMail))
                {
                    eMail.To = item.EMail;
                    eMail.Body = EMailRepository.ArmarBodyEMailHtmlImage(item.Apellido, item.Nombre, item.Documento, item.Carrera,
                                                item.Curso, item.CuotasAdeudadas, item.ImporteDeuda);
                    eMail.Subject = "Notificación de deuda";
                    EMailRepository.EnviarEMailImage(eMail);
                }

            }
            morosos = null;
        }
    }
}

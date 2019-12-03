using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiInscripción.Models.ViewModels
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdTipoDocumento { get; set; }
        public decimal NroDocumento { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string EMail { get; set; }
        public string Direccion { get; set; }
        public byte Estado { get; set; }
        public string Sexo { get; set; }
        public string Contraseña { get; set; }
        public int IdCurso { get; set; }
    }
}
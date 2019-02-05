using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models.InformesModels
{
    class AlumnoCursante
    {
        public int IdCarrera { get; set; }

        public string Carrera { get; set; }

        public int IdCurso { get; set; }

        public string Curso { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string EMail { get; set; }

        public byte Estado { get; set; }

        public string LeyendaEstado()
        {
            switch ((EstadoAlumno)Estado)
            {
                case EstadoAlumno.Activo:
                    return "Activo";
                case EstadoAlumno.Baja:
                    return "Baja";
            }
            return "";
        }
    }
}

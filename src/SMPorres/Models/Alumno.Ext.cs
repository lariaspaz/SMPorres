using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public partial class Alumno
    {
        public string LeyendaEstado
        {
            get
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

        public string LeyendaSexo
        {
            get
            {
                return (Sexo == "M") ? "Masculino" : "Femenino";
            }
        }
    }
}

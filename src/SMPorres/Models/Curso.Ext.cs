using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public partial class Curso
    {
        public string LeyendaModalidad {
        get
            {
                switch ((ModalidadCursado) Modalidad)
                {
                    case ModalidadCursado.Anual:
                        return "Anual";
                    case ModalidadCursado.PrimerCuatrimestre:
                        return "Primer cuatrimestre";
                    case ModalidadCursado.SegundoCuatrimestre:
                        return "Segundo cuatrimestre";
                    case ModalidadCursado.SinCursado:
                        return "Sin cursado";
                    default:
                        break;
                }
                return "";
            }
        }

        public string LeyendaEstado
        {
            get
            {
                switch ((EstadoCurso)Estado)
                {
                    case EstadoCurso.Activo:
                        return "Activo";
                    case EstadoCurso.Baja:
                        return "Baja";
                }
                return "";
            }
        }
    }
}

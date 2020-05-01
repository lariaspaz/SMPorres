using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public partial class TasaMora
    {
        public string LeyendaEstado
        {
            get
            {
                switch ((EstadoTasaMora)Estado)
                {
                    case EstadoTasaMora.Activa:
                        return "Activa";
                    case EstadoTasaMora.Baja:
                        return "Baja";
                    default:
                        break;
                }
                return "";
            }
        }

    }
}

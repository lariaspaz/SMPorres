using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public partial class PlanPago
    {
        public string LeyendaEstado
        {
            get
            {
                switch ((EstadoPlanPago) Estado)
                {
                    case EstadoPlanPago.Vigente:
                        return "Vigente";
                    case EstadoPlanPago.Baja:
                        return "Baja";
                    case EstadoPlanPago.Cancelado:
                        return "Cancelado";
                }
                return "";
            }
        }

        public string LeyendaTipoBeca
        {
            get
            {
                switch ((TipoBeca) TipoBeca)
                {
                    case Models.TipoBeca.AplicaHastaVto:
                        return "Aplica hasta vto.";
                    case Models.TipoBeca.AplicaSiempre:
                        return "Aplica siempre";
                }
                return "";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public class PagoBSE
    {
        //succod	sucursal	moneda	comprobante	tipo_mov	importe	fecha_proceso	cuil	usuario	hora	codbarra	grupoterminal	nrorendicion	fecha_cobro
        public int CódigoSucursal { get; set; }

        public string NombreSucursal { get; set; }

        public string Moneda { get; set; }

        public string Comprobante { get; set; }

        public string TipoMovimiento { get; set; }

        public string Importe { get; set; }

        public string FechaProceso { get; set; }

        public string CuilUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public string Hora { get; set; }

        public string CódigoBarra { get; set; }

        public string GrupoTerminal { get; set; }

        public string NroRendición { get; set; }

        public string FechaMovimiento { get; set; }

    }
}

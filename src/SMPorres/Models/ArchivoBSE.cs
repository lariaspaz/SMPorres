using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public class ArchivoBSE
    {
        //succod	sucursal	moneda	comprobante	tipo_mov	importe	fecha_proceso	cuil	usuario	hora	codbarra	grupoterminal	nrorendicion	fecha_cobro
        public int CódigoSucursal { get; set; }

        public string Sucursal { get; set; }

        public string Moneda { get; set; }

        public Int32 Comprobante { get; set; }

        public string TipoMov { get; set; }

        public decimal Importe { get; set; }

        public DateTime FechaProceso { get; set; }

        public string Cuil { get; set; }

        public string Usuario { get; set; }

        public Int32 Hora { get; set; }

        public string CódigoBarra { get; set; }

        public string GrupoTerminal { get; set; }

        public string NroRendicion { get; set; }

        public DateTime FechaCobro { get; set; }

    }
}

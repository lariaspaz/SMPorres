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
        public int succod { get; set; }
        public string sucursal { get; set; }
        public string moneda { get; set; }
        public string comprobante { get; set; }
        public string tipo_mov { get; set; }
        public double importe { get; set; }
        public DateTime fecha_proceso { get; set; }
        public string cuil { get; set; }
        public string usuario { get; set; }
        public DateTime hora { get; set; }
        public string codbarra { get; set; }
        public string grupoterminal { get; set; }
        public string nrorendicion { get; set; }
        public DateTime fecha_cobro { get; set; }
    }
}

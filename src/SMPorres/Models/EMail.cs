using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public class EMail
    {
        public string From { get; set; }

        public string To { get; set; }

        public string CC { get; set; }

        public string CCO { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}

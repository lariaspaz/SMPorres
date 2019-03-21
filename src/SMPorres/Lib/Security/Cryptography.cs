using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Lib.Security
{
    public class Cryptography
    {
        public static string CalcularMD5(string archivo)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(archivo))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}

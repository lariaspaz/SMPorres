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

        public static string CalcularSHA512(string contraseña)
        {
            using (var alg = SHA512.Create())
            {
                alg.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                return BitConverter.ToString(alg.Hash);
            }
        }

        public static string GenerarContraseña()
        {
            var min = new List<char>();
            var may = new List<char>();
            var nros = new List<char>();

            for (int i = (int)'A'; i < (int)'Z'; i++)
            {
                may.Add((char)i);
            }
            for (int i = (int)'a'; i < (int)'z'; i++)
            {
                min.Add((char)i);
            }
            for (int i = (int)'0'; i < (int)'9'; i++)
            {
                nros.Add((char)i);
            }

            int[] pwd = new int[8];

            var r = new Random();
            pwd[0] = r.Next(1, may.Count());

            var r2 = new Random();
            for (int i = 1; i < 6; i++)
            {
                pwd[i] = r.Next(1, min.Count());
            }

            for (int i = 6; i < 8; i++)
            {
                pwd[i] = r.Next(1, nros.Count());
            }

            Func<int, int, char> intToChar = (int t, int idx) => (idx == 0) ? may.ElementAt(t) :
                                                                 (idx == 6 || idx == 7) ? nros.ElementAt(t) :
                                                                 min.ElementAt(t);
            return String.Join("", pwd.Select(intToChar));
        }

    }
}

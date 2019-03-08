using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Lib.Calculos
{
    public class DígitoVerificador
    {
        public static int CalculaDigitoVerificador(string códigoBarra)
        {
		  const string semilla = "5973";
		  var list = códigoBarra.ToCharArray().Select(c => Convert.ToInt32(c.ToString()));
		  var sem = String.Concat(Enumerable.Repeat(semilla, códigoBarra.Length / semilla.Length + 1));
		  var list2 = sem.ToCharArray().Select(c => Convert.ToInt32(c.ToString()));
		  var sum = list.ElementAt(0) + list.Skip(1).Select((item, index) => item * list2.ElementAt(index)).Sum();            
		  return (int) Math.Truncate((double) sum / (double) 2) % 10;
        }
        
        public static int CalculaDigitoVerificador_old(string codBarra, string semilla)
        {
            int digitoVerificador = 0;

            List<int> Codigo = new List<int>();
            Codigo = ListarElementosCodigoBarra(codBarra);

            List<int> Semilla = new List<int>();
            Semilla = ListarElementosSemilla(semilla);

            int Multiplicar = MultiplicarElementos(Codigo, Semilla);

            int x = Multiplicar / 2;

            if ((x % 10) > 0) digitoVerificador = x % 10;

            return digitoVerificador;
        }

        private static List<int> ListarElementosCodigoBarra(string codigoBarra)
        {
            if (codigoBarra.Length <= 0) return null;            

            List<int> LCodigo = new List<int>();
            var loopCounter = 0;
            while (loopCounter < codigoBarra.Length)
            {
                var digit = Convert.ToInt32(codigoBarra.Substring(loopCounter, 1));
                LCodigo.Add(digit);
                loopCounter++;
            }
            return LCodigo;
        }

        private static List<int> ListarElementosSemilla(string semilla)
        {
            if (semilla.Length <= 0) return null;

            List<int> LSemilla = new List<int>();
            var loopCounter = 0;
            while (loopCounter < semilla.Length)
            {
                var digit = Convert.ToInt32(semilla.Substring(loopCounter, 1));
                LSemilla.Add(digit);
                loopCounter++;
            }
            return LSemilla;
        }

        private static int MultiplicarElementos(List<int> codigo, List<int> semilla)
        {
            var s = "0000002191090000082800";
            var s2 = "15973";
            var list = s.ToCharArray().Select(c => Convert.ToInt32(c));

            var sem = String.Concat(Enumerable.Repeat(s2, s.Length / s2.Length + 1));
            var list2 = sem.Substring(0, s.Length - 1).ToCharArray().Select(c => Convert.ToInt32(c));

            

            Console.WriteLine(list2);

            int Multiplicacion = 0;
            int contadorSemilla = 0;

            Multiplicacion += codigo[0] * 1;    // 1° elemento del CodBarra * 1

            for (int i = 1; i < codigo.Count; i++)
            {
                Multiplicacion += codigo[i] * semilla[contadorSemilla];
                contadorSemilla += 1;

                if (contadorSemilla == semilla.Count) contadorSemilla = 0;  //cuando termina la semilla vuelve a posición 0
            }

            return Multiplicacion;
        }
    }
}

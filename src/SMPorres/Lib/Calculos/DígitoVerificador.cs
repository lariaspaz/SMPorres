using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Lib.Calculos
{
    public class DígitoVerificador
    {
        public static int CalculaDigitoVerificador(string codBarra, string semilla)
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

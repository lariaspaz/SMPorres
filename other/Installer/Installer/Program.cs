using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Installer.Lib.Security;
using System.Data.SqlClient;

namespace Installer
{
    public class Program
    {
        static void Main(string[] args)
        {
            int disponible = ObtenerVersiónDisponible();
            int instalada = ObtenerVersiónInstalada();

            if (disponible  >   instalada)
            {
                InstalarNuevaVersión();
            }
        }

        private static void InstalarNuevaVersión()
        {
            //string cPath = "D:\\Proyectos\\SMPorres\\install\\SMPorres";
            //string cPath = "\\\\JHC\\D$\\Proyectos\\SMPorres\\install\\SMPorres";
            string cPath = "\\\\DB-2K18-02\\C$\\Software\\SMPorres";
            string cParams = "/verysilent";
            ProcessStartInfo startInfo = new ProcessStartInfo(string.Concat(cPath, "\\", "Setup.exe"));
            startInfo.Arguments = cParams;
            startInfo.UseShellExecute = false;

            //startInfo.UseShellExecute = true;
            startInfo.Verb = "runas";
            System.Diagnostics.Process.Start(startInfo);

            GrabarConexión();
        }

        private static void GrabarConexión()
        {
            var connStr =  //StringCipher.Encrypt(txtConnectionString.Text, SMPorresEntities.ConnectionStringPassPhrase);
                //JHC   15MP
                //"E2Xge6CmJK0qzV3GTcEWjFuGIzCS8Qu+OmL2ivoAEteu05BMBy0PS2cLzCOkJQ2nPFNY3C11oUUh1+ts0JRz880gQGDJ68EEoLR6TKo1wAoFwfspFyyEWb2WLNLf6lZd3PS6lLM/ag7maYrji0R0OOzqcFffbdnZKNwXmJnJ3NEe2bss9M1R3F+XrdnLSr96IqQgOcK3SbQDskM4bNgIDmNvBV37LnTzG5JmsplRPIWchu5A+bQrI7UOZXrWtjMe";

                //ISMP  15MP
                "4OtSBTA8Vlo110HBD0qCDX3ZHF7UEqt9ZWnFgTXXqCx+a59utI1yZ59GdC2XXxNzFLm1pT8RqUGp8zKt4WtCS3XSqQOoyq4wtHUnFZsx6WUiqdHO/2XjLupBSyqJscHA5RGu1yVrTeagNa/JLNc7lRTbUOc5nOr+TWVuzroMkD8wmfUHyGSKX4Hg9w6v2eO/Jwl+Iy6uCwZ335OjovtsF1hUcx4k/JY7qvtKg3cuNuYAlXJeOljS5EcQQRnqwv+Z";
            string keyName = //txtClaveRegistroGrabar.Text.Substring(0, txtClaveRegistroGrabar.Text.LastIndexOf("\\"));
                "HKEY_LOCAL_MACHINE\\SOFTWARE\\SMP";
            string value = //txtClaveRegistroGrabar.Text.Substring(txtClaveRegistroGrabar.Text.LastIndexOf("\\") + 1);
                "Cs";
            Registry.SetValue(keyName, value, connStr);
        }

        public static int ObtenerVersiónInstalada()
        {
            int v = 0;
            try
            {
                string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
                {
                    foreach (string subkey_name in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                        {
                            //Console.WriteLine(subkey.GetValue("DisplayName"));
                            string str = subkey.GetValue("DisplayName").ToString();
                            if (str.Length >= 6 & string.Equals(str.Substring(0, 6), "ISMP v"))
                            {
                                //Versión Instalada --  //ISMP versión 1.3.0.28
                                string a = str.Remove(0, 13);

                                v = Convert.ToInt32(a.Replace(".", ""));
                                break;
                            }
                        }
                    }

                }
                return v;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int ObtenerVersiónDisponible()
        {
            try
            {
                //string[] lines = System.IO.File.ReadAllLines(@"D:\Proyectos\SMPorres\other\Installer\Versión.txt");
                //string[] lines = System.IO.File.ReadAllLines(@"\\JHC\D$\Proyectos\SMPorres\other\Installer\Versión.txt");
                string[] lines = System.IO.File.ReadAllLines(@"\\DB -2K18-02\C$\Software\SMPorres\Versión.txt");
                int versiónDisponible = 0;
                int fila = 0;
                foreach (string line in lines)
                {
                    fila++;
                    if (fila == 1 & !string.Equals(line.Trim(), "ISMP versión")) return -10;
                    if (fila == 2) versiónDisponible = Convert.ToInt32(line.Replace(".", ""));
                }
                return versiónDisponible;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}

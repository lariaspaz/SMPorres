using Installer.Lib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installer.Controller
{
    class InstallerController
    {
        private static string _cPath = @"\\DB-2K18-02\C$\Software\SMPorres";
        private static string _connStr = //DB-2K18-02  D4t4B4s315MP
            "Pex+FSWu7ypF5O2ZlcY4TiT9QjZvSmYuFnE/Ruyab4zHbxlm7XqwfkICG8iYRcPhXn/Y8XxebRMtn7kCkStuQBMHiR9LsCF3TaXRruMlqQQMIDdSrS7C88f9tIWohBflsKZJy8yHg8Uy9ggdGEBpu6mR+cwYQpMGe7Jl46ad41yks9ePAc0Bn9sLgxmuE5dpCPvmhxRnBllubEYVgIVCmeAaZj0SQdmZVWRZvFrgLcNaSbxYoD8yqE8GCe7YSvPR";
        private static string _versión = @"\\DB -2K18-02\C$\Software\SMPorres\Versión.txt";
           
        public static void EjecutarSMPorres()
        {
            string entorno = Configuration.Environment;
            if (entorno == "JHC")
            {
                _cPath = @"\\JHC\D$\Proyectos\SMPorres\install\SMPorres";
                _connStr =  //JHC   D4t4B4s315MP
                    "MEnpV0q7cKo4oEkEmW9Oaak41C2nFTLm2/HbbkijfPHbx4eAtxWEPG/F0WWFux4EHH91/CcGoVMr38wxjNxz8W9Fz1Yq4GcL9pRGSKbAUAN+q3DUCgQKDHMl/YSKFTnOhZja/tsTMSjpuQiEosJFFb+QAlTS1VSSs4G3AhLGqPViyLWrQm2IZ2T/3R54DC24S5aRqfbiZDzHGf/hxwBv7ck7ADkR3+GFhoTK+UiMifdKcwcCI5FHzLnj4Nb+CRlP";
                _versión = @"\\JHC\D$\Proyectos\SMPorres\other\Installer\Versión.txt";
            }

            int disponible = ObtenerVersiónDisponible();
            int instalada = ObtenerVersiónInstalada();

            if (disponible > instalada)
            {
                InstalarNuevaVersión();
            } 

            EjecutarPrograma();
        }

        private static void EjecutarPrograma()
        {
            string cPath = @"C:\Program Files\ISMP";
            ProcessStartInfo startInfo = new ProcessStartInfo(string.Concat(cPath, "\\", "SMPorres.exe"));
            startInfo.UseShellExecute = false;
            startInfo.Verb = "runas";
            System.Diagnostics.Process.Start(startInfo);
        }

        private static void InstalarNuevaVersión()
        {
            string cParams = "/verysilent";
            ProcessStartInfo startInfo = new ProcessStartInfo(string.Concat(_cPath, "\\", "Setup.exe"));
            startInfo.Arguments = cParams;
            startInfo.UseShellExecute = false;

            startInfo.Verb = "runas";
            System.Diagnostics.Process.Start(startInfo);

            GrabarConexión();
        }

        private static void GrabarConexión()
        {
            var connStr =  _connStr;
            string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\SMP";
            string value = "Cs";
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
                            string str = subkey.GetValue("DisplayName").ToString();
                            if (str.Length >= 6 & string.Equals(str.Substring(0, 6), "ISMP v"))
                            {
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
                string[] lines = System.IO.File.ReadAllLines(_versión);
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

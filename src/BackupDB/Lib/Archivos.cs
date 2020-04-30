using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupDB.Models;
using System.IO;
using System.IO.Compression;

namespace BackupDB.Lib
{
    static class Archivos
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private static string DBFile
        {
            get
            {
                var fileName = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
                var path = System.Configuration.ConfigurationManager.AppSettings["Path"];
                Directory.CreateDirectory(path); //crea la ruta si no existe
                var f = $"{path}\\{fileName}.bak";
                using (var db = new SMPorres())
                {
                    var dbName = db.Database.Connection.Database;
                    db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction,
                        $"BACKUP DATABASE {dbName} TO DISK = '{f}' WITH FORMAT;");
                }
                return f;
            }
        }

        private static string Comprimir(this string dbName)
        {
            var f = Path.GetFileName(dbName);
            string archivo = Path.GetDirectoryName(dbName) + "\\" +
                Path.ChangeExtension(f, "zip");
            using (FileStream fs = new FileStream(archivo, FileMode.Create))
            using (ZipArchive arch = new ZipArchive(fs, ZipArchiveMode.Create))
            {
                arch.CreateEntryFromFile(dbName, f);
            }
            return archivo;
        }

        private static List<string> ObtenerRuta(DriveService svc)
        {
            var ruta = new List<string>();
            var ids = BuscarCarpeta(svc, "name = 'Backup del sistema de Cobranzas' and trashed = false");
            if (!ids.Any())
            {
                ids.Add(CrearCarpeta(svc, "Backup del sistema de Cobranzas", null));
            }

            string mes = "";
            using (var db = new SMPorres())
            {
                DateTime fecha = db.Database.SqlQuery<DateTime>("select getdate()").FirstOrDefault();
                mes = $"{fecha.Year}-{fecha.Month:00}";
            }

            var childs = new List<string>();
            foreach (var item in ids)
            {
                childs = BuscarCarpeta(svc, $"'{item}' in parents and name = '{mes}'");
            }

            if (childs.Any())
            {
                ruta.Add(childs.FirstOrDefault());
            }
            else
            {
                ruta.Add(CrearCarpeta(svc, mes, ids));
            }

            return ruta;
        }

        private static List<string> BuscarCarpeta(DriveService svc, string filtro)
        {
            var result = new List<string>();
            var reqList = svc.Files.List();
            reqList.Q = "mimeType = 'application/vnd.google-apps.folder' and " + filtro;
            do
            {
                var files = reqList.Execute();
                foreach (var item in files.Files)
                {
                    result.Add(item.Id);
                }
                reqList.PageToken = files.NextPageToken;
            } while (reqList.PageToken != null);
            return result;
        }

        private static string CrearCarpeta(DriveService svc, string nombre, List<string> parents)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = nombre,
                MimeType = "application/vnd.google-apps.folder",
                Parents = parents
            };
            var request = svc.Files.Create(fileMetadata);
            request.Fields = "id";
            var p1 = request.Execute();
            return p1.Id;
        }

        public static string UploadDB(DriveService svc)
        {
            var dbFile = DBFile;
            var zipFile = dbFile.Comprimir();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(zipFile),
                Parents = ObtenerRuta(svc)
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(zipFile, FileMode.Open))
            {
                request = svc.Files.Create(fileMetadata, stream, "application/x-zip-compressed");
                request.Fields = "id";
                var u = request.Upload();
                if (u.Exception != null && !String.IsNullOrEmpty(u.Exception.Message))
                {
                    _log.Error(u.Exception);
                }
                //Console.WriteLine("Request:\n" + Newtonsoft.Json.JsonConvert.SerializeObject(request));
            }
            var file = request.ResponseBody;
            if (file == null)
                _log.Debug("file es null");
            else if (file.Id == null)
            {
                _log.Debug("file.id es null");
                _log.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(file));
            }
            else
                _log.Debug("File ID: " + file.Id);

            DeleteFile(zipFile);
            DeleteFile(dbFile);

            return Path.GetFileName(zipFile);
        }

        private static void DeleteFile(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
        }
    }
}

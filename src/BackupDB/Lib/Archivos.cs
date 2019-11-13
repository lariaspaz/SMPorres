﻿using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupDB.Lib
{
    static class Archivos
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void UploadData(DriveService svc)
        {
            var fileMetadata = new File()
            {
                Name = $"{DateTime.Now.ToString("yyyyMMddHHmmss")} - photo.jpg"
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(@"C:\Proyectos\Varios\Google\Drive\IMG_20180125_113129789_HDR.jpg",
                System.IO.FileMode.Open))
            {
                request = svc.Files.Create(fileMetadata, stream, "image/jpeg");
                request.Fields = "id";
                var u = request.Upload();
                //Console.WriteLine(u.Exception);
                _log.Error(u.Exception);
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
        }

        private static string DBFile
        {
            get
            {
                var s = System.IO.Path.GetDirectoryName(
                            System.Reflection.Assembly.GetExecutingAssembly().Location) +
                        @"\Files\SMPorres.7z";
                return s;                
            }
        }


        public static void UploadDB(DriveService svc)
        {
            var fileMetadata = new File()
            {
                Name = $"{DateTime.Now.ToString("yyyyMMddHHmmss")} - db.7z"
            };
            FilesResource.CreateMediaUpload request;
            
            using (var stream = new System.IO.FileStream(DBFile, System.IO.FileMode.Open))
            {
                request = svc.Files.Create(fileMetadata, stream, "application/x-7z-compressed");
                request.Fields = "id";
                var u = request.Upload();
                if (u.Exception != null &&!String.IsNullOrEmpty(u.Exception.Message))
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
        }


        public static Google.Apis.Drive.v3.Data.File uploadFile(DriveService _service, string _uploadFile, string _parent, string _descrp = "Uploaded with .NET!")
        {
            if (System.IO.File.Exists(_uploadFile))
            {
                File body = new File();
                body.Name = System.IO.Path.GetFileName(_uploadFile);
                body.Description = _descrp;
                body.MimeType = GetMimeType(_uploadFile);
                //body.Parents = new List<ParentReference>() { new ParentReference() { Id = _parent } };

                byte[] byteArray = System.IO.File.ReadAllBytes(_uploadFile);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                try
                {
                    FilesResource.CreateMediaUpload request = _service.Files.Create(body, stream, GetMimeType(_uploadFile));
                    request.Upload();
                    return request.ResponseBody;
                }
                catch (Exception e)
                {
                    _log.Error(e);
                }
            }
            else
            {
                //MessageBox.Show("The file does not exist.", "404");
                _log.Error("The file does not exist.");
            }

            return null;
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}

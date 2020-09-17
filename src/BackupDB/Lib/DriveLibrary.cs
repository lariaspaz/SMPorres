using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackupDB.Lib
{
    internal static class DriveLibrary
    {
        internal static DriveService CrearServicioComoCuentaUsuario()
        {
            UserCredential credential;

            #region Login con archivo json
            //using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            //{
            //    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.Load(stream).Secrets,
            //        new[] { BooksService.Scope.Books },
            //        "user", CancellationToken.None, new FileDataStore("Books.ListMyLibrary"));
            //}
            #endregion

            var secrets = new ClientSecrets
            {
                ClientId = "",
                ClientSecret = ""
            };

            string[] scopes = new[] {
                        DriveService.Scope.Drive,
                        DriveService.Scope.DriveFile
                };

            string usr = "admin"; //debe ser usuario admin
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, usr,
                CancellationToken.None).Result;

            var init = new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = "SqlBackup"
            };
            var svc = new DriveService(init);

            //Long Operations like file uploads might timeout. 10 is just precautionary value, can be 
            //set to any reasonable value depending on what you use your service for.
            svc.HttpClient.Timeout = TimeSpan.FromMinutes(10);

            return svc;
        }
    }
}

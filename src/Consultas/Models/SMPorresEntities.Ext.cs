namespace Consultas.Models
{
    using System;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.SqlClient;

    public partial class SMPorresEntities
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string ConnectionStringPassPhrase = "_SMPorres 11:06";

        public SMPorresEntities() : base(GetConnectionString()) { }

        private static string GetConnectionString()
        {
            var connStr = System.Configuration.ConfigurationManager.AppSettings["Connection"];
            if (String.IsNullOrEmpty(connStr))
            {
                _log.Debug("Buscando una cadena de conexión en el registro");
                var model = "SMPModel";
                string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\SMP\Cs";
                string keyName = key.Substring(0, key.LastIndexOf("\\"));
                string valueName = key.Substring(key.LastIndexOf("\\") + 1);
                var value = (string)Microsoft.Win32.Registry.GetValue(keyName, valueName, "");
                if (String.IsNullOrEmpty(value))
                {
                    _log.Debug($"value = {value}");
                    throw new Exception("No se encontró una cadena de conexión: se debe configurar una cadena de conexión.");
                }

                _log.Debug("Desencriptando cadena");
                var conn = Lib.Security.StringCipher.Decrypt(value, ConnectionStringPassPhrase);

                _log.Debug("Creando cadena de conexión SQL");
                var providerSB = new SqlConnectionStringBuilder(conn);
                var efConnection = new EntityConnectionStringBuilder();
                // or the config file based connection without provider connection string
                // var efConnection = new EntityConnectionStringBuilder(@"metadata=res://*/model1.csdl|res://*/model1.ssdl|res://*/model1.msl;provider=System.Data.SqlClient;");
                efConnection.Provider = "System.Data.SqlClient";
                efConnection.ProviderConnectionString = providerSB.ConnectionString;
                // based on whether you choose to supply the app.config connection string to the constructor
                efConnection.Metadata = string.Format("res://*/Models.{0}.csdl|res://*/Models.{0}.ssdl|res://*/Models.{0}.msl", model);
                return efConnection.ToString();
            }
            else
            {
                _log.Debug("Buscando una cadena de conexión en la configuración local");
                return System.Configuration.ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public partial class SMPorresEntities
    {
        public const string ConnectionStringPassPhrase = "_SMPorres 11:06";

        //public SMPorresEntities() : base(GetConnectionString())
        //{}

        private static string GetConnectionString()
        {
            var connStr = System.Configuration.ConfigurationManager.AppSettings["Connection"];
            if (String.IsNullOrEmpty(connStr))
            {
                var model = "SMPorres";

                string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\SMP\Cs";
                string keyName = key.Substring(0, key.LastIndexOf("\\"));
                string valueName = key.Substring(key.LastIndexOf("\\") + 1);
                var value = (string)Microsoft.Win32.Registry.GetValue(keyName, valueName, "");
                var conn = Lib.Security.StringCipher.Decrypt(value, ConnectionStringPassPhrase);

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
                return System.Configuration.ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            }

        }
    }
}

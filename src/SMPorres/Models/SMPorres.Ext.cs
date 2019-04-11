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
        public SMPorresEntities() : base(GetConnectionString())
        {}

        private static string GetConnectionString()
        {
            var model = "SMPorres";

            var dir = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            var conn = System.IO.File.ReadAllText(dir + @"\.smp");
            conn = Lib.Security.StringCipher.Decrypt(conn, "_SMPorres 11:06");
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
    }
}

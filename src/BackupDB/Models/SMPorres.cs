namespace BackupDB.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Data.Entity.Core.EntityClient;

    public partial class SMPorres : DbContext
    {
        public const string ConnectionStringPassPhrase = "_SMPorres 11:06";

        public SMPorres()
            : base(GetConnectionString())
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        private static string GetConnectionString()
        {
            var connStr = System.Configuration.ConfigurationManager.AppSettings["Connection"];
            if (String.IsNullOrEmpty(connStr))
            {
                //var model = "SMPorres";
                string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\SMP\Cs";
                string keyName = key.Substring(0, key.LastIndexOf("\\"));
                string valueName = key.Substring(key.LastIndexOf("\\") + 1);
                var value = (string)Microsoft.Win32.Registry.GetValue(keyName, valueName, "");
                var conn = Lib.Security.StringCipher.Decrypt(value, ConnectionStringPassPhrase);
                return conn;
            }
            else
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            }

        }
    }
}

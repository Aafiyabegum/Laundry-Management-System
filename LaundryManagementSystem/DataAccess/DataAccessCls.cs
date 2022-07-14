using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LaundryManagementSystem.DataAccess
{

    public class DataAccessCls
    {
        private DbConnection connection;
        private DbTransaction transaction;
        private string Connectionstring;
        private DbProviderFactory providerFactory;



        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["LaundryDBConnection"].ConnectionString;
        }

        public DataAccessCls()
        {
            providerFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            this.connection = providerFactory.CreateConnection();
            this.connection.ConnectionString = GetConnection();
            //this.transaction = this.connection.BeginTransaction();
        }

        public IEnumerable<T> Query<T>(string SqlString) where T:class
        {
            return this.connection.Query<T>(SqlString);
        }

        public int Execute(string query)
        {
            return this.connection.Execute(query);
        }
    }
}
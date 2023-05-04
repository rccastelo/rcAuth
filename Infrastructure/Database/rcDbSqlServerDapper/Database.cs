using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace rcDbSqlServerDapper
{
    public class Database
    {
        private string _connectionString;
        private IConfiguration _configuration;
        public IDbConnection _dbConnection;

        public Database(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = _configuration.GetConnectionString("DefaultConnection");

            this.Open();
        }

        public void Open() 
        {
            this.Close();

            this._dbConnection = new SqlConnection(_connectionString);
            this._dbConnection.Open();
        }

        public void Close()
        {
            if (this._dbConnection != null) {
                if (this._dbConnection.State != ConnectionState.Closed) {
                    this._dbConnection.Close();
                }

                this._dbConnection.Dispose();
            }

            this._dbConnection = null;
        }
    }
}

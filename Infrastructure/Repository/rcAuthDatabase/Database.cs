using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace rcAuthRepository
{
    public class Database : IDisposable
    {
        private string _connectionString;
        public IDbConnection _dbConnection;
        private IConfiguration _configuration;

        public Database(IConfiguration configuration)
        {
            this._configuration = configuration;

            this._connectionString = _configuration.GetConnectionString("DefaultConnection");

            this._dbConnection = new SqlConnection(_connectionString);

            this._dbConnection.Open();
        }

        public void Dispose()
        {
            if (this._dbConnection != null)
            {
                if (this._dbConnection.State != ConnectionState.Closed)
                {
                    this._dbConnection.Close();
                }

                this._dbConnection.Dispose();
            }

            this._dbConnection = null;
            this._connectionString = null;
            this._configuration = null;
        }
    }
}

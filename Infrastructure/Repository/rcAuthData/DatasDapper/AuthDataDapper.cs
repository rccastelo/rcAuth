using Dapper;
using Microsoft.Extensions.Configuration;
using rcAuthData.Interfaces;
using rcAuthDomain.Entities;
using rcDbSqlServerDapper;
using System.Linq;

namespace rcAuthData.DatasDapper
{
    public class AuthDataDapper : DataDapper, IAuthData
    {
        public AuthDataDapper(IConfiguration configuration) : base(configuration)
        {
            
        }

        public AuthEntity Login(AuthEntity entity)
        {
            AuthEntity authRet;

            string command = "SELECT u.id, u.login " +
                "FROM Users u " +
                $"WHERE u.login = '{entity.Login}' " +
                $"AND u.password = '{entity.Password}'";

            _database.Open();

            authRet = _conn.Query<AuthEntity>(command).FirstOrDefault();

            _database.Close();

            return authRet;
        }
    }
}

using Dapper;
using Microsoft.Extensions.Configuration;
using rcAuthData.Interfaces;
using rcAuthDomain;
using rcDbSqlServerDapper;
using System.Linq;

namespace rcAuthData.DapperDatas
{
    public class AuthDataDapper : DataDapper, IAuthData
    {
        public AuthDataDapper(IConfiguration configuration) : base(configuration)
        {
            
        }

        public AuthModel Login(AuthModel authModel)
        {
            AuthModel modelRet;

            string command = "SELECT u.id, u.login " +
                "FROM Users u " +
                $"WHERE u.login = '{authModel.Login}' " +
                $"AND u.password = '{authModel.Password}'";

            _database.Open();

            modelRet = _conn.Query<AuthModel>(command).FirstOrDefault();

            _database.Close();

            return modelRet;
        }
    }
}

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

        public AuthEntity Authenticate(AuthEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            AuthEntity authRet;

            string command = "SELECT l.pk_id_login as id, l.login " +
                "FROM Login l " +
                "INNER JOIN Users u ON (u.pk_id_user = l.fk_user_id AND u.status = 1) " +
                "WHERE l.login = @Login " +
                "AND l.secret = @Secret ";

            parameters.Add("@Login", entity.Login, System.Data.DbType.String, System.Data.ParameterDirection.Input, entity.Login.Length);
            parameters.Add("@Secret", entity.Secret, System.Data.DbType.String, System.Data.ParameterDirection.Input, entity.Secret.Length);

            _database.Open();

            authRet = _conn.Query<AuthEntity>(command, parameters).FirstOrDefault();

            _database.Close();

            return authRet;
        }

        public AuthEntity Authorize(AuthEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            AuthEntity authRet;

            string command = "SELECT l.pk_id_login as id, l.login, s.[key] as system, " +
                "p.date_from as DateFrom, p.date_to as DateTo, p.weekday, p.weekend, " + 
                "p.start_time as StartTime, p.end_time as EndTime " +
                "FROM Login l " +
                "INNER JOIN Permissions p ON (p.fk_user_id = l.fk_user_id AND p.status = 1) " +
                "INNER JOIN Systems s ON (s.pk_id_system = p.fk_system_id AND s.[key] = @System AND s.status = 1) " +
                "WHERE l.login = @Login " +
                "AND l.secret = @Secret ";

            parameters.Add("@Login", entity.Login, System.Data.DbType.String, System.Data.ParameterDirection.Input, entity.Login.Length);
            parameters.Add("@Secret", entity.Secret, System.Data.DbType.String, System.Data.ParameterDirection.Input, entity.Secret.Length);
            parameters.Add("@System", entity.System, System.Data.DbType.String, System.Data.ParameterDirection.Input, entity.System.Length);

            _database.Open();

            authRet = _conn.Query<AuthEntity>(command, parameters).FirstOrDefault();

            _database.Close();

            return authRet;
        }
    }
}

using rcAuthDomain;
using rcAuthRepository.Interfaces;
using rcDbSqlServerDapper;

namespace rcAuthRepository.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IData _data;

        public AuthRepository(IData data)
        {
            this._data = data;
        }

        public AuthModel Login(AuthModel authModel)
        {
            string query = "SELECT u.id, u.login " +
                "FROM Users u " +
                $"WHERE u.login = '{authModel.Login}' " +
                $"AND u.password = '{authModel.Password}'";

            return _data.Get<AuthModel>(query);
        }
    }
}

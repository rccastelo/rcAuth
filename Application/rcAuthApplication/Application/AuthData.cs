using rcAuthApplication.Interfaces;
using rcAuthDomain;
using rcAuthRepository.Interfaces;

namespace rcAuthApplication.Application
{
    public class AuthData : IAuthData
    {
        private readonly IRepository _repository;

        public AuthData(IRepository repository)
        {
            this._repository = repository;
        }

        public AuthModel Login(AuthModel authModel)
        {
            string query = "SELECT u.id, u.login " +
                "FROM Users u " +
                $"WHERE u.login = '{authModel.Login}' " +
                $"AND u.password = '{authModel.Password}'";

            return _repository.Get<AuthModel>(query);
        }
    }
}

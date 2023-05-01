using rcAuthDomain;
using rcAuthRepository.Interfaces;

namespace rcAuthApplication
{
    public class AuthData
    {
        private readonly IRepository _repository;

        public AuthData(IRepository repository)
        {
            this._repository = repository;
        }

        public AuthEntity login(AuthEntity authEntity)
        {
            string query = "SELECT u.id, u.name, u.status " +
                "FROM Users u " +
                $"WHERE u.name = '{authEntity.name}'";

            return _repository.get<AuthEntity>(query);
        }
    }
}

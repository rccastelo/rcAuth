using rcAuthData.Interfaces;
using rcAuthDomain;
using rcAuthRepository.Interfaces;

namespace rcAuthRepository.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IAuthData _data;

        public AuthRepository(IAuthData data)
        {
            this._data = data;
        }

        public AuthModel Login(AuthModel authModel)
        {
            AuthModel authModelRet = null;

            authModelRet = _data.Login(authModel);

            return authModelRet;
        }
    }
}

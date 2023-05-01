using rcAuthDomain;

namespace rcAuthApplication
{
    public class AuthService
    {
        private readonly AuthData _authData;

        public AuthService(AuthData authData)
        {
            _authData = authData;
        }

        public AuthEntity login(AuthEntity authEntity)
        {
            return _authData.login(authEntity);
        }
    }
}

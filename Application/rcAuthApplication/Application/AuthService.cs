using rcAuthApplication.Interfaces;
using rcAuthDomain;

namespace rcAuthApplication.Application
{
    public class AuthService : IAuthService
    {
        private readonly IAuthData _authData;

        public AuthService(IAuthData authData)
        {
            _authData = authData;
        }

        public AuthResponse Login(AuthRequest authRequest)
        {
            AuthResponse response = new AuthResponse();

            if (authRequest != null) {
                AuthModel authModelReq = new AuthModel(authRequest);

                AuthModel authModelResp = _authData.Login(authModelReq);

                response.Item = authModelResp;
            }

            return response;
        }
    }
}

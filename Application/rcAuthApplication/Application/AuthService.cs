using rcAuthApplication.Interfaces;
using rcAuthDomain;
using rcAuthRepository.Interfaces;

namespace rcAuthApplication.Application
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public AuthResponse Login(AuthRequest authRequest)
        {
            AuthResponse response = new AuthResponse();

            if (authRequest != null) {
                AuthModel authModelReq = new AuthModel(authRequest);

                AuthModel authModelResp = _authRepository.Login(authModelReq);

                response.Item = authModelResp;
            }

            return response;
        }
    }
}

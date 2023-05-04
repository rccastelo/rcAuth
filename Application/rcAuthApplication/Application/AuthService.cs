using rcAuthApplication.Interfaces;
using rcAuthDomain;
using rcAuthRepository.Interfaces;
using rcCryptography;

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

                string secret = Crypto.GetSecretSHA512(authModelReq.Login + authModelReq.Password);

                authModelReq.Password = secret;

                AuthModel authModelResp = _authRepository.Login(authModelReq);

                response.Item = authModelResp;
            }

            return response;
        }
    }
}

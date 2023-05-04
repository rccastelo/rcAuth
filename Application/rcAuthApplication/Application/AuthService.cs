using rcAuthApplication.Interfaces;
using rcAuthDomain.Model;
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
            AuthModel authModelResp = null;

            if (authRequest != null) {
                AuthModel authModelReq = new AuthModel(authRequest);

                if (authModelReq.IsValid) {
                    string secret = Crypto.GetSecretSHA512(authModelReq.Login + authModelReq.Password);

                    authModelReq.Password = secret;

                    authModelResp = _authRepository.Login(authModelReq);

                    if (authModelResp == null) {
                        response.IsValid = false;
                        response.AddMessage("Usuário e/ou senha inválido(s)");
                    }
                } else {
                    response.IsValid = authModelReq.IsValid;
                    response.Messages = authModelReq.Messages;
                }

                response.Item = authModelResp;
            } else {
                response.IsValid = false;
                response.AddMessage("Requisição não pode ser nula");
            }

            return response;
        }
    }
}

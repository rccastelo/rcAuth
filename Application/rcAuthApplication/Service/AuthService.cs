using rcAuthApplication.Interfaces;
using rcAuthApplication.Transport;
using rcAuthDomain.Models;
using rcAuthRepository.Interfaces;
using rcCryptography;

namespace rcAuthApplication.Service
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
                AuthModel modelReq = new AuthModel(authRequest);

                if (modelReq.IsValidModel) {
                    string secret = Crypto.GetSecretSHA512(modelReq.Entity.Login + modelReq.Entity.Password);

                    modelReq.Entity.Password = secret;

                    AuthModel modelResp = _authRepository.Login(modelReq);

                    if (modelResp == null) {
                        response.IsValid = false;
                        response.AddMessage("Não foi possível realizar o Login do usuário.");
                    } else {
                        response.IsValid = modelResp.IsValidResponse;
                        response.SetItem(modelResp.Transport);
                        response.AddMessages(modelResp.Messages);
                    }
                } else {
                    response.IsValid = false;
                    response.AddMessages(modelReq.Messages);
                }
            } else {
                response.IsValid = false;
                response.AddMessage("Requisição não pode ser nula.");
            }

            return response;
        }
    }
}

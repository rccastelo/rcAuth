using rcAuthApplication.Interfaces;
using rcAuthApplication.Transport;
using rcAuthDomain.Model;
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

                if (modelReq.ValidModel) {
                    string secret = Crypto.GetSecretSHA512(modelReq.Item.Login + modelReq.Item.Password);

                    modelReq.Item.Password = secret;

                    AuthModel modelResp = _authRepository.Login(modelReq);

                    if (modelResp == null) {
                        response.IsValid = false;
                        response.AddMessage("Não foi possível realizar o Login do usuário.");
                    } else {
                        response.IsValid = modelResp.IsValid;
                        response.Item = modelResp.Item;
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

using rcAuthApplication.Interfaces;
using rcAuthApplication.Transport;
using rcAuthDomain.Interfaces;
using rcAuthDomain.Models;
using rcAuthRepository.Interfaces;

namespace rcAuthApplication.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenModel _tokenModel;

        public AuthService(IAuthRepository authRepository,
                ITokenModel tokenModel)
        {
            _authRepository = authRepository;
            _tokenModel = tokenModel;
        }

        public AuthResponse Login(AuthRequest authRequest)
        {
            AuthResponse response = new AuthResponse();

            if (authRequest != null) {
                AuthModel modelReq = new AuthModel(authRequest);

                if (modelReq.IsValidModel) {
                    AuthModel modelResp = _authRepository.Login(modelReq);

                    if (modelResp != null) {
                        if (modelResp.IsValidResponse) {

                            string token = _tokenModel.GenerateToken(modelResp.Entity);
                            modelResp.SetToken(token);

                            response.SetItem(modelResp.ResponseItem);
                        } else {
                            response.SetItem(modelReq.ResponseItem);
                            response.AddMessages(modelResp.Messages);
                        }

                        response.IsValid = modelResp.IsValidResponse;
                    } else {
                        response.IsValid = false;
                        response.SetItem(modelReq.ResponseItem);
                        response.AddMessage("Não foi possível realizar o Login do usuário.");
                    }
                } else {
                    response.IsValid = false;
                    response.SetItem(modelReq.ResponseItem);
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

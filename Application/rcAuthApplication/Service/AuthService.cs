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
        private readonly IAuthBusiness _authBusiness;

        public AuthService(IAuthRepository authRepository,
                ITokenModel tokenModel,
                IAuthBusiness authBusiness)
        {
            _authRepository = authRepository;
            _tokenModel = tokenModel;
            _authBusiness = authBusiness;
        }

        public AuthResponse Login(AuthRequest authRequest)
        {
            AuthResponse response = new AuthResponse();

            response.IsValid = false;

            if (authRequest == null) {
                response.AddMessage("Requisição não pode ser nula.");
                return response;
            }

            AuthModel modelReq = new AuthModel(authRequest);

            if (!modelReq.IsValidModel) {
                response.SetItem(modelReq.ResponseItem);
                response.AddMessages(modelReq.Messages);
                return response;
            }

            //Authentication
            AuthModel authen = _authRepository.Authenticate(modelReq);

            if (authen == null) {
                response.SetItem(modelReq.ResponseItem);
                response.AddMessage("Não foi possível realizar a autenticação do usuário.");
                return response;
            }

            if (!authen.IsValidResponse) {
                response.SetItem(modelReq.ResponseItem);
                response.AddMessages(authen.Messages);
                return response;
            }

            //Authorization
            AuthModel author = _authRepository.Authorize(modelReq);

            if (author == null) {
                response.SetItem(modelReq.ResponseItem);
                response.AddMessage("Não foi possível realizar a autorização do usuário.");
                return response;
            }

            if (!author.IsValidResponse) {
                response.SetItem(author.ResponseItem);
                response.AddMessages(author.Messages);
                return response;
            }

            //Permission
            AuthModel perm = _authBusiness.Permit(author);

            if (perm == null) {
                response.SetItem(author.ResponseItem);
                response.AddMessage("Não foi possível verificar as permissões do usuário.");
                return response;
            }

            if (!perm.IsValidResponse) {
                response.SetItem(author.ResponseItem);
                response.AddMessages(perm.Messages);
                return response;
            }

            //Token
            string token = _tokenModel.GenerateToken(author.Entity);

            author.SetToken(token);
            response.SetItem(author.ResponseItem);
            response.IsValid = true;

            return response;
        }
    }
}

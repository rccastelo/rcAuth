using rcAuthData.Interfaces;
using rcAuthDomain.Entities;
using rcAuthDomain.Models;
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

        public AuthModel Authenticate(AuthModel authModel)
        {
            AuthModel modelRet = null;

            AuthEntity entity = _data.Authenticate(authModel.Entity);

            if (entity != null) {
                modelRet = new AuthModel(entity);
                modelRet.IsValidResponse = true;
            } else {
                modelRet = new AuthModel();
                modelRet.IsValidResponse = false;
                modelRet.AddMessage("Usuário e/ou senha inválido(s)");
            }

            return modelRet;
        }

        public AuthModel Authorize(AuthModel authModel)
        {
            AuthModel modelRet = null;

            AuthEntity entity = _data.Authorize(authModel.Entity);

            if (entity != null) {
                modelRet = new AuthModel(entity);
                modelRet.IsValidResponse = true;
            } else {
                modelRet = new AuthModel();
                modelRet.IsValidResponse = false;
                modelRet.AddMessage("Usuário não tem autorização para acessar este sistema");
            }

            return modelRet;
        }
    }
}

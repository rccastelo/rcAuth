using rcAuthData.Interfaces;
using rcAuthDomain.Entity;
using rcAuthDomain.Model;
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

        public AuthModel Login(AuthModel authModel)
        {
            AuthModel modelRet = null;

            AuthEntity entity = _data.Login(authModel.Item);

            if (entity != null) {
                modelRet = new AuthModel(entity);
                modelRet.IsValid = true;
            } else {
                modelRet = new AuthModel();
                modelRet.IsValid = false;
                modelRet.AddMessage("Usuário e/ou senha inválido(s)");
            }

            return modelRet;
        }
    }
}

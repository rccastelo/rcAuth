using Microsoft.EntityFrameworkCore;
using rcAuthData.Interfaces;
using rcAuthDomain.Entity;
using rcAuthDomain.Model;
using rcDbSqlServerEF;
using System.Linq;

namespace rcAuthData.DatasEF
{
    public class AuthDataEF : DataEF, IAuthData
    {
        public AuthDataEF(ManagerDbContext context) : base(context) { }

        public AuthModel Login(AuthModel authModel)
        {
            UserEntity userEntity = this._context.Set<UserEntity>().AsNoTracking().SingleOrDefault(
                et => ((et.Login == authModel.Login) && (et.Password == authModel.Password)));

            AuthModel authModelRet = new AuthModel() {
                Id = userEntity.Id,
                Login = userEntity.Login
            };

            return authModelRet;
        }
    }
}

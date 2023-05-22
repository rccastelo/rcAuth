using Microsoft.EntityFrameworkCore;
using rcAuthData.Interfaces;
using rcAuthDomain.Entities;
using rcDbSqlServerEF;
using System.Linq;

namespace rcAuthData.DatasEF
{
    public class AuthDataEF : DataEF, IAuthData
    {
        public AuthDataEF(ManagerDbContext context) : base(context) { }

        public AuthEntity Authenticate(AuthEntity entity)
        {
            AuthEntity auth = null;

            LoginEntity user = this._context.Set<LoginEntity>().AsNoTracking().SingleOrDefault(
                et => ((et.Login == entity.Login) && (et.Password == entity.Secret)));

            if (user != null) {
                auth = new AuthEntity() {
                    Id = user.Id,
                    Login = user.Login
                };
            }

            return auth;
        }

        public AuthEntity Authorize(AuthEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}

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

        public AuthEntity Login(AuthEntity entity)
        {
            AuthEntity auth = null;

            PasswordEntity user = this._context.Set<PasswordEntity>().AsNoTracking().SingleOrDefault(
                et => ((et.Login == entity.Login) && (et.Password == entity.Password)));

            if (user != null) {
                auth = new AuthEntity() {
                    Id = user.Id,
                    Login = user.Login
                };
            }

            return auth;
        }
    }
}

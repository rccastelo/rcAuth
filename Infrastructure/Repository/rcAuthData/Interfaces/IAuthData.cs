using rcAuthDomain.Entities;

namespace rcAuthData.Interfaces
{
    public interface IAuthData
    {
        AuthEntity Login(AuthEntity entity);
    }
}

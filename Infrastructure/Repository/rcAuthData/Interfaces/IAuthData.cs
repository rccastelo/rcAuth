using rcAuthDomain.Entity;

namespace rcAuthData.Interfaces
{
    public interface IAuthData
    {
        AuthEntity Login(AuthEntity entity);
    }
}

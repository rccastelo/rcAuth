using rcAuthDomain.Entities;

namespace rcAuthData.Interfaces
{
    public interface IAuthData
    {
        AuthEntity Authenticate(AuthEntity entity);
        AuthEntity Authorize(AuthEntity entity);
    }
}

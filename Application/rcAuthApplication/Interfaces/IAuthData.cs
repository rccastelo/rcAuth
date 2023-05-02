using rcAuthDomain;

namespace rcAuthApplication.Interfaces
{
    public interface IAuthData
    {
        AuthModel Login(AuthModel authModel);
    }
}

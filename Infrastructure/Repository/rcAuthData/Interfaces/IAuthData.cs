using rcAuthDomain;

namespace rcAuthData.Interfaces
{
    public interface IAuthData
    {
        AuthModel Login(AuthModel authModel);
    }
}

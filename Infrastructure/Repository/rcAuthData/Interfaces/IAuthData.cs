using rcAuthDomain.Model;

namespace rcAuthData.Interfaces
{
    public interface IAuthData
    {
        AuthModel Login(AuthModel authModel);
    }
}

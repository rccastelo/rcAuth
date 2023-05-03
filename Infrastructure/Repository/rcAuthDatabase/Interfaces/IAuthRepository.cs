using rcAuthDomain;

namespace rcAuthRepository.Interfaces
{
    public interface IAuthRepository
    {
        AuthModel Login(AuthModel authModel);
    }
}

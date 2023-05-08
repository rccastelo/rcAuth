using rcAuthDomain.Model;

namespace rcAuthRepository.Interfaces
{
    public interface IAuthRepository
    {
        AuthModel Login(AuthModel model);
    }
}

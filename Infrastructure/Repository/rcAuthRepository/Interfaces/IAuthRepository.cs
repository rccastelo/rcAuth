using rcAuthDomain.Models;

namespace rcAuthRepository.Interfaces
{
    public interface IAuthRepository
    {
        AuthModel Login(AuthModel model);
    }
}

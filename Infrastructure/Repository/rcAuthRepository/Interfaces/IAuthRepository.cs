using rcAuthDomain.Models;

namespace rcAuthRepository.Interfaces
{
    public interface IAuthRepository
    {
        AuthModel Authenticate(AuthModel model);
        AuthModel Authorize(AuthModel model);
    }
}

using rcAuthApplication.Application;

namespace rcAuthApplication.Interfaces
{
    public interface IAuthService
    {
        AuthResponse Login(AuthRequest authRequest);
    }
}

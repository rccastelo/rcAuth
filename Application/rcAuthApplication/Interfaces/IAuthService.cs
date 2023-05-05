using rcAuthApplication.Transport;

namespace rcAuthApplication.Interfaces
{
    public interface IAuthService
    {
        AuthResponse Login(AuthRequest authRequest);
    }
}

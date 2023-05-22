using rcAuthDomain.Models;

namespace rcAuthDomain.Interfaces
{
    public interface IAuthBusiness
    {
        AuthModel Permit(AuthModel authModel);
    }
}

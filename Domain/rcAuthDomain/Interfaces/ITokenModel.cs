using rcAuthDomain.Entities;

namespace rcAuthDomain.Interfaces
{
    public interface ITokenModel
    {
        string GenerateToken(AuthEntity authEntity);
    }
}

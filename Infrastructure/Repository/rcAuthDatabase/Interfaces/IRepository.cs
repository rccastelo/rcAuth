namespace rcAuthRepository.Interfaces
{
    public interface IRepository
    {
        Entity get<Entity>(string query);
    }
}

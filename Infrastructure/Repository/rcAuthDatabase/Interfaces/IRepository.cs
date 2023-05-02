namespace rcAuthRepository.Interfaces
{
    public interface IRepository
    {
        Entity Get<Entity>(string query);
    }
}

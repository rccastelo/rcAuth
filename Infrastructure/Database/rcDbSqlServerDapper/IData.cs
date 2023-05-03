namespace rcDbSqlServerDapper
{
    public interface IData
    {
        Entity Get<Entity>(string query);
    }
}

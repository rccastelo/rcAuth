namespace rcDbSqlServerEF
{
    public class DataEF
    { 
        protected readonly ManagerDbContext _context;

        public DataEF(ManagerDbContext context)
        {
            this._context = context;
        }
    }
}

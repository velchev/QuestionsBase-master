namespace Repository
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class DbContextFactory : IDbContextFactory<DbContext>
    {
        public DbContext Create()
        {
            return new QuestionBaseDbContext();
        }
    }
}

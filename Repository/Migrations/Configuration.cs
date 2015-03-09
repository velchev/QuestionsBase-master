namespace Repository.Migrations
{
    using System.Data.Entity.Migrations;
    using Repository.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.QuestionBaseDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuestionBaseDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.QuestionType.Add(new QuestionTypeEntity{Type = "MVC"});
            context.QuestionType.Add(new QuestionTypeEntity { Type = "OOP" });
            //
        }
    }
}

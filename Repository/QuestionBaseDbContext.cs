namespace Repository
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Entities;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public interface IQuestionBaseContext : IContext
    {
        IDbSet<QuestionEntity> Question { get; set; }
        IDbSet<QuestionTypeEntity> QuestionType { get; set; } 
    }
    public class QuestionBaseDbContext : BaseContext<QuestionBaseDbContext>, IQuestionBaseContext
    {
        //public QuestionBaseDbContext()
        //    : base("QuestionBaseConnectionString")
        //{

        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer<QuestionBaseDbContext>(null);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.HasDefaultSchema("dbo");

   

            modelBuilder.Entity<QuestionEntity>().ToTable("Question");
            modelBuilder.Entity<QuestionEntity>().HasKey(c => c.Id);
            modelBuilder.Entity<QuestionEntity>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<QuestionEntity>().Property(p => p.Title).HasColumnType("NVARCHAR").IsRequired();


            modelBuilder.Entity<QuestionTypeEntity>().ToTable("QuestionType");
            modelBuilder.Entity<QuestionTypeEntity>().HasKey(c => c.Id);
            modelBuilder.Entity<QuestionTypeEntity>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<QuestionEntity>().HasRequired(c => c.QuestionType).WithMany(c => c.QuestionEntities);
        }

        public IDbSet<QuestionEntity> Question { get; set; }
        public IDbSet<QuestionTypeEntity> QuestionType { get; set; }
        public void SetModified(object entity)
        {
            throw new System.NotImplementedException();
        }

        public void SetAdd(object entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
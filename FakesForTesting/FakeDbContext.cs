namespace FakesForTesting
{
    using System.Data.Entity;
    using Repository;
    using Repository.Entities;

    public class QuestionBaseFakeDbContext : IQuestionBaseContext
    {
        public IDbSet<QuestionEntity> Question { get; set; }
        public IDbSet<QuestionTypeEntity> QuestionType { get; set; }
        public void Dispose()
        {
          
        }

        public int SaveChanges()
        {
            throw new System.NotImplementedException();
        }

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

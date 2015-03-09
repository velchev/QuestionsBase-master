namespace Repository
{
    using System.Collections.Generic;
    using Repository.Entities;

    public interface IQuestionRepository
    {
        IEnumerable<QuestionEntity> GetAll();
        
        QuestionEntity FindById(int? id);
        
        void Save(QuestionEntity questionEntity);
        
        void Remove(QuestionEntity questionEntity);
        
        IEnumerable<int> GetAllDifficultyLevels();
    }
}
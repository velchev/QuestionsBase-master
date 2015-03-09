namespace QuestionBase.Services.Services
{
    using Repository.Entities;
    using System.Collections.Generic;

    public interface IQuestionService
    {
        IEnumerable<QuestionEntity> GetAllQuestions();

        QuestionEntity FindById(int? id);

        void Save(QuestionEntity question);

        void Remove(QuestionEntity questionEntity);

        IEnumerable<int> GetAllDifficultyLevels();
    }
}

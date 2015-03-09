namespace QuestionBase.Services
{
    using System.Collections.Generic;
    using QuestionBase.Services.Services;
    using Repository;
    using Repository.Entities;

    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repository;
        
        public QuestionService(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<QuestionEntity> GetAllQuestions()
        {
            return _repository.GetAll();
        }

        public IEnumerable<int> GetAllDifficultyLevels()
        {
            return _repository.GetAllDifficultyLevels();
        }

        public QuestionEntity FindById(int? id)
        {
            return _repository.FindById(id);
        }

        public void Save(QuestionEntity question)
        {
            _repository.Save(question);
        }

        public void Remove(QuestionEntity questionEntity)
        {
            _repository.Remove(questionEntity);
        }
    }
}
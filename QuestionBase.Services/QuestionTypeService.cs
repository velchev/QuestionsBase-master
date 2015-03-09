namespace QuestionBase.Services
{
    using System.Collections.Generic;
    using Services;
    using Repository;
    using Repository.Entities;

    public class QuestionTypeService : IQuestionTypeService
    {
        private readonly IQuestionTypeRepository _repository;

        public QuestionTypeService(IQuestionTypeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<QuestionTypeEntity> GetAllQuestionTypes()
        {
            return _repository.GetAll();
        }

        public void Save(QuestionTypeEntity questionType)
        {
            _repository.Save(questionType);
        }

        public QuestionTypeEntity FindById(int? id)
        {
            return _repository.FindById(id);
        }

        public void Remove(QuestionTypeEntity questionTypeEntity)
        {
            _repository.Remove(questionTypeEntity);
        }
    }
}
namespace Repository.Repositories
{
    using System.Linq;
    using Repository.Entities;
    using RepositoryCommon;
    using System.Collections.Generic;

    public class QuestionTypeRepository : IQuestionTypeRepository
    {
        private readonly IRepositoryBase<QuestionTypeEntity> _repository;

        public QuestionTypeRepository(IRepositoryBase<QuestionTypeEntity> repository)
        {
            _repository = repository;
        }

        public IEnumerable<QuestionTypeEntity> GetAll()
        {
            return _repository.GetAllQueryable().ToList();
        }

        public void Save(QuestionTypeEntity questionTypeEntity)
        {
            if (questionTypeEntity.Id == 0)
                _repository.AddAndSave(questionTypeEntity);
            else
            {
                if (_repository.Update(questionTypeEntity))
                {
                    _repository.Save();
                }
            }
        }

        public QuestionTypeEntity FindById(int? id)
        {
            return _repository.Get(x => x.Id == id, null, "QuestionEntities").FirstOrDefault();
        }

        public void Remove(QuestionTypeEntity questionTypeEntity)
        {
            _repository.Delete(questionTypeEntity);

            _repository.Save();
        }
    }
}
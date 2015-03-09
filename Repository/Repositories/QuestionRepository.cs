namespace Repository.Repositories
{
    using System.Linq;
    using Entities;
    using RepositoryCommon;
    using System.Collections.Generic;

    public class QuestionRepository : IQuestionRepository
    {
        private readonly IRepositoryBase<QuestionEntity> _repository;

        public QuestionRepository(IRepositoryBase<QuestionEntity> repository)
        {
            _repository = repository;
        }

        public IEnumerable<QuestionEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public QuestionEntity FindById(int? id)
        {
            return _repository.GetById(id);
        }

        public void Save(QuestionEntity questionEntity)
        {
            if (questionEntity.Id == 0)
            {
                _repository.AddAndSave(questionEntity);
            }
            else
            {
                if(_repository.Update(questionEntity))
                {
                    _repository.Save();
                }
            }
        }

        public void Remove(QuestionEntity questionEntity)
        {
            _repository.Delete(questionEntity);
            
            _repository.Save();
        }

        public IEnumerable<int> GetAllDifficultyLevels()
        {
            var possibleDifficultyLevels = new HashSet<int>();

            var query = _repository.GetAll();
            foreach (var questionEntity in query)
            {
                if(!possibleDifficultyLevels.Contains(questionEntity.DifficultyLevel))
                {
                    possibleDifficultyLevels.Add(questionEntity.DifficultyLevel);
                }
            }

            IEnumerable<int> hList = possibleDifficultyLevels.ToList().OrderBy(x=>x);

            return hList;
        }
    }
}
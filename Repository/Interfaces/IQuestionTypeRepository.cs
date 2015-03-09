
namespace Repository
{
    using Repository.Entities;
    using System.Collections.Generic;

    public interface IQuestionTypeRepository
    {
        IEnumerable<QuestionTypeEntity> GetAll();

        void Save(QuestionTypeEntity questionTypeEntity);

        QuestionTypeEntity FindById(int? id);

        void Remove(QuestionTypeEntity questionTypeEntity);
    }
}
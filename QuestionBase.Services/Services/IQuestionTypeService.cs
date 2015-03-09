namespace QuestionBase.Services.Services
{
    using Repository.Entities;
    using System.Collections.Generic;

    public interface IQuestionTypeService
    {
        IEnumerable<QuestionTypeEntity> GetAllQuestionTypes();
        
        void Save(QuestionTypeEntity questionType);
        
        QuestionTypeEntity FindById(int? id);

        void Remove(QuestionTypeEntity questionTypeEntity);
    }
}

namespace Repository.Test
{
    using NUnit.Framework;
    using QuestionBase.Services;
    using Repository.Entities;
    using Repository.Repositories;
    using RepositoryCommon;
    using Rhino.Mocks;

    [TestFixture]
    public class QuestionRepositoryTest
    {
        private QuestionRepository _questionRepository;
        [SetUp]
        public void Setup()
        {
            _questionRepository = MockRepository.GenerateMock<QuestionRepository>();

        }

        //[TestCase]
        //public void ConstructorCreateNotNullInstance()
        //{
        //    var question = new QuestionEntity { Id = 1, Title = "QuestionTitle"};
        //    _questionRepository.Expect(s => s.GetAll().GetById(1)).Return(question);

        //    var result =_repository.GetById(1);

        //    Assert.IsNotNull(_repository);
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.Title, "QuestionTitle");
        //}

        //[TestCase]
        //public void Save_CountryEntry()
        //{
        //    var question = new QuestionEntity { Id = 1, Title = "QuestionTitle"};
        //    _repository.Expect(s => s.GetById(1)).Return(question);

        //    //var target = new QuestionService(_questionRepository).GetById(1);
            

        //    //Assert.AreEqual(target.Title, "QuestionTitle");


        //    _repository.VerifyAllExpectations();
        //}


    }
}

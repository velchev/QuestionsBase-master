using NUnit.Framework;
namespace QuestionsBase.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Controllers;
    using Moq;
    using QuestionBase.Services.Services;
    using Repository.Entities;
    using Rhino.Mocks;
    using TestStack.FluentMVCTesting;
    using MockRepository = Rhino.Mocks.MockRepository;

    [TestFixture]
    public class QuestionTypeControllerTests
    {
        private IQuestionTypeService _questionTypeService;
        private QuestionTypeController _controller;
        private List<QuestionTypeEntity> _predefinedQuestionTypes;

      
        [SetUp]
        public void Setup()
        {
            _predefinedQuestionTypes = new List<QuestionTypeEntity>();
            _predefinedQuestionTypes.Add(new QuestionTypeEntity
            {
                Id = 1,
                Type = "QuestionType1"
            });
            _predefinedQuestionTypes.Add(new QuestionTypeEntity
            {
                Id = 2,
                Type = "QuestionType2"
            });


            _questionTypeService = MockRepository.GenerateMock<IQuestionTypeService>();
            _controller = new QuestionTypeController(_questionTypeService);

            _questionTypeService.Expect(x => x.GetAllQuestionTypes()).Return(_predefinedQuestionTypes);
            _questionTypeService.Expect(x => x.FindById(1)).Return(_predefinedQuestionTypes[0]);
        }


        [Test]
        public void ShouldRenderDefaultView()
        {
            _controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView()
                .WithModel<IList<QuestionTypeEntity>>(x => x.Where(f => f.Type.Contains("Quest")));

            _controller.WithCallTo(x => x.Create()).ShouldRenderDefaultView();

        }

        [Test]
        public void ShouldReturnDefaultViewOfCreateIfModelIsInValid()
        {
            var questionTypeFake = new QuestionTypeEntity();
            questionTypeFake.Id = -1;
            questionTypeFake.Type = "QuestionType1";
            
            _controller.ModelState.AddModelError("test1", new Exception());
            _controller.WithCallTo(x => x.Create(questionTypeFake)).ShouldRenderDefaultView();
        }


        [Test]
        public void ShouldRedirectToIndexActionIfModelIsValid()
        {
            var questionTypeFake = new QuestionTypeEntity();
            questionTypeFake.Id = -1;
            questionTypeFake.Type = "QuestionType1";

            _controller.WithCallTo(x => x.Create(questionTypeFake)).ShouldRedirectTo(x => x.Index);
        }

        [Test]
        public void DeleteConfirmedRedirectsToIndexAction()
        {
            _controller.WithCallTo(x => x.DeleteConfirmed(It.IsAny<int>())).ShouldRedirectTo(x => x.Index);
        }

        [Test]
        public void DeleteActionReturnsBadRequestIfNoIdSent()
        {
            _controller.WithCallTo(x=>x.Delete(null)).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeleteActionReturnsNotFoundIfItemWithIdNotExists()
        {
            _controller.WithCallTo(x => x.Delete(100)).ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [Test]
        public void DeleteActionReturnsDefaultViewIfItemWithIdExists()
        {
            _controller.WithCallTo(x => x.Delete(1)).ShouldRenderDefaultView().WithModel<QuestionTypeEntity>();
        }
    }
}

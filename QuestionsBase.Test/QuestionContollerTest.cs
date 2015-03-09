namespace QuestionsBase.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Moq;
    using NUnit.Framework;
    using QuestionBase.Services.Services;
    using Controllers;
    using Repository.Entities;
    using Rhino.Mocks;
    using Rhino.Mocks.Constraints;
    using TestStack.FluentMVCTesting;
    using MockRepository = Rhino.Mocks.MockRepository;

    [TestFixture]
    public class QuestionContollerTest
    {
        private IQuestionService _questionService;
        private IQuestionTypeService _questionTypeService;
        QuestionController _target;
        private new List<QuestionTypeEntity> _questionTypes;
        private new List<QuestionEntity> _questions;

        private const string QueryStringSearch = "aaa";
        private const int QueryStringQuestionType = 1;

        [SetUp]
        public void Setup()
        {
            _questionService = MockRepository.GenerateMock<IQuestionService>();
            _questionTypeService = MockRepository.GenerateMock<IQuestionTypeService>();
            _target = new QuestionController(_questionService, _questionTypeService);



            _questionTypes = new List<QuestionTypeEntity>
                    {
                        new QuestionTypeEntity {Id = 1, Type = "MVC"},
                        new QuestionTypeEntity {Id = 2, Type = "OOP"}
                    };
            _questions = new List<QuestionEntity>
                                {
                                    new QuestionEntity
                                        {
                                            Id = 1,
                                            Title = "Question1",
                                            DifficultyLevel = 2,
                                            Answer = "Answer1",
                                            QuestionTypeEntityId = 2
                                        },
                                    new QuestionEntity
                                        {
                                            Id = 2,
                                            Title = "Question2",
                                            DifficultyLevel = 1,
                                            Answer = "Answer2",
                                            QuestionTypeEntityId = 1
                                        }
                                };

            _questionService.Expect(s => s.GetAllQuestions()).Return(_questions);
            _questionTypeService.Expect(s => s.FindById(1)).Return(_questionTypes[0]);
            _questionTypeService.Expect(s => s.GetAllQuestionTypes()).Return(_questionTypes);

            _questionService.Expect(s => s.FindById(1)).Return(_questions[0]);
        }


        [Test]
        public void ShouldRecirectToFilderAction()
        {
            var contextMock = new Mock<ControllerContext>();
            var mockHttpContext = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();

            session.Setup(ctx => ctx["search"]).Returns(QueryStringSearch);
            session.Setup(ctx => ctx["qtype"]).Returns(QueryStringQuestionType);

            mockHttpContext.Setup(ctx => ctx.Session).Returns(session.Object);
            contextMock.Setup(ctx => ctx.HttpContext).Returns(mockHttpContext.Object);

            _target.ControllerContext = contextMock.Object;

            _target.WithCallTo(x => x.Index()).ShouldRedirectTo(x => x.Filter(QueryStringQuestionType, QueryStringSearch));
        }

        [Test]
        public void ShouldRenderDefaultView()
        {
            var contextMock = new Mock<ControllerContext>();
            var mockHttpContext = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();

            session.Setup(ctx => ctx["search"]).Returns(QueryStringSearch);

            mockHttpContext.Setup(ctx => ctx.Session).Returns(session.Object);
            contextMock.Setup(ctx => ctx.HttpContext).Returns(mockHttpContext.Object);

            _target.ControllerContext = contextMock.Object;

            _target.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }


        [TestCase]
        public void ConstructorReturnNotNullInstance()
        {
            var contextMock = new Mock<ControllerContext>();
            var mockHttpContext = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();


            mockHttpContext.Setup(ctx => ctx.Session).Returns(session.Object);
            contextMock.Setup(ctx => ctx.HttpContext).Returns(mockHttpContext.Object);

            _target.ControllerContext = contextMock.Object;

            _target.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        [TestCase]
        public void IndexReturnsNotNullView()
        {
            var result = _target.Index();
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(view.Model);

            Assert.AreEqual(2, ((IEnumerable<QuestionEntity>)view.Model).Count());
        }


        [TestCase]
        public void FilterReturnsNotNullView()
        {
            var result = _target.Filter(1);
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(view.Model);

            Assert.AreEqual(1, ((IEnumerable<QuestionEntity>)view.Model).Count());
        }

        [TestCase]
        public void FilterWithNoParameterReturnsNotNullView()
        {
            var result = _target.Filter();
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(view.Model);

            Assert.AreEqual(2, ((IEnumerable<QuestionEntity>)view.Model).Count());
        }

        [TestCase]
        public void DetailsReturnsOneItemTest()
        {
            var result = _target.Details(1);
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(view.Model);

            Assert.AreEqual(1, ((QuestionEntity)view.Model).Id);
        }

        [TestCase]
        public void CreateNotNull()
        {
            var result = _target.Create();
            var view = result as ViewResult;

            Assert.NotNull(view);
        }



        [Test]
        public void Check_For_Default_View_Or_Action()
        {
            _target.WithCallTo(x => x.Create()).ShouldRenderDefaultView();
            _target.WithCallTo(x => x.Details(0)).ShouldGiveHttpStatus(HttpStatusCode.NotFound);
            _target.WithCallTo(x => x.Details(1)).ShouldRenderDefaultView();

            int? aa = null;
            _target.WithCallTo(x=>x.Edit(aa)).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);

            _target.WithCallTo(x => x.Edit(-1)).ShouldGiveHttpStatus(HttpStatusCode.NotFound);
            _target.WithCallTo(x => x.Edit(1)).ShouldRenderDefaultView();


            _target.WithCallTo(x => x.Delete(aa)).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
            _target.WithCallTo(x => x.Delete(-1)).ShouldGiveHttpStatus(HttpStatusCode.NotFound);
            _target.WithCallTo(x => x.Delete(1)).ShouldRenderDefaultView();
        }
    }
}

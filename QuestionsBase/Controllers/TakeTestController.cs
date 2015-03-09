namespace QuestionsBase.Controllers
{
    using System.Linq;
    using QuestionBase.Services.Services;
    using System.Web.Mvc;
    using Repository.Entities;

    public class TakeTestController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IQuestionTypeService _questionTypeService;

        public TakeTestController(IQuestionService questionService, IQuestionTypeService questionTypeService)
        {
            _questionService = questionService;
            _questionTypeService = questionTypeService;
        }

        public ActionResult Index()
        {
            ViewBag.QuestionTypeEntityId = new SelectList(_questionTypeService.GetAllQuestionTypes(), "Id", "Type");

            ViewBag.DifficultyLevel = new SelectList(_questionService.GetAllDifficultyLevels());


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "QuestionTypeEntityId, DifficultyLevel")] QuestionEntity questionentity)
        {
            var difficultLevel = questionentity.DifficultyLevel;

            return RedirectToAction("Slide", "TakeTest", new { questionTypeId = questionentity.QuestionTypeEntityId, number = 1, difficultyLevel = difficultLevel });
        }

        [HttpGet]
        public ActionResult Slide(int questionTypeId, int number, int difficultyLevel)
        {
            var questions = _questionService.GetAllQuestions().Where(x => x.QuestionTypeEntityId == questionTypeId || questionTypeId == 0).OrderBy(x => x.Id).Where(x => x.DifficultyLevel >= difficultyLevel).ToList();

            var question = questions.Skip(number - 1).Take(1).FirstOrDefault();

            ViewBag.QuestionTypeId = questionTypeId;
            ViewBag.Number = number;
            ViewBag.DifficultyLevel = difficultyLevel;
            ViewBag.TotalNumber = questions.Count();

            return View(question);
        }

        [HttpPost]
        public ActionResult Next(int questionTypeId, int number, int totalNumber, int difficultyLevel)
        {
            if (number + 1 > totalNumber)
                number = 1;
            else
                number++;

            return RedirectToAction("Slide", "TakeTest", new { questionTypeId = questionTypeId, number = number, difficultyLevel = difficultyLevel });
        }
    }
}
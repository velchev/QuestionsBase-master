namespace QuestionsBase.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Models;
    using QuestionBase.Services.Services;
    using Repository.Entities;

    [Authorize]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IQuestionTypeService _questionTypeService;

        public QuestionController(IQuestionService questionService, IQuestionTypeService questionTypeService)
        {
            _questionService = questionService;
            _questionTypeService = questionTypeService;
        }


        // GET: /Question/
        public ActionResult Index()
        {
            var searcQuery = string.Empty;
            if (Session["search"] != null)
            {
                searcQuery = (string)Session["search"];
            }

            ViewBag.Search = searcQuery;

            if (Session["qtype"] != null)
            {
                var questionType = (int)Session["qtype"];

                return RedirectToAction("Filter", new { questionTypeId = questionType, search = searcQuery });
            }

            ViewBag.QuestionTypeEntityId = new SelectList(_questionTypeService.GetAllQuestionTypes(), "Id", "Type");

            var question = _questionService.GetAllQuestions().OrderBy(x => x.Id);
            //ViewBag.QuestionTypes = _questionTypeService.GetAllQuestionTypes();
            return View(question.ToList());
        }



        public ActionResult Filter(int questionTypeId = -1, string search = "")
        {
            Session["qtype"] = questionTypeId;
            Session["search"] = search;

            ViewBag.QuestionTypeEntityId = new SelectList(_questionTypeService.GetAllQuestionTypes(), "Id", "Type", questionTypeId);

            var questionsList = _questionService.GetAllQuestions();

            if (questionTypeId != -1)
                questionsList = questionsList.Where(x => x.QuestionTypeEntityId == questionTypeId);

            if (!string.IsNullOrEmpty(search))
            {
                questionsList = questionsList.Where(x => (x.Title != null && x.Title.ToLower().Contains(search.ToLower())) || (x.Answer != null && x.Answer.ToLower().Contains(search.ToLower()))).ToList();
            }

            ViewBag.Search = search;

            return View("Index", questionsList);
        }

        public ActionResult NavbarSearch(SearchModel model)
        {
            var a = new RouteValueDictionary();

            a.Add("search", model.SearchString);

            return RedirectToAction("Filter", a);
        }

        // GET: /Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuestionEntity questionentity = _questionService.FindById(id);

            if (questionentity == null)
            {
                return HttpNotFound();
            }
            return View(questionentity);
        }

        // GET: /Question/Create
        public ActionResult Create()
        {
            ViewBag.QuestionTypeEntityId = new SelectList(_questionTypeService.GetAllQuestionTypes(), "Id", "Type");

            return View();
        }

        // POST: /Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id, Title, Answer, QuestionTypeEntityId, DifficultyLevel")] QuestionEntity questionentity)
        {
            if (ModelState.IsValid)
            {
                _questionService.Save(questionentity);

                return RedirectToAction("Index");
            }

            ViewBag.QuestionTypeEntityId = new SelectList(_questionTypeService.GetAllQuestionTypes(), "Id", "Type", questionentity.QuestionTypeEntityId);

            return View(questionentity);
        }

        // GET: /Question/Edit/5
        [ValidateInput(false)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuestionEntity questionentity = _questionService.FindById(id);
            if (questionentity == null)
            {
                return HttpNotFound();
            }

            ViewBag.QuestionTypeEntityId = new SelectList(_questionTypeService.GetAllQuestionTypes(), "Id", "Type", questionentity.QuestionTypeEntityId);

            return View(questionentity);
        }

        // POST: /Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Title,Answer,QuestionTypeEntityId,DifficultyLevel")] QuestionEntity questionentity)
        {
            if (ModelState.IsValid)
            {
                var itemToUpdate = _questionService.FindById(questionentity.Id);
                itemToUpdate.Answer = questionentity.Answer;
                itemToUpdate.DifficultyLevel = questionentity.DifficultyLevel;
                itemToUpdate.QuestionTypeEntityId = questionentity.QuestionTypeEntityId;
                itemToUpdate.Title = questionentity.Title;

                _questionService.Save(itemToUpdate);

                return RedirectToAction("Index");
            }

            ViewBag.QuestionTypeEntityId = new SelectList(_questionTypeService.GetAllQuestionTypes(), "Id", "Type", questionentity.QuestionTypeEntityId);

            return View(questionentity);
        }

        // GET: /Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuestionEntity questionentity = _questionService.FindById(id);
            if (questionentity == null)
            {
                return HttpNotFound();
            }
            return View(questionentity);
        }

        // POST: /Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionEntity questionentity = _questionService.FindById(id);

            _questionService.Remove(questionentity);

            return RedirectToAction("Index");
        }
    }
}

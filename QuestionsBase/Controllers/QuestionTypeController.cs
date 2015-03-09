namespace QuestionsBase.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Repository.Entities;
    using QuestionBase.Services.Services;

    public class QuestionTypeController : Controller
    {
        private readonly IQuestionTypeService _questionTypeService;

        public QuestionTypeController(IQuestionTypeService questionTypeService)
        {
            _questionTypeService = questionTypeService;
        }

        // GET: /QuestionType/
        public ActionResult Index()
        {
            return View(_questionTypeService.GetAllQuestionTypes());
        }

        // GET: /QuestionType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuestionTypeEntity questiontypeentity = _questionTypeService.FindById(id);

            if (questiontypeentity == null)
            {
                return HttpNotFound();
            }
            return View(questiontypeentity);
        }

        // GET: /QuestionType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /QuestionType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Type")] QuestionTypeEntity questiontypeentity)
        {
            if (ModelState.IsValid)
            {
                _questionTypeService.Save(questiontypeentity);

                return RedirectToAction("Index");
            }

            return View(questiontypeentity);
        }

        // GET: /QuestionType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            QuestionTypeEntity questiontypeentity = _questionTypeService.FindById(id); //db.QuestionType.Find(id);
            
            if (questiontypeentity == null)
            {
                return HttpNotFound();
            }

            return View(questiontypeentity);
        }

        // POST: /QuestionType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Type")] QuestionTypeEntity questiontypeentity)
        {
            if (ModelState.IsValid)
            {
                var itemToUpdate = _questionTypeService.FindById(questiontypeentity.Id);
                itemToUpdate.Type = questiontypeentity.Type;

                _questionTypeService.Save(itemToUpdate);

                return RedirectToAction("Index");
            }
            return View(questiontypeentity);
        }

        // GET: /QuestionType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuestionTypeEntity questiontypeentity = _questionTypeService.FindById(id);
            
            if (questiontypeentity == null)
            {
                return HttpNotFound();
            }

            return View(questiontypeentity);
        }

        // POST: /QuestionType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionTypeEntity questiontypeentity = _questionTypeService.FindById(id);
            
            _questionTypeService.Remove(questiontypeentity);

            return RedirectToAction("Index");
        }
    }
}
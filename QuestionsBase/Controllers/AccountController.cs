using System.Web.Mvc;

namespace QuestionsBase.Controllers
{
    using System.Web.Security;
    public class LoginViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class AccountController : Controller
    {
        //
        // GET: /Admin/Account/

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult LogOn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("LogOn");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        public ActionResult LogOn(LoginViewModel login,
            string returnUrl)
        {
            if (!ValidateLogOn(login.Username, login.Password))
                return View("LogOn");

            FormsAuthentication.SetAuthCookie(login.Password, false);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");

        }

        private bool ValidateLogOn(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
                ModelState.AddModelError("username", "User name required");

            if (string.IsNullOrEmpty(password))
                ModelState.AddModelError("password", "Password required");

            if (ModelState.IsValid && !FormsAuthentication.
                Authenticate(userName, password))
                ModelState.AddModelError("_FORM", "Wrong user name or password");

            return ModelState.IsValid;
        }

        public RedirectToRouteResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LogOn");
        }
    }
}
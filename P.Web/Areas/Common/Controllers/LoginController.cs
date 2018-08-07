using System;
using System.Web.Mvc;
using P.Common.Exceptions;
using P.Model;
using P.Model.ViewModel;
using P.Service.Interface;


namespace P.Web.Areas.Common.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Common/Login/

        public ActionResult Index()
        {
            return View();
        }
        private readonly IAccountService _AccountService;
        public LoginController(IAccountService accountService)
        {
            this._AccountService = accountService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//防止XSS攻击
        public ActionResult Index(LoginVM loginVM)
        {
            try
            {
                Result result = _AccountService.Login(loginVM);
                string msg = result.Message ?? result.ResultType.GetDescription();
                if (result.ResultType == ResultType.Success)
                {
                    //  return Redirect(Url.Action("Index", "Home"));
                    return  RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", msg);
                return View(loginVM);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e);
                return View(loginVM);
            }
        }

        public ActionResult Logout()
        {
            _AccountService.Logout();
            return RedirectToAction("Index", "Home");
        }


    }
}

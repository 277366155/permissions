using P.Web.Extension.Filters;
using System.Web.Mvc;

namespace P.Web.Areas.Common.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Layout]
        public ActionResult Index()
        {
            return View();
        }

    }
}

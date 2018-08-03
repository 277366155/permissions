using System.Web.Mvc;

namespace P.Web.Extension.Filters
{
    public class IsAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                filterContext.Result = new RedirectResult("/Login/Index", true);
            }
        }
    }
}
using System;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class SharedViewBagAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public string MyProperty { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "Your application description page.";
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public void OnException(ExceptionContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}
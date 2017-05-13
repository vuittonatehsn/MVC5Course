using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public abstract class BaseController: Controller
    {
        public ActionResult Debug()
        {
            return Content("hi");
        }
        
        //protected override void HandleUnknownAction(string actionName)
        //{
        //    this.RedirectToAction("Index").ExecuteResult(this.ControllerContext);
        //}
    }
}
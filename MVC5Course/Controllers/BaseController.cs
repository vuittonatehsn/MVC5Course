using MVC5Course.Attribute;
using MVC5Course.Models;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public abstract class BaseController: Controller
    {
        protected FabricsEntities db = new FabricsEntities();

        [LocalOnly]
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
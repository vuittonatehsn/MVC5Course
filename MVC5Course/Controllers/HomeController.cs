using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            View("About").ExecuteResult(this.ControllerContext);//這時候就去執行About View 頁面
            View("About").ExecuteResult(this.ControllerContext);
            var viewName = "About";
            string result;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                result =  sw.GetStringBuilder().ToString();
            }


            //return new ViewResult { };
            
            return View();
        }
        public ActionResult PartialView()
        {
            //在頁面用F12的Console去跑這段ajax語法call 這個method。 $.get('/Home/PartialView', function(data) {alert(data);});
            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }else
            {
                return View("About");
            }
  
        }

        public ActionResult SomeAction()
        {
            return View("SuccessRedirect", "/");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
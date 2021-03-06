﻿using MVC5Course.Attribute;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
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
            return PartialView("SuccessRedirect", "/Home/SomeAction");
        }


        public ActionResult GetFile()
        {
            return File(Server.MapPath("~/Content/download/build2017.jpg"), "image/png", "newName.jpg");
        }

        public ActionResult GetJson()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return Json(this.db.Product.Take(2), JsonRequestBehavior.AllowGet);
        }


        [SharedViewBag]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        //[LocalOnly]
        [HandleError(ExceptionType= typeof(DbUpdateException), View = "SqlError")]
        public ActionResult Contact()
        {
            throw new DbUpdateException("hihi it is time to pee!");
            //ViewBag.Message = "Your contact page.";
            //throw new Exception("testing");
            //throw new ArgumentException("Error Handled!!");

            //return View();
        }

        public ActionResult Test()
        {
            throw new ArgumentException("Error Handled!!");
            //return View();
        }

        public ActionResult VT()
        {
            ViewBag.IsEnabled = true;
            return View();
        }

        public ActionResult VTest()
        {
            var data = new int[] { 1, 2, 3, 4, 5 };
            return PartialView(data);
        }
    }
}
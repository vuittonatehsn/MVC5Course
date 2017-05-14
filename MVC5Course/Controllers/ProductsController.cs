using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        private ProductRepository repo = RepositoryHelper.GetProductRepository();
        // GET: Products
        public ActionResult Index(bool? active = true)
        {
            var queryable = repo.GetAll取得十筆資料(active, showAll: false);//很特別的用法showAll重頭到尾都沒有被new出來，這是.net的機制，叫做:具名參數

            ViewData.Model = queryable;  //等於 View(queryable)
                                         //var queryable = db.Product.Where(r=>r.Active.HasValue & r.Active==active).OrderByDescending(x => x.ProductId).Take(10);

            ViewBag.qqq = ""; //ViewBag 吃多4個byte than ViewData

            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id = 133)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.GetById(id.Value);
            
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                //db.Product.Add(product);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.All().FirstOrDefault(r => r.ProductId == id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = repo.GetById(id);
            if (TryUpdateModel(product, new string[] { "ProductName", "Price" }))//model binding
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

     
            //if (ModelState.IsValid)
            //{
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
               
            //}
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            Product product = repo.GetById(id);
            
            

            //product.IsDeleted = true;改寫在Repository 層，這樣其他頁也可以用
            //repo.Delete(product);
            repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        

        //mvcaction

        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                TempData["ProductResult"] = "商品成功拉拉";
                return RedirectToAction("Index");
            }

            return View();
        }
        //[AjaxOnly]
        public ActionResult ListProduct(SearchRequest model)
           //(string searchKey = "z", int? StockRangeStart= 0, int? StockRangeEnd = 99999)
        {
         

            //return RedirectToAction("ListProduct");
            
            return View(GetProductListBySearch(model));
        }
        [HttpPost]
        public ActionResult BatchUpdate(SearchRequest model, ProductBatchVM[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var s in items)
                {
                    var p = db.Product.Find(s.ProductId);
                    p.Price = s.Price.Value;
                    p.Stock = s.Stock.Value;
                }
                db.SaveChanges();
                return RedirectToAction("ListProduct");

            }

            IQueryable<ProductViewModel> resultData = GetProductListBySearch(model);

            return View("ListProduct", resultData);
        }

        private IQueryable<ProductViewModel> GetProductListBySearch(SearchRequest model)
        {
            var data = repo.GetAll取得十筆資料(true);
            //if (ModelState.IsValid)
            //{

                data = data.Where(w => w.ProductName.Contains(model.searchKey)
                                        && w.Stock > model.stockRangeStart
                                        && w.Stock < model.stockRangeEnd);

            //}

            var resultData = data.Select(w => new ProductViewModel
            {
                ProductId = w.ProductId,
                ProductName = w.ProductName,
                Price = w.Price,
                Active = w.Active,
                Stock = w.Stock
                

            });
            return resultData;
        }


    }
}

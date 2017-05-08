using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            var all = db.Product.AsQueryable();
            //IEnumerable: 會存取DB
            //IQueryable: 不會存取DB

            var data = all.Where(w => w.Active == true && w.IsDeleted!=true).OrderByDescending(e=>e.ProductName).Take(10);
            var data1 = all.Where(p => p.ProductId == 1); //會存取DB
            //var data2 = all.FirstOrDefault(p => p.ProductId == 1); // 會存取DB
            var data2_2 = db.Product.FirstOrDefault(p => p.ProductId == 1);
            var data3 = db.Product.Find(1); //會存取DB

            //效能比較
            //db.Product.Count(r => r.Price > 10);//效能最快
            //db.Product.Where(r => r.Price > 10).ToList().Count();//先取到>10所有，在count 資料庫
            //db.Product.Where(r => r.Price > 10).Count();//sql直接count全部


            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            //var p = db.Product.Find(id);
            //db.Database.ExecuteSqlCommand("select * from Product");
            var p = db.Database.SqlQuery<Product>("SELECT * FROM dbo.Product WHERE ProductId = @p0", id).FirstOrDefault();

            return View(p);
        }
        public ActionResult edit(int id)
        {
            var p = db.Product.Find(id);
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product p )
        {
            var product = db.Product.Find(id);
            if(product != null && ModelState.IsValid)
            {
                product.ProductName = p.ProductName;
                product.Price = p.Price;
                product.Stock = p.Stock;
                product.Active = p.Active;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }

        public ActionResult Delete(int id)
        {
            //var deleteList = db.OrderLine.Where(w => w.Product.ProductId == id).AsQueryable();

            var product = db.Product.Find(id);
            //foreach(var item in Product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item);
            //}
            db.OrderLine.RemoveRange(product.OrderLine);



            //var p = db.Product.Find(id);
            //db.Product.Remove(Product);
            product.IsDeleted = true;
            try
            {
                db.SaveChanges();
                
            }
            catch (DbEntityValidationException ex) {
                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}
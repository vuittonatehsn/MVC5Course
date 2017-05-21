using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;

namespace MVC5Course.Controllers
{
    [Bind(Exclude = "Note")]
    public class 會員Controller : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        
        // GET: 會員
        public ActionResult Index(int rateList = -1, string lastNameList = "", int pageNo =1)
        //public ActionResult Index(ClientIndexViewModel clientIndexViewModel)
        {
            var rateList1 = (from p in db.Client
                              select p.CreditRating ).Distinct().OrderBy(e => e).ToList();
            var lastNameList1 = (from p in db.Client
                              select p.LastName).Distinct().OrderBy(e => e).ToList();

            ViewBag.rateList = new SelectList(rateList1);
            ViewBag.lastNameList = new SelectList(lastNameList1);
            //var client = db.Client.Include(c => c.Occupation);
            var client = db.Client.AsQueryable();
            
            if (rateList >= 0)
                {
                client = client.Where(p => p.CreditRating == rateList);
                }
            if (!String.IsNullOrEmpty(lastNameList))
                {
                client = client.Where(p => p.LastName == lastNameList);
             }

            ViewData.Model = client.OrderByDescending(w => w.ClientId).ToPagedList(pageNo, 10);


            return View();
        }

        public ActionResult Detail2()
        {
            var client = db.Client.Select(w=> new ClientViewMOdel
            {
                firstName= w.FirstName,
                Gender = w.Gender
            });
            return View(client.Take(10));
        }


        // GET: 會員/Details/5
        public ActionResult 資料Details(int? id = 1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: 會員/Create
        public ActionResult Create()
        {
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName");
            return View();
        }

        // POST: 會員/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Client.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: 會員/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            var rateList1 = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            ViewBag.CreditRating = new SelectList(rateList1, client.CreditRating);
            return View(client);
        }

        // POST: 會員/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: 會員/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: 會員/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Client.Find(id);
            db.Client.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

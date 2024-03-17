using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.UI.WebControls;
using System.Data.Entity.Infrastructure;
using System.Web.Configuration;



namespace OnlineShop.Controllers
{
    public class Table_2Controller : Controller
    {
        private sign_up db = new sign_up();
        // GET: Table_1
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            
            Table_2 table_1 = new Table_2();
            
            return View(table_1);
        }
        [HttpPost]
        public ActionResult AddOrEdit(Table_2 table1)
        {


            using (sign_up7 m = new sign_up7())
            {
                if (m.Table_2.Any(u => u.Username == table1.Username))
                {
                    ViewBag.DuplicateMessage = "username exist";
                    return View("AddOrEdit", table1);
                }
                m.Table_2.Add(table1);
                m.SaveChanges();
            }
            ViewBag.SuccessMessage = "sign successful";
            return View("AddOrEdit", new Table_2());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Table_2 user)
        {
            using (sign_up7 m = new sign_up7())
            {
                var userInDb = m.Table_2.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                if (userInDb != null)
                {
                    if (user.Username == "Admin")
                    {
                        return RedirectToAction("Index");
                    }

                    return RedirectToAction("Products", "Table_2");
                }
                else
                {

                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult Products()
        {
            return View(db.Book.ToList());
        }

        [HttpGet]
        public ActionResult Onsale()
        {
            return View(db.Book.ToList());
        }

        public ActionResult Index()
        {
            return View(db.Book.ToList());
        }
        [HttpGet]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book table_2 = db.Book.Find(id);
            if (table_2 == null)
            {
                return HttpNotFound();
            }
            return View(table_2);
        }
        
       

        public ActionResult Create()
        {
            return View();
        }

        // POST: Table_21/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,price,amount,category,date,image,age,onsale")] Book table)
        {
            if (ModelState.IsValid)
            {
                db.Book.Add(table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            //if (id == null)
            //{
            //    Console.WriteLine("asd");
            //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            //}
            Book table = db.Book.Find(id);
            //if (table == null)
            //{
            //    return HttpNotFound();
            //}
            return View(table);
            //return RedirectToAction("Index");
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name,price,amount,category,date,onsale")] Book table)
        {
            //if (ModelState.IsValid)
            //{
                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
            //return View(table);
        }



        [HttpGet]
        public ActionResult Delete(string id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Book table = db.Book.Find(id);
            //if (table == null)
            //{
            //    return HttpNotFound();
            //}
            return View(table);
        }

 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Book table = db.Book.Find(id);
            db.Book.Remove(table);
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
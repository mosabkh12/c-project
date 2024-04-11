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
using Library.ViewModels;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Text;



namespace OnlineShop.Controllers
{
    public class Table_2Controller : Controller
    {
        private sign_up db = new sign_up();
        private sign_up1 p = new sign_up1();
        private sign_up12 d = new sign_up12();
        private sign_up14 s = new sign_up14();
        private sign_up15 r = new sign_up15();

        private string EncryptString(string plainText, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = new byte[16]; 

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes;

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedBytes);
            }
        }
        private byte[] GenerateRandomKey()
        {
            byte[] key = new byte[32]; 

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            return key;
        }
        public ActionResult YourAction()
        {
            var books = db.Book.ToList();
            var payments = p.payment2.ToList();

            var viewModel = new BooksAndPaymentsViewModel
            {
                Books = books,
                Payments = payments
            };

            return View(viewModel);
        }

        //private sign_up1 d = new sign_up1();
        //private sign_up d = new sign_up();
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
            BooksAndPaymentsViewModel Wproductd = new BooksAndPaymentsViewModel();

            return View(db.Book.ToList());
        }

        [HttpGet]
        public ActionResult Onsale()
        {
            return View(db.Book.ToList());
        }
        [HttpGet]
        public ActionResult payment()
        {
            payment2 table_1 = new payment2();
            return View(table_1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public List<Book> GetItems()
        {
            // Implement logic to query database and return list of items
            return db.Book.ToList();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //the newest version(test)[Bind(Include = "cardholder,cardnum,expdate,cvv,id")]
        public ActionResult payment( payment2 table, string hiddenInput)
        {
            string[] items = hiddenInput.Split(',');
            if (ModelState.IsValid)
            {
                byte[] encryptionKey = GenerateRandomKey();

                string encryptedcardnum = EncryptString(table.cardnum, encryptionKey);
                table.cardnum = encryptedcardnum;

                table.count1 = int.Parse(items[1]);
                using (sign_up1 m = new sign_up1())
                {
                   

                    m.payment2.Add(table);
                    m.SaveChanges();
                   
                        string asd = items[0];
                        // Find the book to update
                        var bookToUpdate = db.Book.FirstOrDefault(b => b.name == asd);
                        var entity = new messagee
                        {
                            message1 = "The amount of book " + bookToUpdate.name + " is now zero. Please restock.",
                            bookn = bookToUpdate.name
                        };

                        if (bookToUpdate != null)
                        {
                            // Decrease the amount by the quantity purchased
                            bookToUpdate.amount -= int.Parse(items[1]);

                            // Ensure the amount doesn't go below zero

                            if (bookToUpdate.amount < 0)
                            {
                                bookToUpdate.amount = 0;
                            }

                            db.SaveChanges();
                            if (bookToUpdate.amount == 0)
                            {
                                s.messagee.Add(entity);
                                s.SaveChanges();
                            TempData["SuccessMessage"] = "Payment successful!";
                            return RedirectToAction("payment");
                            }


                            TempData["SuccessMessage"] = "Payment successful!";
                            return RedirectToAction("payment");
                        } 
                    else
                    {
                        // Handle the case where the book with the provided name is not found
                        TempData["ErrorMessage"] = "Book not found!";
                        return RedirectToAction("payment");
                    }
                }
            }
          
            return View(table);
        }


        public ActionResult values()
        {
            return View(db.Book.ToList());
        }
        public ActionResult request()
        {
            return View();
        }
        public ActionResult Basket()
        {
            return View();
        }
        public ActionResult reqshow()
        {
            return View(d.request.ToList());
        }
        public ActionResult amount()
        {
            return View(s.messagee.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult request([Bind(Include = "bookreq,amountreq")] request table)
        {
            if (d.request.Any(u => u.bookreq == table.bookreq))
            {
                ViewBag.DuplicateMessage = "The book already in the library";
                return View("request", table);
            }
         
            d.request.Add(table);
            TempData["SuccessMessage1"] = "request sent";
            d.SaveChanges();
            return RedirectToAction("request");
        
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

        public ActionResult Fantacy()
        {
            return View(db.Book.ToList());
        }
        public ActionResult Romance()
        {
            return View(db.Book.ToList());
        }
        public ActionResult answer()
        {
            return View(r.message3.ToList());
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
        public ActionResult Create([Bind(Include = "name,price,amount,category,date,image,age,onsale")] Book table,request req)
        {
            if (db.Book.Any(u => u.name == table.name))
            {
                ViewBag.DuplicateMessage = "name exist";
                return View("Create", table);
            }

            
            var bookToUpdate = d.request.FirstOrDefault(b => b.bookreq == table.name);

            if (bookToUpdate != null)
            {

                var entity = new message3
                {
                    bk = bookToUpdate.bookreq,
                    message4  = "The book "+ bookToUpdate.bookreq+ " is now available"
                };
                r.message3.Add(entity);
                r.SaveChanges();
                db.Book.Add(table);
                ViewBag.SuccessMessage = "sign successful";
                db.SaveChanges();
                return RedirectToAction("index");

            }

            db.Book.Add(table);
            ViewBag.SuccessMessage = "sign successful";
            db.SaveChanges();
            return RedirectToAction("Index");
          
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
           
            Book table = db.Book.Find(id);
         
            return View(table);
            
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name,price,amount,category,date,onsale")] Book table)
        {

            db.Entry(table).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

          
        }



        [HttpGet]
        public ActionResult Delete(string id)
        {
            Book table = db.Book.Find(id);
          
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
        public ActionResult Delete1(string id)
        {

            request table = d.request.Find(id);

            return View(table);
        }


        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(string id)
        {
            request table = d.request.Find(id);
            d.request.Remove(table);
            d.SaveChanges();
            return RedirectToAction("reqshow");
        }

        public ActionResult Deletee(string id)
        {

            messagee table = s.messagee.Find(id);

            return View(table);
        }


        [HttpPost, ActionName("Deletee")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(string id)
        {
            messagee table = s.messagee.Find(id);
            s.messagee.Remove(table);
            s.SaveChanges();
            return RedirectToAction("amount");
        }
        public ActionResult Delete9(string id)
        {

            message3 table = r.message3.Find(id);

            return View(table);
        }


        [HttpPost, ActionName("Delete9")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed9(string id)
        {
            message3 table = r.message3.Find(id);
            r.message3.Remove(table);
            r.SaveChanges();
            return RedirectToAction("answer");
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Trade2.Models;

namespace E_Trade2.Controllers
{
    
    public class ProductsController : Controller
    {
        private Etrade2Entities db = new Etrade2Entities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Categories);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.RefCategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] //Ckeditor de fotoğraf eklemeye izin verdik
        public ActionResult Create([Bind(Include = "ProductId,ProductName,RefCategoryId,ProductDescription,ProductPrice")] Products products,
            HttpPostedFileBase ProductImage)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();

                    if (ProductImage.ContentLength >   0)
                    {
                        
                        var path = Path.Combine(Server.MapPath("~/Image"), products.ProductId + ".jpg");
                        ProductImage.SaveAs(path);
                    }
                

                return RedirectToAction("Index");
            }

            ViewBag.RefCategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", products.RefCategoryId);
            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefCategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", products.RefCategoryId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,RefCategoryId,ProductDescription,ProductPrice")] Products products, HttpPostedFileBase ProductImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();

                if (ProductImage != null && ProductImage.ContentLength > 0)
                {
                    string FilePath = Path.Combine(Server.MapPath("~/Image"), products.ProductId + ".jpg");
                    ProductImage.SaveAs(FilePath);//File Path farklı kaydet

                }

                return RedirectToAction("Index");
            }
            ViewBag.RefCategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", products.RefCategoryId);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            //---------------------------------------
            string FilePath = Path.Combine(Server.MapPath("~/Image"), products.ProductId + ".jpg");
            FileInfo fi = new FileInfo(FilePath);

            if (fi.Exists)
            {
                fi.Delete();
            }
            //Burda yaptıklarımız Ürünü sildiğimizde Fotoğrafıda otomatik olarak silsin 
            //---------------------------------------

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

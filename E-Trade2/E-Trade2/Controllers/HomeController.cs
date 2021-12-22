using E_Trade2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Trade2.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        Etrade2Entities db = new Etrade2Entities();
        [Authorize(Roles = "user,admin")]
        
        public ActionResult Index()
        {
            ViewBag.CategoryList = db.Categories.ToList();
            ViewBag.ProductLast = db.Products.OrderByDescending(x => x.ProductId).Take(12).ToList(); 
            return View();
        }
     
        public ActionResult Categories(int id)
        {
            ViewBag.CategoryList = db.Categories.ToList();
            ViewBag.Category = db.Categories.Find(id);

            return View(db.Products.Where(x=>x.RefCategoryId==id).OrderBy(x=>x.ProductName).ToList());
        }
    }
}
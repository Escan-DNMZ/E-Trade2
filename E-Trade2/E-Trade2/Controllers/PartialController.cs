using E_Trade2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Trade2.Controllers
{
    public class PartialController : Controller
    {
        Etrade2Entities db = new Etrade2Entities();
        
        [AllowAnonymous]
        public ActionResult Category()
        {
            ViewBag.CategoryList = db.Categories.ToList();
            return PartialView();
        }
    }
}
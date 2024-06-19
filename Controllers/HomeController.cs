using ngay8thang3_Complete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ngay8thang3_Complete.Controllers
{
    public class HomeController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();
        public ActionResult Index()
        {
            var list = db.DongHoes.ToList();
            return View(list);
        }
        public ActionResult contact()
        {
            return View();
        }
   
    }
}
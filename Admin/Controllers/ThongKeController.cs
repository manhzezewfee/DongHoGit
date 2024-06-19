using Microsoft.Ajax.Utilities;
using ngay8thang3_Complete.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngay8thang3_Complete.Areas.Admin.Controllers
{
    
    public class ThongKeController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();
        // GET: Admin/ThongKe
        public PartialViewResult Index()
        {
            return  PartialView("_ThongKe");
        }
        public ActionResult doanhthu()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetDoanhThu(string formDate, string toDate)
        {
            var query = from dhang in db.DatHangs
                        join dh_ct in db.DatHang_ChiTiet on dhang.ID equals dh_ct.DatHang_ID
                        join dh in db.DongHoes on dh_ct.DongHo_ID equals dh.ID
                        select new
                        {
                            CreateDate = dhang.NgayDatHang,
                            Quatity = dh_ct.SoLuong,
                            Price = dh_ct.DonGia,
                            OriginPrice = dh.GiaNhap
                        };
            if(!string.IsNullOrEmpty(formDate))
            {
                DateTime startDate = DateTime.ParseExact(formDate, "dd/MM/yy", null);
                query = query.Where(x => x.CreateDate >= startDate);

            }   
            if(!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yy", null);
                query = query.Where(x => x.CreateDate < endDate);
            }
            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreateDate)).Select(x => new
            {
                Date = x.Key.Value,
                TongTienNhap = x.Sum(y => y.Quatity * y.OriginPrice),
                TongTienBan = x.Sum(y => y.Quatity * y.Price)
            }).Select(x => new
            {
                Date = x.Date,
                DoanhThu = x.TongTienBan,
                LoiNhuan = x.TongTienBan - x.TongTienNhap
            });
            return Json(new {result = result, msg = "Query thành thông"}, JsonRequestBehavior.AllowGet);
        }
    }
}
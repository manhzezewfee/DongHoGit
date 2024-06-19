using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ngay8thang3_Complete.Models;

namespace ngay8thang3_Complete.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();

        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            if (Session["idadmin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");

            }
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(db.KhachHangs.ToList());
        }

        // GET: Admin/KhachHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoVaten,DienThoai,DiaChi,TenDangNhap,MatKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }

        // GET: Admin/KhachHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }
        private string GetMD5(string matKhau)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromdata = Encoding.UTF8.GetBytes(matKhau);
            byte[] targetData = md5.ComputeHash(fromdata);
            string byte25string = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte25string += targetData[i].ToString("x2");
            }
            return byte25string;
        }
        // POST: Admin/KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoVaten,DienThoai,DiaChi,TenDangNhap,Email,MatKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                string mk = GetMD5(khachHang.MatKhau);
                khachHang.MatKhau = mk;
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                TempData["result"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DemoAjax()
        {
            return View();
        }
        //hiển thị
        [HttpGet]
        public JsonResult DanhSach()
        {
            try
            {
                var ds = (from kh in db.KhachHangs
                          select new
                          {
                              id = kh.ID,
                              hoten = kh.HoVaten,
                              dienthoai = kh.DienThoai,
                              diachi = kh.DiaChi,
                              tendangnhap = kh.TenDangNhap,
                              matkhau = kh.MatKhau

                          }).ToList();
                return Json(new { code = 200, ds = ds, msg = "Lấy danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Lấy danh sách thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //thêm mới
        [HttpPost]
        public JsonResult addTaiKhoan(string hoten, string dienthoai, string diachi, string tendangnhap, string matkhau)
        {
            try
            {
                string mk = GetMD5(matkhau);
                KhachHang kh = new KhachHang();
                kh.HoVaten = hoten;
                kh.DienThoai = dienthoai;
                kh.DiaChi = diachi;
                kh.TenDangNhap = tendangnhap;
                kh.MatKhau = mk;

                db.KhachHangs.Add(kh);
                db.SaveChanges();
                TempData["result"] = "Thêm thành công";
                return Json(new { code = 200, msg = "Thêm mới thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Thêm mới Thất bại" + ex }, JsonRequestBehavior.AllowGet);
            }
        }

        //chi tiết
        [HttpGet]
        public JsonResult Chitiet(int id)
        {
            try
            {
                var dt = db.KhachHangs.SingleOrDefault(x => x.ID == id);
                string mk = GetMD5(dt.MatKhau);
                dt.MatKhau = mk;

                return Json(new { code = 200, dt = dt, msg = "láy thông tin Thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "láy thông tin Thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }

        //sửa thông tin
        //public ActionResult SuaThongTin(int id)
        //{
        //    try
        //    {
        //        var dt = db.KhachHangs.SingleOrDefault(x => x.ID == id);
        //        return 
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //}






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

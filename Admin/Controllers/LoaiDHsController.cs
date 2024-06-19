using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ngay8thang3_Complete.Models;

namespace ngay8thang3_Complete.Areas.Admin.Controllers
{
    public class LoaiDHsController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();

        // GET: Admin/LoaiDHs
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
            return View(db.LoaiDHs.ToList());
        }

        // GET: Admin/LoaiDHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDH loaiDH = db.LoaiDHs.Find(id);
            if (loaiDH == null)
            {
                return HttpNotFound();
            }
            return View(loaiDH);
        }

        // GET: Admin/LoaiDHs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiDHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenLoai")] LoaiDH loaiDH)
        {
            if (ModelState.IsValid)
            {
                db.LoaiDHs.Add(loaiDH);
                db.SaveChanges();
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            return View(loaiDH);
        }

        // GET: Admin/LoaiDHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDH loaiDH = db.LoaiDHs.Find(id);
            if (loaiDH == null)
            {
                return HttpNotFound();
            }
            return View(loaiDH);
        }

        // POST: Admin/LoaiDHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenLoai")] LoaiDH loaiDH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiDH).State = EntityState.Modified;
                db.SaveChanges();
                TempData["result"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(loaiDH);
        }

        // GET: Admin/LoaiDHs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDH loaiDH = db.LoaiDHs.Find(id);
            if (loaiDH == null)
            {
                return HttpNotFound();
            }
            return View(loaiDH);
        }

        // POST: Admin/LoaiDHs/Delete/5

        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LoaiDH loaiDH = db.LoaiDHs.Find(id);
                db.LoaiDHs.Remove(loaiDH);
                db.SaveChanges();
                TempData["result"] = "Đã xóa thành công";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["result"] = "Có lỗi xảy ra! hãy thử lại";
                return RedirectToAction("Index");
            }

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

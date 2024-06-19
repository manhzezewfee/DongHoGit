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
    public class ThuongHieuController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();

        // GET: Admin/ThuongHieu
        public ActionResult Index()
        {
            if (Session["idadmin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");

            }
            if (TempData["AlertMessage"] != null)
                TempData["AlertMessage"] = TempData["AlertMessage"];
            return View(db.ThuongHieux.ToList());
        }

        // GET: Admin/ThuongHieu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThuongHieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenThuongHieu")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                db.ThuongHieux.Add(thuongHieu);
                db.SaveChanges();
                TempData["AlertMessage"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // POST: Admin/ThuongHieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenThuongHieu")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thuongHieu).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // POST: Admin/ThuongHieu/Delete/5

        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
                db.ThuongHieux.Remove(thuongHieu);
                db.SaveChanges();
                TempData["AlertMessage"] = "Xóa thành công";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
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

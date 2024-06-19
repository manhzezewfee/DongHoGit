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
    public class DatHang_ChiTietController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();

        // GET: Admin/DatHang_ChiTiet
        public ActionResult Index()
        {
            if (Session["idadmin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");

            }
            var datHang_ChiTiet = db.DatHang_ChiTiet.Include(d => d.DatHang).Include(d => d.DongHo);
            return View(datHang_ChiTiet.ToList());
        }

        // GET: Admin/DatHang_ChiTiet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang_ChiTiet datHang_ChiTiet = db.DatHang_ChiTiet.Find(id);
            if (datHang_ChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(datHang_ChiTiet);
        }

        // GET: Admin/DatHang_ChiTiet/Create
        public ActionResult Create()
        {
            ViewBag.DatHang_ID = new SelectList(db.DatHangs, "ID", "DienThoaiGiaoHang");
            ViewBag.DongHo_ID = new SelectList(db.DongHoes, "ID", "TenDongHo");
            return View();
        }

        // POST: Admin/DatHang_ChiTiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DatHang_ID,DongHo_ID,SoLuong,DonGia")] DatHang_ChiTiet datHang_ChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.DatHang_ChiTiet.Add(datHang_ChiTiet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DatHang_ID = new SelectList(db.DatHangs, "ID", "DienThoaiGiaoHang", datHang_ChiTiet.DatHang_ID);
            ViewBag.DongHo_ID = new SelectList(db.DongHoes, "ID", "TenDongHo", datHang_ChiTiet.DongHo_ID);
            return View(datHang_ChiTiet);
        }

        // GET: Admin/DatHang_ChiTiet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang_ChiTiet datHang_ChiTiet = db.DatHang_ChiTiet.Find(id);
            if (datHang_ChiTiet == null)
            {
                return HttpNotFound();
            }
            ViewBag.DatHang_ID = new SelectList(db.DatHangs, "ID", "DienThoaiGiaoHang", datHang_ChiTiet.DatHang_ID);
            ViewBag.DongHo_ID = new SelectList(db.DongHoes, "ID", "TenDongHo", datHang_ChiTiet.DongHo_ID);
            return View(datHang_ChiTiet);
        }

        // POST: Admin/DatHang_ChiTiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DatHang_ID,DongHo_ID,SoLuong,DonGia")] DatHang_ChiTiet datHang_ChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datHang_ChiTiet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DatHang_ID = new SelectList(db.DatHangs, "ID", "DienThoaiGiaoHang", datHang_ChiTiet.DatHang_ID);
            ViewBag.DongHo_ID = new SelectList(db.DongHoes, "ID", "TenDongHo", datHang_ChiTiet.DongHo_ID);
            return View(datHang_ChiTiet);
        }

        // GET: Admin/DatHang_ChiTiet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang_ChiTiet datHang_ChiTiet = db.DatHang_ChiTiet.Find(id);
            if (datHang_ChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(datHang_ChiTiet);
        }

        // POST: Admin/DatHang_ChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatHang_ChiTiet datHang_ChiTiet = db.DatHang_ChiTiet.Find(id);
            db.DatHang_ChiTiet.Remove(datHang_ChiTiet);
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

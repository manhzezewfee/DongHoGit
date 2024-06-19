﻿using System;
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
    public class XuatXuController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();

        // GET: Admin/XuatXu
        public ActionResult Index()
        {
            if (Session["idadmin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");

            }
            return View(db.XuatXus.ToList());
        }

        // GET: Admin/XuatXu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatXu xuatXu = db.XuatXus.Find(id);
            if (xuatXu == null)
            {
                return HttpNotFound();
            }
            return View(xuatXu);
        }

        // GET: Admin/XuatXu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/XuatXu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenQG")] XuatXu xuatXu)
        {
            if (ModelState.IsValid)
            {
                db.XuatXus.Add(xuatXu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(xuatXu);
        }

        // GET: Admin/XuatXu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatXu xuatXu = db.XuatXus.Find(id);
            if (xuatXu == null)
            {
                return HttpNotFound();
            }
            return View(xuatXu);
        }

        // POST: Admin/XuatXu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenQG")] XuatXu xuatXu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xuatXu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(xuatXu);
        }

        // GET: Admin/XuatXu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatXu xuatXu = db.XuatXus.Find(id);
            if (xuatXu == null)
            {
                return HttpNotFound();
            }
            return View(xuatXu);
        }

        // POST: Admin/XuatXu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            XuatXu xuatXu = db.XuatXus.Find(id);
            db.XuatXus.Remove(xuatXu);
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

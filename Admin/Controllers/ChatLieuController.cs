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
    public class ChatLieuController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();

        // GET: Admin/ChatLieu
        public ActionResult Index()
        {
            if (Session["idadmin"]==null)
            {
                return RedirectToAction("DangNhap","Home");

            }
            if (TempData["result"] !=null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }    
            return View(db.ChatLieux.ToList());
        }

        // GET: Admin/ChatLieu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // GET: Admin/ChatLieu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChatLieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenChatLieu")] ChatLieu chatLieu)
        {
            if (ModelState.IsValid)
            {
                db.ChatLieux.Add(chatLieu);
                db.SaveChanges();
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            return View(chatLieu);
        }

        // GET: Admin/ChatLieu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // POST: Admin/ChatLieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenChatLieu")] ChatLieu chatLieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatLieu).State = EntityState.Modified;
                db.SaveChanges();
                TempData["result"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(chatLieu);
        }

        // GET: Admin/ChatLieu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // POST: Admin/ChatLieu/Delete/5
       
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ChatLieu dongHo = db.ChatLieux.Find(id);
                db.ChatLieux.Remove(dongHo);
                db.SaveChanges();
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ngay8thang3_Complete.Models;
using PagedList;

namespace ngay8thang3_Complete.Areas.Admin.Controllers
{
    public class DongHoController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();

        // GET: Admin/DongHo
        public ActionResult Index()
        {
            if (Session["fullnameAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            else
            {
                if (TempData["result"] != null)
                {
                    ViewBag.SuccessMsg = TempData["result"];
                }
                var dongHoes = db.DongHoes.Include(d => d.ChatLieu).Include(d => d.LoaiDH).Include(d => d.ThuongHieu).Include(d => d.XuatXu);
                return View(dongHoes.ToList());
            }

        }
        //dùng tay để phân trang, tìm kiếm và sắp xếp
        //public ActionResult Index(string currentFilter, string SearchString, int? page)
        //{
        //    var dongho = new List<DongHo>();
        //    if (SearchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        SearchString = currentFilter;
        //    }
        //    if (!string.IsNullOrEmpty(SearchString))
        //    {
        //        //lấy ds sản phẩm theo từ khóa tìm kiếm
        //        dongho = db.DongHoes.Where(n => n.TenDongHo.Contains(SearchString)).ToList();
        //    }
        //    else
        //    {
        //        //lay all sån phâm trong bång product
        //        dongho = db.DongHoes.ToList();
        //    }
        //    ViewBag.CurrentFilter = SearchString;
        //    //số lượng item của 1 trang = 4
        //    int pageSize = 5;
        //    int pageNumber = (page ?? 1);
        //    if (dongho.Count<1)
        //    {
        //        ViewBag.thongbao = "Hiện tại không có sản phẩm nào";
        //    }
        //    //sắp xếp theo id sản phẩm,sp mới đưa lên đầu
        //    dongho = dongho.OrderByDescending(n => n.ID).ToList();
        //    return View(dongho.ToPagedList(pageNumber, pageSize));

        //}
        public ActionResult index2()
        {

            return View(db.DongHoes.ToList());
        }
        // GET: Admin/DongHo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongHo dongHo = db.DongHoes.Find(id);
            if (dongHo == null)
            {
                return HttpNotFound();
            }
            return View(dongHo);
        }

        // GET: Admin/DongHo/Create
        public ActionResult Create()
        {
            ViewBag.ChatLieu_ID = new SelectList(db.ChatLieux, "ID", "TenChatLieu");
            ViewBag.TenLoai_ID = new SelectList(db.LoaiDHs, "ID", "TenLoai");
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieux, "ID", "TenThuongHieu");
            ViewBag.XuatXu_ID = new SelectList(db.XuatXus, "ID", "TenQG");
            return View();
        }

        // POST: Admin/DongHo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,ThuongHieu_ID,TenLoai_ID,ChatLieu_ID,XuatXu_ID,TenDongHo,MauSac,HanBaoHanh,DonGia,SoLuong,HinhAnhDH,MoTaNgan,Đaciem,MoTa,KhuyenMai")] DongHo dongHo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dongHo.HinhAnhDH = "Storage/";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string filename = Path.GetFileName(f.FileName);
                        string upload = Server.MapPath("~/Storage/" + filename);
                        f.SaveAs(upload);
                        dongHo.HinhAnhDH += filename;
                    }
                    db.DongHoes.Add(dongHo);
                    db.SaveChanges();
                    TempData["result"] = "Thêm mới thành công";
                   
                }
                
                TempData["result"] = "thêm mới Thành công";
                return RedirectToAction("Index");
               

            }
            catch (Exception)
            {
                ViewBag.ChatLieu_ID = new SelectList(db.ChatLieux, "ID", "TenChatLieu", dongHo.ChatLieu_ID);
                ViewBag.TenLoai_ID = new SelectList(db.LoaiDHs, "ID", "TenLoai", dongHo.TenLoai_ID);
                ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieux, "ID", "TenThuongHieu", dongHo.ThuongHieu_ID);
                ViewBag.XuatXu_ID = new SelectList(db.XuatXus, "ID", "TenQG", dongHo.XuatXu_ID);
                return View(dongHo);

            }



        }

        // GET: Admin/DongHo/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongHo dongHo = db.DongHoes.Find(id);
            if (dongHo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChatLieu_ID = new SelectList(db.ChatLieux, "ID", "TenChatLieu", dongHo.ChatLieu_ID);
            ViewBag.TenLoai_ID = new SelectList(db.LoaiDHs, "ID", "TenLoai", dongHo.TenLoai_ID);
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieux, "ID", "TenThuongHieu", dongHo.ThuongHieu_ID);
            ViewBag.XuatXu_ID = new SelectList(db.XuatXus, "ID", "TenQG", dongHo.XuatXu_ID);
            return View(dongHo);
        }

        // POST: Admin/DongHo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,ThuongHieu_ID,TenLoai_ID,ChatLieu_ID,XuatXu_ID,TenDongHo,MauSac,HanBaoHanh,DonGia,SoLuong,HinhAnhDH,MoTaNgan,Đaciem,MoTa,KhuyenMai,GiaNhap")] DongHo dongHo)
        {
            if (ModelState.IsValid)
            {
                //dongHo.HinhAnhDH = "Storage/";
                //var f = Request.Files["HinhAnhDH"];
                //if (f != null && f.ContentLength > 0)
                //{
                //    string filename = Path.GetFileName(f.FileName);
                //    string upload = Server.MapPath("~/Storage/" + filename);
                //    f.SaveAs(upload);
                //    dongHo.HinhAnhDH += filename;
                //}
                db.Entry(dongHo).State = EntityState.Modified;
                db.SaveChanges();
                TempData["result"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            ViewBag.ChatLieu_ID = new SelectList(db.ChatLieux, "ID", "TenChatLieu", dongHo.ChatLieu_ID);
            ViewBag.TenLoai_ID = new SelectList(db.LoaiDHs, "ID", "TenLoai", dongHo.TenLoai_ID);
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieux, "ID", "TenThuongHieu", dongHo.ThuongHieu_ID);
            ViewBag.XuatXu_ID = new SelectList(db.XuatXus, "ID", "TenQG", dongHo.XuatXu_ID);
            return View(dongHo);
        }

        // GET: Admin/DongHo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongHo dongHo = db.DongHoes.Find(id);
            if (dongHo == null)
            {
                return HttpNotFound();
            }
            return View(dongHo);
        }

        // POST: Admin/DongHo/Delete/5

        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                DongHo dongHo = db.DongHoes.Find(id);
                db.DongHoes.Remove(dongHo);
                db.SaveChanges();
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }
        //Khuyễn mại

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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ngay8thang3_Complete.Models;
using PagedList;

namespace ngay8thang3_Complete.Controllers
{
    public class DongHoesController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();
        [ChildActionOnly]
        // GET: DongHoes
        //danh mục thể loại
        public ActionResult Category()
        {
            var catego = db.LoaiDHs.ToList();
            return PartialView(catego);
        }

        public ActionResult ListId(int ID)
        {
            var list = db.DongHoes.Where(d => d.TenLoai_ID == ID).ToList();
            return View(list);

        }
        //thương hiệu
        public ActionResult ThuongHieu()
        {
            var th = db.ThuongHieux.OrderBy(d => d.ID).ToList();
            return PartialView(th);
        }
        public ActionResult list_th(int id)
        {
            var th = db.DongHoes.Where(d => d.ThuongHieu_ID == id).ToList();
            return View(th);
        }
        //danh sách
        //Danh sách 1
        //public ActionResult Index(string SearchString)
        //{
        //    if (SearchString != null)
        //    {
        //        var dh = db.DongHoes.Where(x => x.TenDongHo.Contains(SearchString));
        //        Session["search"] = SearchString;
        //        return View(dh.ToList());
        //    }
        //    else
        //    {
        //        var dh = db.DongHoes;
        //        Session["search"] = null;
        //        return View(dh.ToList());
        //    }

           
        //}
        //dùng tay để phân trang, tìm kiếm và sắp xếp
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var dongho = new List<DongHo>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                //lấy ds sản phẩm theo từ khóa tìm kiếm
                dongho = db.DongHoes.Where(n => n.TenDongHo.Contains(SearchString)).ToList();
                Session["search"] = SearchString;
            }
            else
            {
                //lay all sån phâm trong bång product
                dongho = db.DongHoes.ToList();
                Session["search"] = null; ;
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item của 1 trang = 4
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            if (dongho.ToList().Count<1)
            {
                ViewBag.thongbao = "Hiện tại không có sản phẩm nào";
            }
            
            //sắp xếp theo id sản phẩm,sp mới đưa lên đầu
            dongho = dongho.OrderByDescending(n => n.ID).ToList();
            return View(dongho.ToPagedList(pageNumber, pageSize));

        }
        //kết thúc danh sách 1
        //danh sách 2
        public ActionResult Index2()
        {
            var List = db.DongHoes.Include(d => d.ChatLieu).Include(d => d.LoaiDH).Include(d => d.ThuongHieu).Include(d => d.XuatXu);
            return View(List.ToList());
        }
        // GET: DongHoes/Details/5
        public ActionResult Details(int? id)
        {
            var dt = db.DongHoes.Where(d => d.ID == id).SingleOrDefault();
            return View(dt);
        }
        public ActionResult Detailsss(int? id)
        {
            var dt = db.DongHoes.Where(d => d.ID == id).SingleOrDefault();
            return View(dt);
        }

        // GET: DongHoes/Create
        public ActionResult Create()
        {
            ViewBag.ChatLieu_ID = new SelectList(db.ChatLieux, "ID", "TenChatLieu");
            ViewBag.TenLoai_ID = new SelectList(db.LoaiDHs, "ID", "TenLoai");
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieux, "ID", "TenThuongHieu");
            ViewBag.XuatXu_ID = new SelectList(db.XuatXus, "ID", "TenQG");
            return View();
        }

        // POST: DongHoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ThuongHieu_ID,TenLoai_ID,ChatLieu_ID,XuatXu_ID,TenDongHo,MauSac,HanBaoHanh,DonGia,SoLuong,HinhAnhDH,MoTa")] DongHo dongHo)
        {
            if (ModelState.IsValid)
            {
                db.DongHoes.Add(dongHo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChatLieu_ID = new SelectList(db.ChatLieux, "ID", "TenChatLieu", dongHo.ChatLieu_ID);
            ViewBag.TenLoai_ID = new SelectList(db.LoaiDHs, "ID", "TenLoai", dongHo.TenLoai_ID);
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieux, "ID", "TenThuongHieu", dongHo.ThuongHieu_ID);
            ViewBag.XuatXu_ID = new SelectList(db.XuatXus, "ID", "TenQG", dongHo.XuatXu_ID);
            return View(dongHo);
        }

        // GET: DongHoes/Edit/5
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

        // POST: DongHoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ThuongHieu_ID,TenLoai_ID,ChatLieu_ID,XuatXu_ID,TenDongHo,MauSac,HanBaoHanh,DonGia,SoLuong,HinhAnhDH,MoTa")] DongHo dongHo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dongHo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChatLieu_ID = new SelectList(db.ChatLieux, "ID", "TenChatLieu", dongHo.ChatLieu_ID);
            ViewBag.TenLoai_ID = new SelectList(db.LoaiDHs, "ID", "TenLoai", dongHo.TenLoai_ID);
            ViewBag.ThuongHieu_ID = new SelectList(db.ThuongHieux, "ID", "TenThuongHieu", dongHo.ThuongHieu_ID);
            ViewBag.XuatXu_ID = new SelectList(db.XuatXus, "ID", "TenQG", dongHo.XuatXu_ID);
            return View(dongHo);
        }

        // GET: DongHoes/Delete/5
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

        // POST: DongHoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DongHo dongHo = db.DongHoes.Find(id);
            db.DongHoes.Remove(dongHo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Khhuyeens mại
        /// </summary>
        public PartialViewResult KhuyenMai()
        {
            var km = from dh in db.DongHoes
                     where dh.KhuyenMai > 0
                     select dh;
            return PartialView("_KhuyenMai", km);
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

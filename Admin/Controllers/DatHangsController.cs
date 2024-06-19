using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ngay8thang3_Complete.Models;
using ngay8thang3_Complete.Models.CommonModel;

namespace ngay8thang3_Complete.Areas.Admin.Controllers
{
    public class DatHangsController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();

        // GET: Admin/DatHangs
        public ActionResult Index()
        {
            if (Session["idadmin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");

            }
            var datHangs = db.DatHangs.Include(d => d.KhachHang).Include(d => d.NhanVien);
            return View(datHangs.ToList());
        }
        //xem cchi tieets don hang

        public ActionResult ChiTiet(int id)
        {
            var x = from dh in db.DatHangs where dh.ID == id
                    select dh;

            var obj = from dh in db.DongHoes
                      join ctdh in db.DatHang_ChiTiet on dh.ID equals ctdh.DongHo_ID
                      join dhang in x on ctdh.DatHang_ID equals dhang.ID
                      join kh in db.KhachHangs on dhang.KhachHang_ID equals kh.ID
                      where dhang.ID == ctdh.DatHang_ID

                      select new DonHang_ThongTinChiTiet
                      {
                          maDonHang = dhang.ID,
                          HoTen = kh.HoVaten,
                          DiaChiGiaoHang = dhang.DiaChiGiaoHang,
                          NgayDatHang = (DateTime)dhang.NgayDatHang,
                          TenSanPham = dh.TenDongHo,
                          DonGia = (int)(dh.DonGia - (dh.DonGia * dh.KhuyenMai / 100)),
                          SoLuong = (int)ctdh.SoLuong,
                          TinhTrang = dhang.TinhTrang,
                          SoDTGiaoHang = dhang.DienThoaiGiaoHang

                      };
            return View(obj.ToList());
        }
        // GET: Admin/DatHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHangs.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            return View(datHang);
        }

        // GET: Admin/DatHangs/Create
        public ActionResult Create()
        {
            ViewBag.KhachHang_ID = new SelectList(db.KhachHangs, "ID", "HoVaten");
            ViewBag.NhanVien_ID = new SelectList(db.NhanViens, "ID", "HoVaTen");
            return View();
        }

        // POST: Admin/DatHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NhanVien_ID,KhachHang_ID,DienThoaiGiaoHang,DiaChiGiaoHang,NgayDatHang,TinhTrang")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                db.DatHangs.Add(datHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhachHang_ID = new SelectList(db.KhachHangs, "ID", "HoVaten", datHang.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanViens, "ID", "HoVaTen", datHang.NhanVien_ID);
            return View(datHang);
        }

        // GET: Admin/DatHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHangs.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhachHang_ID = new SelectList(db.KhachHangs, "ID", "HoVaten", datHang.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanViens, "ID", "HoVaTen", datHang.NhanVien_ID);
            return View(datHang);
        }

        // POST: Admin/DatHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NhanVien_ID,KhachHang_ID,DienThoaiGiaoHang,DiaChiGiaoHang,NgayDatHang,TinhTrang")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhachHang_ID = new SelectList(db.KhachHangs, "ID", "HoVaten", datHang.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanViens, "ID", "HoVaTen", datHang.NhanVien_ID);
            return View(datHang);
        }

        // GET: Admin/DatHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHangs.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            return View(datHang);
        }

        // POST: Admin/DatHangs/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                DatHang dathang = db.DatHangs.Find(id);
                db.DatHangs.Remove(dathang);
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

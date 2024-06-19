using ngay8thang3_Complete.Models;
using ngay8thang3_Complete.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngay8thang3_Complete.Controllers
{
    public class ShoppingCardController : Controller
    {
        // GET: ShoppingCard
        MyShopDbContext db = new MyShopDbContext();
        public ActionResult Index()
        {
            if(TempData["thongbao"]!= null)
            {
                ViewBag.tb = TempData["thongbao"];
            }    
            return View();
        }
        public ActionResult ThemVaoGio(int maSP)
        {
            if (Session["cart"] == null)
            {
                var sp = db.DongHoes.Find(maSP);
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { dongho = sp, soLuongTrongGio = 1 });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                
                int index = isExist(maSP);
                if (index != -1)
                {
                    cart[index].soLuongTrongGio++;
                }
                else
                {
                    var sp = db.DongHoes.Find(maSP);
                    cart.Add(new CartItem { dongho = sp, soLuongTrongGio = 1 });
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }

            return RedirectToAction("Index");
        }

        // GET: GioHang/CapNhatTang/{maSP}
        public ActionResult CapNhatTang(int maSP)
        {
            var sp = db.DongHoes.Find(maSP);
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.dongho.ID == maSP && item.soLuongTrongGio <= 10)
                    if (item.soLuongTrongGio < sp.SoLuong)
                    {
                        item.soLuongTrongGio++;
                    }
                    else
                    {
                        TempData["Message"] = "Sản phẩm không đủ số lượng hiện có " + sp.SoLuong;
                    }

            }
            Session["cart"] = cart;

            return RedirectToAction("Index");
        }

        // GET: GioHang/CapNhatGiam/{maSP}
        public ActionResult CapNhatGiam(int maSP)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.dongho.ID == maSP && item.soLuongTrongGio >= 1)
                    item.soLuongTrongGio--;
            }
            Session["cart"] = cart;

            return RedirectToAction("Index");
        }

        // GET: GioHang/XoaKhoiGio/{maSP}
        public ActionResult XoaKhoiGio(int maSP)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            int index = isExist(maSP);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].dongho.ID.Equals(id))
                    return i;
            return -1;
        }
        //thanh toán
        public ActionResult ThanhToan()
        {
            if (Session["iduser"] == null)
            {
                return RedirectToAction("Index", "KhachHangs");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThanhToan(DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                // Lưu vào bảng DatHang
                DatHang dh = new DatHang();
                dh.DiaChiGiaoHang = datHang.DiaChiGiaoHang;
                dh.DienThoaiGiaoHang = datHang.DienThoaiGiaoHang;
                dh.NgayDatHang = DateTime.Now;
                dh.KhachHang_ID = Convert.ToInt32(Session["iduser"]);
                dh.TinhTrang = 0;
                db.DatHangs.Add(dh);
                db.SaveChanges();

                // Lưu vào bảng DatHang_ChiTiet
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                foreach (var item in cart)
                {
                    DatHang_ChiTiet ct = new DatHang_ChiTiet();
                    ct.DatHang_ID = dh.ID;
                    ct.DongHo_ID = item.dongho.ID;
                    ct.SoLuong = Convert.ToInt16(item.soLuongTrongGio);
                    ct.DonGia = item.dongho.DonGia - item.dongho.KhuyenMai*item.dongho.DonGia/100;
                    db.DatHang_ChiTiet.Add(ct);
                    var dongho = db.DongHoes.Find(item.dongho.ID);
                    dongho.SoLuong -= item.soLuongTrongGio;
                    db.SaveChanges();
                }

                // Xóa giỏ hàng
                cart.Clear();
                Session["count"] = 0;
                TempData["thongbao"] = "Đặt hàng thành công";
                // Quay về trang chủ
                return RedirectToAction("Index");
            }

            return View(datHang);
        }
    }
}
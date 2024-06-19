using ngay8thang3_Complete.Models;
using ngay8thang3_Complete.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ngay8thang3_Complete.Areas.Admin.Controllers
{
    public class NhanViensController : Controller
    {
        // GET: Admin/NhanViens
        MyShopDbContext db = new MyShopDbContext();
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
        [HttpGet]
        public ActionResult Index()
        {
            if (TempData["resultdn"] != null)
            {
                ViewBag.SuccessMsg = TempData["resultdn"];
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index(NhanVienModel nhanvien)
        {
            if (ModelState.IsValid)
            {
                string fpass = Models.CommonModel.SHA1.ComputeHash(nhanvien.MatKhau); //mã hóa
                var getUser = db.NhanViens.Where(m => m.TenDangNhap == nhanvien.TenDangNhap).FirstOrDefault();
                if (getUser==null)
                {
                    ViewBag.login = "Tên đăng nhập không tồn tại";
                    return View("Index");
                }
               else if (nhanvien.MatKhau == "123456")
                {
                    Session["idadmin"] = getUser.ID;
                    Session["fullnameAdmin"] = getUser.TenDangNhap;
                    Session["fullnameAd"] = getUser.HoVaTen;
                    Session["quyen"] = getUser.Quyen;
                    TempData["resultdn"] = "Đăng nhập thành công";
                    return RedirectToAction("Index", "Home");

                }
               else if (getUser.MatKhau != fpass)
                {
                    ViewBag.login = "Tên đăng nhập hoặc mật khẩu không chính xác";
                    return View("Index");
                }
                else
                {
                    Session["idadmin"] = getUser.ID;
                    Session["fullnameAdmin"] = getUser.TenDangNhap;
                    Session["fullnameAd"] = getUser.HoVaTen;
                    Session["quyen"] = getUser.Quyen;
                    TempData["resultdn"] = "Đăng nhập thành công";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.login = "Đăng nhập thất bại";
                return View("Index");
            }

        }
        public ActionResult Logout()
        {
            Session["fullnameAdmin"] = null;
            Session["fullnameAd"] = null;
            return RedirectToAction("DangNhap","Home");
        }
        public ActionResult changePassword()
        {
            return View();
        }
        public ActionResult ListNv()
        {
            if (TempData["result"]!=null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }    
            return View(db.NhanViens.ToList());
        }
        public ActionResult Create()
        {
            return View();

            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="HoVaTen,DienThoai,DiaChi,TenDangNhap,MatKhau,Quyen")]NhanVien nv)
        {
            if(ModelState.IsValid)
            {
                db.NhanViens.Add(nv);
                db.SaveChanges();
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("ListNv");
            }
            return View();
        }
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var x = db.NhanViens.Find(id);
                db.NhanViens.Remove(x);
                db.SaveChanges();
                TempData["result"] = "Đã xóa thành công";
                return RedirectToAction("ListNv");
            }
            catch (Exception)
            {
                TempData["result"] = "Có lỗi xảy ra. hãy thử thực hiện lại";
               return RedirectToAction("ListNv");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoVaTen,DienThoai,DiaChi,TenDangNhap,Quyen")] NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            return View(nv);
        }
     
        public ActionResult DoiMatKhau()
        {
            if (TempData["success"]!=null)
            {
                ViewBag.success = TempData["success"];
            }    
                return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau([Bind(Include = "MatKhau,MatKhauMoi,XacNhanMatKhauMoi")] NhanVienChangePassword nhanVienChangePassword)
        {
            if (ModelState.IsValid)
            {
                int manv = Convert.ToInt32(Session["idadmin"]);
                NhanVien nhanVien = db.NhanViens.Find(manv);
                if (nhanVien == null)
                {
                    return HttpNotFound();
                }
                string mk = Models.CommonModel.SHA1.ComputeHash(nhanVienChangePassword.MatKhau);
                nhanVienChangePassword.MatKhau = mk;
                
                if (nhanVien.MatKhau == nhanVienChangePassword.MatKhau)
                {
                    nhanVienChangePassword.MatKhauMoi = Models.CommonModel.SHA1.ComputeHash(nhanVienChangePassword.MatKhauMoi);
                    nhanVienChangePassword.XacNhanMatKhauMoi = nhanVienChangePassword.MatKhauMoi;

                    nhanVien.MatKhau = nhanVienChangePassword.MatKhauMoi;
                   
                    db.Entry(nhanVien).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Đổi mật khẩu thành công! hãy đăng nhập lại";
                    return RedirectToAction("Logout");
                }
                else
                {
                    ViewBag.error = "Mật khẩu cũ không đúng !!!";
                    return View();
                }

            }
            return View(nhanVienChangePassword);
        }

    }
}
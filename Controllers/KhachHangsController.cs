using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations.Sql;
using System.Deployment.Internal;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ngay8thang3_Complete.Models;
using ngay8thang3_Complete.Models.CommonModel;

namespace ngay8thang3_Complete.Controllers
{
    public class KhachHangsController : Controller
    {
        private MyShopDbContext db = new MyShopDbContext();

        // GET: KhachHangs

        //GET: đăng ký
        public ActionResult ThongTinTaiKhoan(int id)
        {
            var tk = db.KhachHangs.Where(m => m.ID == id);

            return View(tk);
        }

        public ActionResult Register()
        {
            return View();
        }
        //thêm mới bình thu0wongf
        //public ActionResult Register(KhachHang _user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var check = db.KhachHangs.FirstOrDefault(s => s.TenDangNhap == _user.TenDangNhap);
        //        if (check == null)
        //        {
        //            _user.MatKhau = GetMD5(_user.MatKhau); //mã hóa mật khẩu
        //            db.Configuration.ValidateOnSaveEnabled = false;
        //            db.KhachHangs.Add(_user); //Thêm tài khoản vào db
        //            db.SaveChanges();
        //            ViewBag.success = "Đăng ký thành công!";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.error = "Tài khoản đã tồn tại";
        //        }
        //    }
        //    return View();
        //}
        [HttpPost]
        public JsonResult Register(string hoten, string dienthoai, string email, string diachi, string tendangnhap, string matkhau)
        {

            try
            {
                var ex = db.KhachHangs.Where(x => x.TenDangNhap == tendangnhap).FirstOrDefault();
                if (ex != null)
                {
                    return Json(new { code = 500, msg = "Tài khoản đã tồn tại" + ex }, JsonRequestBehavior.AllowGet);
                }
                else if (db.KhachHangs.SingleOrDefault(x => x.Email == email) != null)
                {
                    return Json(new { code = 500, msg = "Email đã tồn tại, hãy thử 1 Email khác" + ex }, JsonRequestBehavior.AllowGet);
                }
                
                string mk = GetMD5(matkhau);
                KhachHang kh = new KhachHang();
                kh.HoVaten = hoten;
                kh.DienThoai = dienthoai;
                kh.DiaChi = diachi;
                kh.TenDangNhap = tendangnhap;
                kh.MatKhau = mk;

                db.KhachHangs.Add(kh);
                db.SaveChanges();
                TempData["result"] = "đăng ký thành công!";
                return Json(new { code = 200, msg = "đăng ký thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "đăng ký Thất bại" + ex }, JsonRequestBehavior.AllowGet);
            }
        }

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
        //Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(KhachHangModel model)
        {
            if (ModelState.IsValid)
            {
                var f_pass = GetMD5(model.MatKhau);
                var user = db.KhachHangs.Where(m => m.TenDangNhap.Contains(model.TenDangNhap) && m.MatKhau.Contains(f_pass)).FirstOrDefault();
                if (user == null)
                {
                    ViewBag.Login = "tên tài khoản hoặc mật khẩu không chính xác";
                    return View("Index");
                }
                else
                {
                    Session["iduser"] = user.ID;
                    Session["username"] = user.TenDangNhap;
                    Session["fullname"] = user.HoVaten;
                    return RedirectToAction("Index", "Home");
                }
            }


            return View(model);


            //var f_pass = GetMD5(password);
            //var data = db.KhachHangs.Where(u => u.TenDangNhap.Contains(username) && u.MatKhau.Contains(f_pass)).FirstOrDefault(); //firstOrDefault lấy dữ liệu của model kiểu đổi tượng.
            //                                                                                                                      //firstordefault 
            //if (data == null)
            //{
            //    ViewBag.Login = "Tên tài khoản hoặc mật khẩu không chính xác";
            //    return View("Index");

            //}
            //else
            //{
            //    Session["fullname"] = data.HoVaten;
            //    Session["username"] = data.TenDangNhap;
            //    Session["iduser"] = data.ID;

            //    return RedirectToAction("Index", "DongHoes");
            //}





        }
        public ActionResult Logout()
        {
            Session["fullname"] = null;
            Session["username"] = null;
            Session["iduser"] = null;
            return RedirectToAction("Index", "DongHoes");
        }

        // GET: KhachHangs/Details/5

        public JsonResult ThongTinCaNhan(int id, string hoten, string diachi, string dienthoai)
        {
            try
            {
                var dt = db.KhachHangs.SingleOrDefault(x => x.ID == id);
                dt.HoVaten = hoten;
                dt.DiaChi = diachi;
                dt.DienThoai = dienthoai;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Cập nhật thành công", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Cập nhật Thất bại" + ex.Message, JsonRequestBehavior.AllowGet });
            }
        }
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <returns></returns>

        [HttpPost]

        public JsonResult ChangePassword(int id, string matkhaucu, string matkhaumoi)
        {

            try
            {
                string fcu = GetMD5(matkhaucu);
                string fmoi = GetMD5(matkhaumoi);
                var dt = db.KhachHangs.SingleOrDefault(x => x.ID == id);
                if (dt.MatKhau != fcu)
                {
                    return Json(new { code = 500, msg = "Mật khẩu cũ không chính xác" }, JsonRequestBehavior.AllowGet);


                }
                else
                {
                    dt.MatKhau = fmoi;
                    db.SaveChanges();
                    Session["fullname"] = null;
                    Session["username"] = null;
                    Session["iduser"] = null;
                    return Json(new { code = 200, msg = "Đổi mật khẩu thành công hãy đăng nhập lại!" }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Đổi mk thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }


        // GET: KhachHangs/Create
        [HttpGet]
        public JsonResult ChiTiet(int id)
        {

            try
            {
                db.Configuration.ProxyCreationEnabled = false; //vòng lặp. tham chiếu tròn
                var dt = db.KhachHangs.SingleOrDefault(x => x.ID == id);
                string mk = GetMD5(dt.MatKhau);
                dt.MatKhau = mk;

                return Json(new { code = 200, dt = dt, msg = "lấy thông tin thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "lấy thông tin thất bại!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //sửa thông tin

        [HttpPost]
        public JsonResult SuaThongTin(int id, string hoten, string email, string diachi, string dienthoai)
        {
            try
            {
                if(db.KhachHangs.SingleOrDefault(x=>x.Email == email)!=null)
                {

                    return Json(new { code = 500, msg = "Email đã tồn tại, hãy thử 1 email khác" }, JsonRequestBehavior.AllowGet);

                }
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                var match = regex.Match(email);
                if (match.Success)
                {
                    var dt = db.KhachHangs.SingleOrDefault(x => x.ID == id);
                    dt.HoVaten = hoten;
                    dt.Email = email;
                    dt.DiaChi = diachi;
                    dt.DienThoai = dienthoai;
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Sửa thông tin thành công" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { code = 500, msg = "Email không đúng định dạng" }, JsonRequestBehavior.AllowGet);

                }


            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Sửa không thành công" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public PartialViewResult Edit()
        {
            int id = Convert.ToInt32(Session["iduser"]);
            KhachHang khachHang = db.KhachHangs.Find(id);

            return PartialView("_Edit", khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoVaten,DienThoai,DiaChi,TenDangNhap,MatKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThongTinTaiKhoan");
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //đơn hàng của tôi 

        public PartialViewResult MyOrders()
        {
            int makh = Convert.ToInt32(Session["iduser"]);
            
            var Myorders = (from dh in db.DongHoes
                            join ct in db.DatHang_ChiTiet on dh.ID equals ct.DongHo_ID
                            join dhang in db.DatHangs on ct.DatHang_ID equals dhang.ID
                            join kh in db.KhachHangs on dhang.KhachHang_ID equals kh.ID
                            where (kh.ID == makh)

                            select new Myorders()
                            {
                                TenDongHo = dh.TenDongHo,
                                HinhAnhDH = dh.HinhAnhDH,
                                DonGia = ct.DonGia,
                                ID = kh.ID,
                                SoLuong = ct.SoLuong,
                                NgayDatHang = dhang.NgayDatHang,
                                TinhTrang = dhang.TinhTrang

                            }).OrderByDescending(dhang => dhang.NgayDatHang).ToList();


            return PartialView("MyOrders", Myorders);
        }
        //mua nhiều nhất
        public PartialViewResult BuyMost()
        {
            var muanhieu = from dh in db.DongHoes
                           join ctdh in db.DatHang_ChiTiet on dh.ID equals ctdh.DongHo_ID
                           join dhang in db.DatHangs on ctdh.DatHang_ID equals dhang.ID
                           select new BestSaleModels()
                           {
                               ID = dh.ID,
                               TenDongHo = dh.TenDongHo,
                               DonGia = ctdh.DonGia,
                               HinhAnhDH = dh.HinhAnhDH,
                               SoLuong = ctdh.SoLuong
                           };
            ViewBag.sl = db.DatHang_ChiTiet.Count(x => x.ID == 2);

            return PartialView("_MuaNhieu", muanhieu);
        }
        public PartialViewResult BuyMost1()
        {
            var muanhieu = from dh in db.DongHoes
                           join ctdh in db.DatHang_ChiTiet on dh.ID equals ctdh.DongHo_ID
                           join dhang in db.DatHangs on ctdh.DatHang_ID equals dhang.ID
                           select new BestSaleModels()
                           {
                               ID = dh.ID,
                               TenDongHo = dh.TenDongHo,
                               DonGia = ctdh.DonGia,
                               HinhAnhDH = dh.HinhAnhDH,
                               SoLuong = ctdh.SoLuong
                           };
            ViewBag.sl = db.DatHang_ChiTiet.Count(x => x.ID == 2);

            return PartialView("_XemThem", muanhieu);
        }
        
        [HttpPost]
        public ActionResult Forgotpass(string email)
        {
            // tiến hành thực hiện chức năng quên mật khẩu
            string email1 = email;
            var account = db.KhachHangs.FirstOrDefault(x => x.Email == email1);
            if (account != null)
            {
                // tiến hành gửi mail
                var mail = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("manhzeze154@gmail.com", "893172pro"),
                    //ebpgrllbqsfudqsi

                    EnableSsl = true

                };
                var message = new MailMessage();
                message.From = new MailAddress("manhzeze154@gmail.com");
                message.ReplyToList.Add("manhzeze154@gmail.com");
                message.To.Add(new MailAddress(account.Email));
                message.Subject = "Thông báo về việc thay đổi mật khẩu của tài khoản" + account.TenDangNhap;
                string new_pass = RandomMK();
                message.Body = "Mật khẩu của bạn đã được reset thành: " + new_pass;
                account.MatKhau = GetMD5(new_pass);
                db.SaveChanges();
                mail.Send(message);
                TempData["ThongBao"] = "Đã gửi mail thành công !!";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["ThongBao"] = "Địa chỉ email của tài khoản không chính xác";
                return RedirectToAction("Index");
            }
        }

        public string RandomMK()
        {
            //chúng ta sẽ tạo hàm random mật khẩu
            string numberRandom_str;
            Random rd = new Random();
            numberRandom_str = rd.Next(100000, 1000000).ToString();
            return numberRandom_str;
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

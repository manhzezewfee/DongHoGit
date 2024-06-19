using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ngay8thang3_Complete.Models.CommonModel
{
    public class NhanVienChangePassword
    {
        
            [Display(Name = "Mật khẩu cũ")]    
            [DataType(DataType.Password)]
            public string MatKhau { get; set; }

            [Display(Name = "Mật khẩu mới")]
            [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
            [MaxLength(100, ErrorMessage = "Mật khẩu tối đa 100 kí tự")]
            [MinLength(3, ErrorMessage = "Mật khẩu tối thiểu 3 kí tự")]
            [DataType(DataType.Password)]
            public string MatKhauMoi { get; set; }

            [Display(Name = "Xác nhận mật khẩu mới")]
            [Required(ErrorMessage = "Xác nhận khẩu không được bỏ trống!")]
            [DataType(DataType.Password)]
            [Compare("MatKhauMoi", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
            public string XacNhanMatKhauMoi { get; set; }

        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ngay8thang3_Complete.Models.CommonModel
{
    public class DonHang_ThongTinChiTiet
    {
        public int maDonHang { get; set; }
        public string HoTen { get; set; }
        public string SoDTGiaoHang { get; set; }
        public DateTime NgayDatHang { get; set; }
        public short? TinhTrang { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
    }
}
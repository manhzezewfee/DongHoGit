﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ngay8thang3_Complete.Models.CommonModel
{
    // đơn hàng của tôi viewmodel
    public class Myorders
    {
        public int ID { get; set; }
        public string TenDongHo { get; set; }
        public string HinhAnhDH { get; set; }
        public HttpPostedFileBase DuLieuHinhAnhDH { get; set; }
        public Nullable<int> DonGia { get; set; }
        public Nullable<short> SoLuong { get; set; }
        public Nullable<System.DateTime> NgayDatHang { get; set; }
        public short? TinhTrang { get; set; }

    }
    public class BestSaleModels
    {

        public int ID { get; set; }
        public string TenDongHo { get; set; }
        public string HinhAnhDH { get; set; }
        public Nullable<int> DonGia { get; set; }
        public Nullable<short> SoLuong { get; set; }

    }
}
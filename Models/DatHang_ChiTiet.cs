namespace ngay8thang3_Complete.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DatHang_ChiTiet
    {
        public int ID { get; set; }

        public int? DatHang_ID { get; set; }

        public int? DongHo_ID { get; set; }

        public short? SoLuong { get; set; }

        public int? DonGia { get; set; }

        public virtual DatHang DatHang { get; set; }

        public virtual DongHo DongHo { get; set; }
    }
}

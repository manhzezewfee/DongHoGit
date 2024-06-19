namespace ngay8thang3_Complete.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DongHo")]
    public partial class DongHo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DongHo()
        {
            DatHang_ChiTiet = new HashSet<DatHang_ChiTiet>();
        }

        public int ID { get; set; }

        public int? ThuongHieu_ID { get; set; }

        public int? TenLoai_ID { get; set; }

        public int? ChatLieu_ID { get; set; }

        public int? XuatXu_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string TenDongHo { get; set; }

        [Required]
        [StringLength(255)]
        public string MauSac { get; set; }

        public int? HanBaoHanh { get; set; }

        public int DonGia { get; set; }

        public int SoLuong { get; set; }

        [StringLength(255)]
        public string HinhAnhDH { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTaNgan { get; set; }

        [Column(TypeName = "ntext")]
        public string Đaciem { get; set; }

        public int? KhuyenMai { get; set; }

        public int GiaNhap { get; set; }

        public virtual ChatLieu ChatLieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang_ChiTiet> DatHang_ChiTiet { get; set; }

        public virtual LoaiDH LoaiDH { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }

        public virtual XuatXu XuatXu { get; set; }
    }
}

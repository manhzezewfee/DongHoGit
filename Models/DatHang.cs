namespace ngay8thang3_Complete.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DatHang")]
    public partial class DatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DatHang()
        {
            DatHang_ChiTiet = new HashSet<DatHang_ChiTiet>();
        }

        public int ID { get; set; }

        public int? NhanVien_ID { get; set; }

        public int? KhachHang_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string DienThoaiGiaoHang { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChiGiaoHang { get; set; }

        public DateTime? NgayDatHang { get; set; }

        public short? TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang_ChiTiet> DatHang_ChiTiet { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}

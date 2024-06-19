namespace ngay8thang3_Complete.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DatHang = new HashSet<DatHang>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string HoVaten { get; set; }

        [StringLength(10)]
        public string DienThoai { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [StringLength(255)]
        public string MatKhau { get; set; }

        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang> DatHang { get; set; }
    }
}

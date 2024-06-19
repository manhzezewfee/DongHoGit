namespace ngay8thang3_Complete.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            DatHang = new HashSet<DatHang>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string HoVaTen { get; set; }

        [StringLength(20)]
        public string DienThoai { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [StringLength(255)]
        public string MatKhau { get; set; }

        public bool? Quyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang> DatHang { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ngay8thang3_Complete.Models
{
    public partial class MyShopDbContext : DbContext
    {
        public MyShopDbContext()
            : base("name=MyShopDbContext")
        {
        }

        public virtual DbSet<ChatLieu> ChatLieux { get; set; }
        public virtual DbSet<DatHang> DatHangs { get; set; }
        public virtual DbSet<DatHang_ChiTiet> DatHang_ChiTiet { get; set; }
        public virtual DbSet<DongHo> DongHoes { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiDH> LoaiDHs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieux { get; set; }
        public virtual DbSet<XuatXu> XuatXus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatLieu>()
                .HasMany(e => e.DongHoes)
                .WithOptional(e => e.ChatLieu)
                .HasForeignKey(e => e.ChatLieu_ID);

            modelBuilder.Entity<DatHang>()
                .HasMany(e => e.DatHang_ChiTiet)
                .WithOptional(e => e.DatHang)
                .HasForeignKey(e => e.DatHang_ID);

            modelBuilder.Entity<DongHo>()
                .HasMany(e => e.DatHang_ChiTiet)
                .WithOptional(e => e.DongHo)
                .HasForeignKey(e => e.DongHo_ID);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DatHangs)
                .WithOptional(e => e.KhachHang)
                .HasForeignKey(e => e.KhachHang_ID);

            modelBuilder.Entity<LoaiDH>()
                .HasMany(e => e.DongHoes)
                .WithOptional(e => e.LoaiDH)
                .HasForeignKey(e => e.TenLoai_ID);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.DatHangs)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.NhanVien_ID);

            modelBuilder.Entity<ThuongHieu>()
                .HasMany(e => e.DongHoes)
                .WithOptional(e => e.ThuongHieu)
                .HasForeignKey(e => e.ThuongHieu_ID);

            modelBuilder.Entity<XuatXu>()
                .HasMany(e => e.DongHoes)
                .WithOptional(e => e.XuatXu)
                .HasForeignKey(e => e.XuatXu_ID);
        }
    }
}

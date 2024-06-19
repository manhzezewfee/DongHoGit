namespace ngay8thang3_Complete.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatLieu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenChatLieu = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DongHo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ThuongHieu_ID = c.Int(),
                        TenLoai_ID = c.Int(),
                        ChatLieu_ID = c.Int(),
                        XuatXu_ID = c.Int(),
                        TenDongHo = c.String(nullable: false, maxLength: 255),
                        MauSac = c.String(nullable: false, maxLength: 255),
                        HanBaoHanh = c.Int(),
                        DonGia = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        HinhAnhDH = c.String(maxLength: 255),
                        MoTa = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LoaiDH", t => t.TenLoai_ID)
                .ForeignKey("dbo.ThuongHieu", t => t.ThuongHieu_ID)
                .ForeignKey("dbo.XuatXu", t => t.XuatXu_ID)
                .ForeignKey("dbo.ChatLieu", t => t.ChatLieu_ID)
                .Index(t => t.ThuongHieu_ID)
                .Index(t => t.TenLoai_ID)
                .Index(t => t.ChatLieu_ID)
                .Index(t => t.XuatXu_ID);
            
            CreateTable(
                "dbo.DatHang_ChiTiet",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatHang_ID = c.Int(),
                        DongHo_ID = c.Int(),
                        SoLuong = c.Short(),
                        DonGia = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DatHang", t => t.DatHang_ID)
                .ForeignKey("dbo.DongHo", t => t.DongHo_ID)
                .Index(t => t.DatHang_ID)
                .Index(t => t.DongHo_ID);
            
            CreateTable(
                "dbo.DatHang",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NhanVien_ID = c.Int(),
                        KhachHang_ID = c.Int(),
                        DienThoaiGiaoHang = c.String(maxLength: 20),
                        DiaChiGiaoHang = c.String(maxLength: 255),
                        NgayDatHang = c.DateTime(),
                        TinhTrang = c.Short(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.KhachHang", t => t.KhachHang_ID)
                .ForeignKey("dbo.NhanVien", t => t.NhanVien_ID)
                .Index(t => t.NhanVien_ID)
                .Index(t => t.KhachHang_ID);
            
            CreateTable(
                "dbo.KhachHang",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HoVaten = c.String(nullable: false, maxLength: 100),
                        DienThoai = c.String(maxLength: 10),
                        DiaChi = c.String(nullable: false, maxLength: 255),
                        TenDangNhap = c.String(nullable: false, maxLength: 50),
                        MatKhau = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NhanVien",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HoVaTen = c.String(maxLength: 100),
                        DienThoai = c.String(maxLength: 20),
                        DiaChi = c.String(maxLength: 255),
                        TenDangNhap = c.String(maxLength: 50),
                        MatKhau = c.String(maxLength: 255),
                        Quyen = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LoaiDH",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenLoai = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ThuongHieu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenThuongHieu = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.XuatXu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenQG = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DongHo", "ChatLieu_ID", "dbo.ChatLieu");
            DropForeignKey("dbo.DongHo", "XuatXu_ID", "dbo.XuatXu");
            DropForeignKey("dbo.DongHo", "ThuongHieu_ID", "dbo.ThuongHieu");
            DropForeignKey("dbo.DongHo", "TenLoai_ID", "dbo.LoaiDH");
            DropForeignKey("dbo.DatHang_ChiTiet", "DongHo_ID", "dbo.DongHo");
            DropForeignKey("dbo.DatHang", "NhanVien_ID", "dbo.NhanVien");
            DropForeignKey("dbo.DatHang", "KhachHang_ID", "dbo.KhachHang");
            DropForeignKey("dbo.DatHang_ChiTiet", "DatHang_ID", "dbo.DatHang");
            DropIndex("dbo.DatHang", new[] { "KhachHang_ID" });
            DropIndex("dbo.DatHang", new[] { "NhanVien_ID" });
            DropIndex("dbo.DatHang_ChiTiet", new[] { "DongHo_ID" });
            DropIndex("dbo.DatHang_ChiTiet", new[] { "DatHang_ID" });
            DropIndex("dbo.DongHo", new[] { "XuatXu_ID" });
            DropIndex("dbo.DongHo", new[] { "ChatLieu_ID" });
            DropIndex("dbo.DongHo", new[] { "TenLoai_ID" });
            DropIndex("dbo.DongHo", new[] { "ThuongHieu_ID" });
            DropTable("dbo.XuatXu");
            DropTable("dbo.ThuongHieu");
            DropTable("dbo.LoaiDH");
            DropTable("dbo.NhanVien");
            DropTable("dbo.KhachHang");
            DropTable("dbo.DatHang");
            DropTable("dbo.DatHang_ChiTiet");
            DropTable("dbo.DongHo");
            DropTable("dbo.ChatLieu");
        }
    }
}

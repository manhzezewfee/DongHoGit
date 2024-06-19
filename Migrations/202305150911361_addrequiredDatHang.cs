namespace ngay8thang3_Complete.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiredDatHang : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DatHang", "DienThoaiGiaoHang", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.DatHang", "DiaChiGiaoHang", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DatHang", "DiaChiGiaoHang", c => c.String(maxLength: 255));
            AlterColumn("dbo.DatHang", "DienThoaiGiaoHang", c => c.String(maxLength: 20));
        }
    }
}

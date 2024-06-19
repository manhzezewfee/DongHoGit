namespace ngay8thang3_Complete.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThuongHieu : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ThuongHieu", "TenThuongHieu", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ThuongHieu", "TenThuongHieu", c => c.String(maxLength: 255));
        }
    }
}

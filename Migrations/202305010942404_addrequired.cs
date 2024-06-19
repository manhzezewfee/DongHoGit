namespace ngay8thang3_Complete.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LoaiDH", "TenLoai", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoaiDH", "TenLoai", c => c.String(maxLength: 255));
        }
    }
}

namespace ngay8thang3_Complete.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_objKhuyenMai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DongHo", "KhuyenMai", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DongHo", "KhuyenMai");
        }
    }
}

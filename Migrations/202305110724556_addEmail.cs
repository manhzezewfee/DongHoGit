namespace ngay8thang3_Complete.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KhachHang", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KhachHang", "Email");
        }
    }
}

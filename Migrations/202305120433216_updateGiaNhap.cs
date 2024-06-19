namespace ngay8thang3_Complete.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateGiaNhap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DongHo", "GiaNhap", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DongHo", "GiaNhap");
        }
    }
}

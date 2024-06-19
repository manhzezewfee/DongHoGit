namespace ngay8thang3_Complete.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class objectObjDongHo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DongHo", "Đaciem", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DongHo", "Đaciem");
        }
    }
}

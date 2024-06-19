namespace ngay8thang3_Complete.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class objectMoTaDongHo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DongHo", "MoTaNgan", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DongHo", "MoTaNgan");
        }
    }
}

namespace MyChicken.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderProducts", "QtyAmount", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "TotalAmount", c => c.Double(nullable: false));
            DropColumn("dbo.OrderProducts", "Total");
            DropColumn("dbo.Orders", "TotalAmout");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "TotalAmout", c => c.Double(nullable: false));
            AddColumn("dbo.OrderProducts", "Total", c => c.Double(nullable: false));
            DropColumn("dbo.Orders", "TotalAmount");
            DropColumn("dbo.OrderProducts", "QtyAmount");
        }
    }
}

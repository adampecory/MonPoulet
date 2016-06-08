namespace MyChicken.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V5_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DeliveryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "DelivryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "DelivryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "DeliveryDate");
        }
    }
}

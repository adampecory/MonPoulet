namespace MyChicken.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V3_User_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "DeliveryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Comment", c => c.String());
            AddColumn("dbo.AspNetUsers", "Tel", c => c.String());
            AddColumn("dbo.AspNetUsers", "Email", c => c.String());
            AddColumn("dbo.AspNetUsers", "Adresse", c => c.String());
            DropColumn("dbo.Orders", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "Adresse");
            DropColumn("dbo.AspNetUsers", "Email");
            DropColumn("dbo.AspNetUsers", "Tel");
            DropColumn("dbo.Orders", "Comment");
            DropColumn("dbo.Orders", "DeliveryDate");
            DropColumn("dbo.Orders", "OrderDate");
        }
    }
}

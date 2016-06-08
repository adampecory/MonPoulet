namespace MyChicken.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V3_User_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "DelivryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Tel", c => c.String(maxLength: 10));
            AddColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Adresse", c => c.String(maxLength: 100));
            DropColumn("dbo.Orders", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "Adresse");
            DropColumn("dbo.AspNetUsers", "Email");
            DropColumn("dbo.AspNetUsers", "Tel");
            DropColumn("dbo.Orders", "DelivryDate");
            DropColumn("dbo.Orders", "OrderDate");
        }
    }
}

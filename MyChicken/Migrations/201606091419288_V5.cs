namespace MyChicken.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "UserID");
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "UserID");
            AlterColumn("dbo.Orders", "UserID", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "UserID", c => c.Long(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "UserID", newName: "User_Id");
            AddColumn("dbo.Orders", "UserID", c => c.Long(nullable: false));
        }
    }
}

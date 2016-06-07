namespace MyChicken.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Statut : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Statut", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Statut");
        }
    }
}

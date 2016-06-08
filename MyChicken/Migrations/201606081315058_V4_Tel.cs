namespace MyChicken.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V4_Tel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Tel");
            AddColumn("dbo.AspNetUsers", "Tel", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
        }
    }
}

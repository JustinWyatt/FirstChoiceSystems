namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvailableForMarket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "AvailableForMarket", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "AvailableForMarket");
        }
    }
}

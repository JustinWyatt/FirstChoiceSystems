namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "CashCost", c => c.Double(nullable: false));
            AddColumn("dbo.Items", "RevenueInCash", c => c.Double(nullable: false));
            AddColumn("dbo.Items", "RevenueInTradeDollars", c => c.Double(nullable: false));
            AddColumn("dbo.Items", "CashEquivalentValue", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "CompanyPhoto", c => c.String());
            AddColumn("dbo.AspNetUsers", "RepresentativePhoto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RepresentativePhoto");
            DropColumn("dbo.AspNetUsers", "CompanyPhoto");
            DropColumn("dbo.Items", "CashEquivalentValue");
            DropColumn("dbo.Items", "RevenueInTradeDollars");
            DropColumn("dbo.Items", "RevenueInCash");
            DropColumn("dbo.Items", "CashCost");
        }
    }
}

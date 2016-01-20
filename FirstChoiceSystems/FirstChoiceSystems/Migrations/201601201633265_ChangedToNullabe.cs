namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedToNullabe : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "PricePerUnit", c => c.Double());
            AlterColumn("dbo.Items", "CashCost", c => c.Double());
            AlterColumn("dbo.Items", "RevenueInCash", c => c.Double());
            AlterColumn("dbo.Items", "RevenueInTradeDollars", c => c.Double());
            AlterColumn("dbo.Items", "CashEquivalentValue", c => c.Double());
            AlterColumn("dbo.PurchaseItems", "PricePerUnitBoughtAt", c => c.Double());
            AlterColumn("dbo.AspNetUsers", "Balance", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Balance", c => c.Double(nullable: false));
            AlterColumn("dbo.PurchaseItems", "PricePerUnitBoughtAt", c => c.Double(nullable: false));
            AlterColumn("dbo.Items", "CashEquivalentValue", c => c.Double(nullable: false));
            AlterColumn("dbo.Items", "RevenueInTradeDollars", c => c.Double(nullable: false));
            AlterColumn("dbo.Items", "RevenueInCash", c => c.Double(nullable: false));
            AlterColumn("dbo.Items", "CashCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Items", "PricePerUnit", c => c.Double(nullable: false));
        }
    }
}

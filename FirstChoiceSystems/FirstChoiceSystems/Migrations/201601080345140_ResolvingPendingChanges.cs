namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResolvingPendingChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseItems", "DatePurchased", c => c.DateTime(nullable: false));
            DropColumn("dbo.PurchaseItems", "CreatedOn");
            DropColumn("dbo.PurchaseItems", "ModifiedOn");
            DropColumn("dbo.PurchaseItems", "ModifiedBy");
            DropColumn("dbo.PurchaseItems", "CreatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseItems", "CreatedBy", c => c.String());
            AddColumn("dbo.PurchaseItems", "ModifiedBy", c => c.String());
            AddColumn("dbo.PurchaseItems", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.PurchaseItems", "CreatedOn", c => c.DateTime());
            DropColumn("dbo.PurchaseItems", "DatePurchased");
        }
    }
}

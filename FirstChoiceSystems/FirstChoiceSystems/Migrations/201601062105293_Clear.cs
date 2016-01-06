namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clear : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Inventory_Id", "dbo.Inventories");
            DropForeignKey("dbo.Inventories", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Items", new[] { "Inventory_Id" });
            DropIndex("dbo.Inventories", new[] { "Owner_Id" });
            AddColumn("dbo.Items", "ItemName", c => c.String());
            AlterColumn("dbo.Items", "PricePerUnit", c => c.Double(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Balance", c => c.Double(nullable: false));
            AlterColumn("dbo.PurchaseItems", "PricePerUnitBoughtAt", c => c.Double(nullable: false));
            DropColumn("dbo.Items", "Inventory_Id");
            DropTable("dbo.Inventories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedBy = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Items", "Inventory_Id", c => c.Int());
            AlterColumn("dbo.PurchaseItems", "PricePerUnitBoughtAt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AspNetUsers", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Items", "PricePerUnit", c => c.Int(nullable: false));
            DropColumn("dbo.Items", "ItemName");
            CreateIndex("dbo.Inventories", "Owner_Id");
            CreateIndex("dbo.Items", "Inventory_Id");
            AddForeignKey("dbo.Inventories", "Owner_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Items", "Inventory_Id", "dbo.Inventories", "Id");
        }
    }
}

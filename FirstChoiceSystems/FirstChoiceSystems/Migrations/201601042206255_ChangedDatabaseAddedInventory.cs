namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDatabaseAddedInventory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Categories", newName: "BusinessCategories");
            CreateTable(
                "dbo.ItemImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Business_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Business_Id)
                .Index(t => t.Business_Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemDescription = c.String(),
                        Price = c.Int(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        DateSold = c.DateTime(),
                        ItemCategory_Id = c.Int(),
                        Inventory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemCategories", t => t.ItemCategory_Id)
                .ForeignKey("dbo.Inventories", t => t.Inventory_Id)
                .Index(t => t.ItemCategory_Id)
                .Index(t => t.Inventory_Id);
            
            CreateTable(
                "dbo.ItemCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Transactions", "DateOfApproval", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Inventory_Id", "dbo.Inventories");
            DropForeignKey("dbo.Items", "ItemCategory_Id", "dbo.ItemCategories");
            DropForeignKey("dbo.ItemImages", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.Inventories", "Business_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Items", new[] { "Inventory_Id" });
            DropIndex("dbo.Items", new[] { "ItemCategory_Id" });
            DropIndex("dbo.Inventories", new[] { "Business_Id" });
            DropIndex("dbo.ItemImages", new[] { "Item_Id" });
            AlterColumn("dbo.Transactions", "DateOfApproval", c => c.DateTime(nullable: false));
            DropTable("dbo.ItemCategories");
            DropTable("dbo.Items");
            DropTable("dbo.Inventories");
            DropTable("dbo.ItemImages");
            RenameTable(name: "dbo.BusinessCategories", newName: "Categories");
        }
    }
}

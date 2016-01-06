namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingPendingChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Sale_Id", "dbo.Sales");
            DropForeignKey("dbo.Sales", "ItemSold_Id", "dbo.Items");
            DropForeignKey("dbo.Sales", "Seller_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Sale_Id" });
            DropIndex("dbo.Sales", new[] { "ItemSold_Id" });
            DropIndex("dbo.Sales", new[] { "Seller_Id" });
            RenameColumn(table: "dbo.Inventories", name: "Business_Id", newName: "Owner_Id");
            RenameIndex(table: "dbo.Inventories", name: "IX_Business_Id", newName: "IX_Owner_Id");
            AlterColumn("dbo.AspNetUsers", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AspNetUsers", "Sale_Id");
            DropTable("dbo.Sales");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        SaleAmount = c.Double(nullable: false),
                        DateSold = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedBy = c.String(),
                        ItemSold_Id = c.Int(),
                        Seller_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Sale_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Balance", c => c.Double(nullable: false));
            RenameIndex(table: "dbo.Inventories", name: "IX_Owner_Id", newName: "IX_Business_Id");
            RenameColumn(table: "dbo.Inventories", name: "Owner_Id", newName: "Business_Id");
            CreateIndex("dbo.Sales", "Seller_Id");
            CreateIndex("dbo.Sales", "ItemSold_Id");
            CreateIndex("dbo.AspNetUsers", "Sale_Id");
            AddForeignKey("dbo.Sales", "Seller_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Sales", "ItemSold_Id", "dbo.Items", "Id");
            AddForeignKey("dbo.AspNetUsers", "Sale_Id", "dbo.Sales", "Id");
        }
    }
}

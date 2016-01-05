namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSalesTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Transactions", newName: "Purchases");
            DropForeignKey("dbo.Transactions", "Business_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Purchases", new[] { "Seller_Id" });
            DropColumn("dbo.Purchases", "Business_Id");
            RenameColumn(table: "dbo.Purchases", name: "Seller_Id", newName: "Business_Id");
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        SaleAmount = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedBy = c.String(),
                        ItemSold_Id = c.Int(),
                        Seller_Id = c.String(maxLength: 128),
                        Business_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemSold_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Seller_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Business_Id)
                .Index(t => t.ItemSold_Id)
                .Index(t => t.Seller_Id)
                .Index(t => t.Business_Id);
            
            AddColumn("dbo.AspNetUsers", "Purchase_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Sale_Id", c => c.Int());
            AddColumn("dbo.Items", "Purchase_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Balance", c => c.Double(nullable: false));
            AlterColumn("dbo.Purchases", "Amount", c => c.Double(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Purchase_Id");
            CreateIndex("dbo.AspNetUsers", "Sale_Id");
            CreateIndex("dbo.Items", "Purchase_Id");
            AddForeignKey("dbo.Items", "Purchase_Id", "dbo.Purchases", "Id");
            AddForeignKey("dbo.AspNetUsers", "Purchase_Id", "dbo.Purchases", "Id");
            AddForeignKey("dbo.AspNetUsers", "Sale_Id", "dbo.Sales", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "Business_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sales", "Seller_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sales", "ItemSold_Id", "dbo.Items");
            DropForeignKey("dbo.AspNetUsers", "Sale_Id", "dbo.Sales");
            DropForeignKey("dbo.AspNetUsers", "Purchase_Id", "dbo.Purchases");
            DropForeignKey("dbo.Items", "Purchase_Id", "dbo.Purchases");
            DropIndex("dbo.Sales", new[] { "Business_Id" });
            DropIndex("dbo.Sales", new[] { "Seller_Id" });
            DropIndex("dbo.Sales", new[] { "ItemSold_Id" });
            DropIndex("dbo.Items", new[] { "Purchase_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Sale_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Purchase_Id" });
            AlterColumn("dbo.Purchases", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Balance", c => c.Int(nullable: false));
            DropColumn("dbo.Items", "Purchase_Id");
            DropColumn("dbo.AspNetUsers", "Sale_Id");
            DropColumn("dbo.AspNetUsers", "Purchase_Id");
            DropTable("dbo.Sales");
            RenameColumn(table: "dbo.Purchases", name: "Business_Id", newName: "Seller_Id");
            AddColumn("dbo.Purchases", "Business_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Purchases", "Seller_Id");
            AddForeignKey("dbo.Transactions", "Business_Id", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.Purchases", newName: "Transactions");
        }
    }
}

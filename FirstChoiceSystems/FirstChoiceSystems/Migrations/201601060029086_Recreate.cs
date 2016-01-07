    namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedBy = c.String(),
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
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedBy = c.String(),
                        Business_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Business_Id)
                .Index(t => t.Business_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PersonOfContact = c.String(),
                        CompanyName = c.String(),
                        CompanyAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Postal = c.Int(nullable: false),
                        CompanyWebsite = c.String(),
                        Balance = c.Double(nullable: false),
                        AccountNumber = c.Int(nullable: false),
                        DateRegistered = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        BusinessCategory_Id = c.Int(),
                        Sale_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessCategories", t => t.BusinessCategory_Id)
                .ForeignKey("dbo.Sales", t => t.Sale_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.BusinessCategory_Id)
                .Index(t => t.Sale_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemDescription = c.String(),
                        PricePerUnit = c.Int(nullable: false),
                        UnitsAvailable = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedBy = c.String(),
                        ItemCategory_Id = c.Int(),
                        Seller_Id = c.String(maxLength: 128),
                        Inventory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemCategories", t => t.ItemCategory_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Seller_Id)
                .ForeignKey("dbo.Inventories", t => t.Inventory_Id)
                .Index(t => t.ItemCategory_Id)
                .Index(t => t.Seller_Id)
                .Index(t => t.Inventory_Id);
            
            CreateTable(
                "dbo.ItemCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuanityBought = c.Int(nullable: false),
                        PricePerUnitBoughtAt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        ApprovalDate = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedBy = c.String(),
                        Buyer_Id = c.String(maxLength: 128),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Buyer_Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Buyer_Id)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemSold_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Seller_Id)
                .Index(t => t.ItemSold_Id)
                .Index(t => t.Seller_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "Seller_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sales", "ItemSold_Id", "dbo.Items");
            DropForeignKey("dbo.AspNetUsers", "Sale_Id", "dbo.Sales");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Items", "Inventory_Id", "dbo.Inventories");
            DropForeignKey("dbo.Inventories", "Business_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Items", "Seller_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.PurchaseItems", "Buyer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Items", "ItemCategory_Id", "dbo.ItemCategories");
            DropForeignKey("dbo.ItemImages", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "BusinessCategory_Id", "dbo.BusinessCategories");
            DropIndex("dbo.Sales", new[] { "Seller_Id" });
            DropIndex("dbo.Sales", new[] { "ItemSold_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.PurchaseItems", new[] { "Item_Id" });
            DropIndex("dbo.PurchaseItems", new[] { "Buyer_Id" });
            DropIndex("dbo.Items", new[] { "Inventory_Id" });
            DropIndex("dbo.Items", new[] { "Seller_Id" });
            DropIndex("dbo.Items", new[] { "ItemCategory_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Sale_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "BusinessCategory_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Inventories", new[] { "Business_Id" });
            DropIndex("dbo.ItemImages", new[] { "Item_Id" });
            DropTable("dbo.Sales");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.PurchaseItems");
            DropTable("dbo.ItemCategories");
            DropTable("dbo.Items");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Inventories");
            DropTable("dbo.ItemImages");
            DropTable("dbo.BusinessCategories");
        }
    }
}

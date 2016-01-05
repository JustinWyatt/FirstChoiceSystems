namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusinessCategories", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.BusinessCategories", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.BusinessCategories", "ModifiedBy", c => c.String());
            AddColumn("dbo.BusinessCategories", "CreatedBy", c => c.String());
            AddColumn("dbo.ItemImages", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ItemImages", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ItemImages", "ModifiedBy", c => c.String());
            AddColumn("dbo.ItemImages", "CreatedBy", c => c.String());
            AddColumn("dbo.Inventories", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Inventories", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Inventories", "ModifiedBy", c => c.String());
            AddColumn("dbo.Inventories", "CreatedBy", c => c.String());
            AddColumn("dbo.Transactions", "ApprovalDate", c => c.DateTime());
            AddColumn("dbo.Transactions", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transactions", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transactions", "ModifiedBy", c => c.String());
            AddColumn("dbo.Transactions", "CreatedBy", c => c.String());
            AddColumn("dbo.Items", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "ModifiedOn", c => c.DateTime(nullable: false)); 
            AddColumn("dbo.Items", "ModifiedBy", c => c.String());
            AddColumn("dbo.Items", "CreatedBy", c => c.String());
            AddColumn("dbo.Items", "Seller_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ItemCategories", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ItemCategories", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ItemCategories", "ModifiedBy", c => c.String());
            AddColumn("dbo.ItemCategories", "CreatedBy", c => c.String());
            CreateIndex("dbo.Items", "Seller_Id");
            AddForeignKey("dbo.Items", "Seller_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Transactions", "DateOfTransaction");
            DropColumn("dbo.Transactions", "DateOfApproval");
            DropColumn("dbo.Transactions", "IsPending");
            DropColumn("dbo.Items", "DatePosted");
            DropColumn("dbo.Items", "DateUpdated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "DateUpdated", c => c.DateTime());
            AddColumn("dbo.Items", "DatePosted", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transactions", "IsPending", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transactions", "DateOfApproval", c => c.DateTime());
            AddColumn("dbo.Transactions", "DateOfTransaction", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Items", "Seller_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Items", new[] { "Seller_Id" });
            DropColumn("dbo.ItemCategories", "CreatedBy");
            DropColumn("dbo.ItemCategories", "ModifiedBy");
            DropColumn("dbo.ItemCategories", "ModifiedOn");
            DropColumn("dbo.ItemCategories", "CreatedOn");
            DropColumn("dbo.Items", "Seller_Id");
            DropColumn("dbo.Items", "CreatedBy");
            DropColumn("dbo.Items", "ModifiedBy");
            DropColumn("dbo.Items", "ModifiedOn");
            DropColumn("dbo.Items", "CreatedOn");
            DropColumn("dbo.Items", "Quantity");
            DropColumn("dbo.Transactions", "CreatedBy");
            DropColumn("dbo.Transactions", "ModifiedBy");
            DropColumn("dbo.Transactions", "ModifiedOn");
            DropColumn("dbo.Transactions", "CreatedOn");
            DropColumn("dbo.Transactions", "Status");
            DropColumn("dbo.Transactions", "ApprovalDate");
            DropColumn("dbo.Inventories", "CreatedBy");
            DropColumn("dbo.Inventories", "ModifiedBy");
            DropColumn("dbo.Inventories", "ModifiedOn");
            DropColumn("dbo.Inventories", "CreatedOn");
            DropColumn("dbo.ItemImages", "CreatedBy");
            DropColumn("dbo.ItemImages", "ModifiedBy");
            DropColumn("dbo.ItemImages", "ModifiedOn");
            DropColumn("dbo.ItemImages", "CreatedOn");
            DropColumn("dbo.BusinessCategories", "CreatedBy");
            DropColumn("dbo.BusinessCategories", "ModifiedBy");
            DropColumn("dbo.BusinessCategories", "ModifiedOn");
            DropColumn("dbo.BusinessCategories", "CreatedOn");
        }
    }
}

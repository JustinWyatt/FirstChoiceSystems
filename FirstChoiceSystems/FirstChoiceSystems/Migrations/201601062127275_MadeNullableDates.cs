namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeNullableDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusinessCategories", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.BusinessCategories", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.ItemImages", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.ItemImages", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.ItemCategories", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.ItemCategories", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Items", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Items", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.PurchaseItems", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.PurchaseItems", "ModifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseItems", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseItems", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Items", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Items", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ItemCategories", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ItemCategories", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ItemImages", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ItemImages", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BusinessCategories", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BusinessCategories", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}

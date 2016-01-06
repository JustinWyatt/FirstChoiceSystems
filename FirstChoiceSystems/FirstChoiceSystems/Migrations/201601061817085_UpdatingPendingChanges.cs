namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingPendingChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PurchaseItems", "ApprovalDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseItems", "ApprovalDate", c => c.DateTime());
        }
    }
}

namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "DateOfApproval", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "DateOfApproval");
        }
    }
}

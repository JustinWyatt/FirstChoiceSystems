namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPendingOptionToTransactionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "IsPending", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "IsPending");
        }
    }
}

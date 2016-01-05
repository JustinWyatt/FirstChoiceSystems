namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionTweaks : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Transactions", new[] { "Seller_Id" });
            RenameColumn(table: "dbo.AspNetUsers", name: "Seller_Id", newName: "Transaction_Id");
            CreateIndex("dbo.AspNetUsers", "Transaction_Id");
            DropColumn("dbo.Transactions", "Seller_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Seller_Id", c => c.String(maxLength: 128));
            DropIndex("dbo.AspNetUsers", new[] { "Transaction_Id" });
            RenameColumn(table: "dbo.AspNetUsers", name: "Transaction_Id", newName: "Seller_Id");
            CreateIndex("dbo.Transactions", "Seller_Id");
        }
    }
}

namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Business_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "Business_Id");
            AddForeignKey("dbo.Transactions", "Business_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Business_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "Business_Id" });
            DropColumn("dbo.Transactions", "Business_Id");
        }
    }
}

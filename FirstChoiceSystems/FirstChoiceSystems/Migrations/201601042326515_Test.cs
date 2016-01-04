namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "Category_Id", newName: "BusinessCategory_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Category_Id", newName: "IX_BusinessCategory_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_BusinessCategory_Id", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "BusinessCategory_Id", newName: "Category_Id");
        }
    }
}

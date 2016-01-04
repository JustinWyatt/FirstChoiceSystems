namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCategoryTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryName", c => c.String());
            DropColumn("dbo.Categories", "Restaraunt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Restaraunt", c => c.String());
            DropColumn("dbo.Categories", "CategoryName");
        }
    }
}

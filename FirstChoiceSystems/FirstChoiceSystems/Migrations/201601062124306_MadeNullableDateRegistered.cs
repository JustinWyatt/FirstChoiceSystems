namespace FirstChoiceSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeNullableDateRegistered : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "DateRegistered", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DateRegistered", c => c.DateTime(nullable: false));
        }
    }
}

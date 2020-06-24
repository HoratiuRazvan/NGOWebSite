namespace Licenta_V0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateUserRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MagazineModels", "PublishDate", c => c.DateTime(nullable: false));
            Sql("INSERT INTO AspNetRoles(Id,Name) VALUES(1,'Admin')");
            Sql("INSERT INTO AspNetRoles(Id,Name) VALUES(2,'Manager')");
            Sql("INSERT INTO AspNetRoles(Id,Name) VALUES(3,'Editorial')");
            Sql("INSERT INTO AspNetRoles(Id,Name) VALUES(4,'TeamManager')");

        }
        
        public override void Down()
        {
            DropColumn("dbo.MagazineModels", "PublishDate");
        }
    }
}

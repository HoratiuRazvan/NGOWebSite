namespace Licenta_V0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlusMagazine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MagazineModels", "MagazinePDF", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MagazineModels", "MagazinePDF");
        }
    }
}

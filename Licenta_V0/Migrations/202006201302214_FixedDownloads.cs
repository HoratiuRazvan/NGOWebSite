namespace Licenta_V0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedDownloads : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MagazineModels", "MagazinePDF");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MagazineModels", "MagazinePDF", c => c.Binary());
        }
    }
}

namespace Licenta_V0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PDFSolved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MagazineModels", "MagazinePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MagazineModels", "MagazinePath", c => c.String());
        }
    }
}

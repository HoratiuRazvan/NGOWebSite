namespace Licenta_V0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArticles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleModels", "ArticleImages", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleModels", "ArticleImages");
        }
    }
}

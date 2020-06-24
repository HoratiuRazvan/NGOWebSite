namespace Licenta_V0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlusArticles1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleModels", "ArticleDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleModels", "ArticleDescription");
        }
    }
}

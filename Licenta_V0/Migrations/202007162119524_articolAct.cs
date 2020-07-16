namespace Licenta_V0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class articolAct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleModels", "ArticleText", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ArticleModels", "ArticleText", c => c.String(nullable: false));
        }
    }
}

namespace Licenta_V0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.String(nullable: false, maxLength: 128),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.MemberId, cascadeDelete: false)
                .ForeignKey("dbo.TeamModels", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.TeamModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        TeamLeaderId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TeamLeaderId, cascadeDelete: true)
                .Index(t => t.TeamLeaderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberModels", "TeamId", "dbo.TeamModels");
            DropForeignKey("dbo.TeamModels", "TeamLeaderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MemberModels", "MemberId", "dbo.AspNetUsers");
            DropIndex("dbo.TeamModels", new[] { "TeamLeaderId" });
            DropIndex("dbo.MemberModels", new[] { "TeamId" });
            DropIndex("dbo.MemberModels", new[] { "MemberId" });
            DropTable("dbo.TeamModels");
            DropTable("dbo.MemberModels");
        }
    }
}

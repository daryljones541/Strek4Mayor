namespace Strek4Mayor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Strek4MayorContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsArticles",
                c => new
                    {
                        NewsArticleID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PublishDate = c.DateTime(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.NewsArticleID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsArticles");
        }
    }
}

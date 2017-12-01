namespace Adneotheque.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 55),
                        Lastname = c.String(maxLength: 67),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        DocumentCategories = c.Int(nullable: false),
                        DocumentIdentifier = c.String(nullable: false),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 500),
                        Rating = c.Boolean(nullable: false),
                        ReviewerName = c.String(nullable: false, maxLength: 75),
                        DocumentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId);
            
            CreateTable(
                "dbo.AuthorDocuments",
                c => new
                    {
                        Author_Id = c.Int(nullable: false),
                        Document_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Author_Id, t.Document_Id })
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.Document_Id, cascadeDelete: true)
                .Index(t => t.Author_Id)
                .Index(t => t.Document_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorDocuments", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.AuthorDocuments", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.Reviews", "DocumentId", "dbo.Documents");
            DropIndex("dbo.AuthorDocuments", new[] { "Document_Id" });
            DropIndex("dbo.AuthorDocuments", new[] { "Author_Id" });
            DropIndex("dbo.Reviews", new[] { "DocumentId" });
            DropTable("dbo.AuthorDocuments");
            DropTable("dbo.Reviews");
            DropTable("dbo.Documents");
            DropTable("dbo.Authors");
        }
    }
}

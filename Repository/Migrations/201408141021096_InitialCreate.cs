namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 4000),
                        Answer = c.String(),
                        QuestionTypeEntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionType", t => t.QuestionTypeEntityId, cascadeDelete: true)
                .Index(t => t.QuestionTypeEntityId);
            
            CreateTable(
                "dbo.QuestionType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Question", "QuestionTypeEntityId", "dbo.QuestionType");
            DropIndex("dbo.Question", new[] { "QuestionTypeEntityId" });
            DropTable("dbo.QuestionType");
            DropTable("dbo.Question");
        }
    }
}

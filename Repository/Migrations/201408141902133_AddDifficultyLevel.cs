namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDifficultyLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Question", "DifficultyLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Question", "DifficultyLevel");
        }
    }
}

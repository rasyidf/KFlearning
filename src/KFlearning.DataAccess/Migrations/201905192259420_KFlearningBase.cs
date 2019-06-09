namespace KFlearning.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KFlearningBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        RootPath = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Title = c.String(),
                        Content = c.String(),
                        StreamUri = c.String(),
                        BlogUri = c.String(),
                        SampleUri = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Posts");
            DropTable("dbo.Projects");
        }
    }
}

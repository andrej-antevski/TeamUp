namespace TeamUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtechnologies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Grade = c.Single(nullable: false),
                        Comment = c.String(),
                        Resume_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resumes", t => t.Resume_Id)
                .Index(t => t.Resume_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Technologies", "Resume_Id", "dbo.Resumes");
            DropIndex("dbo.Technologies", new[] { "Resume_Id" });
            DropTable("dbo.Technologies");
        }
    }
}
